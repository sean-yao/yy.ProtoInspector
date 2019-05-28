using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Fiddler;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProtoBuf;
using ProtoBuf.Meta;

namespace yy.ProtoInspector
{
    internal class ProtoSerializer
    {
        const string DefaultAssemblyDir = @"..";
        const string DefaultProtoContractAssembly = @"SampleProtoContracts.dll";
        const string LastAssemblyPathPreferenceKey = "yy.protoinspector.assemblypath.last";

        static Logger logger = new Logger(false);
        static JsonSerializerSettings defaultJsonSettings = new JsonSerializerSettings()
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            Culture = CultureInfo.InvariantCulture,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateFormatString = "yyyy-MM-ddTHH:mm:ss.fffZ",
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
        };

        Dictionary<Type, string> protos = new Dictionary<Type, string>();

        public Dictionary<Type, string> LoadedProtos => protos;

        private Action<ProtoSerializer> refreshProtos;


        public ProtoSerializer(Action<ProtoSerializer> refreshProtos)
        {
            RuntimeTypeModel.Default.AutoCompile = true;
            RuntimeTypeModel.Default.InferTagFromNameDefault = true;
            RuntimeTypeModel.Default.AllowParseableTypes = true;
            this.refreshProtos = refreshProtos;            
            LoadAssembly(GetLastSelectedAssemblyPath());
        }

        private static string GetLastSelectedAssemblyPath()
        {
            var defaultAssembly = Path.Combine(DefaultAssemblyDir, DefaultProtoContractAssembly);
            return FiddlerApplication.Prefs.GetStringPref(LastAssemblyPathPreferenceKey, defaultAssembly);            
        }

        public void PromptToLoadAssembly()
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.DefaultExt = "dll";
            ofd.Filter = "class libraries (*.dll)|*.dll|All files (*.*)|*.*";
            if (Directory.Exists(DefaultAssemblyDir))
            {
                ofd.InitialDirectory = DefaultAssemblyDir;
            }

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.LoadAssembly(ofd.FileName);
                    FiddlerApplication.Prefs.SetStringPref(LastAssemblyPathPreferenceKey, ofd.FileName);                    
                }
                catch (TypeLoadException tle)
                {
                    logger.LogString(tle.ToString());
                }
            }
        }

        private void LoadAssembly(string fileName)
        {
            if (!File.Exists(fileName))
            {
                MessageBox.Show($"File not found:{fileName}", caption: "Error", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                return;
            }

            var assembly = Assembly.LoadFrom(fileName);
            var contracts = GetLoadableTypes(assembly)
                .Where(x => x.GetCustomAttributes<ProtoBuf.ProtoContractAttribute>().Any())
                .OrderBy(x => x.Name);

            var mi = typeof(Serializer).GetMethod("GetProto", new Type[0]);

            this.protos.Clear();
            foreach (var contract in contracts)
            {
                var getProto = mi.MakeGenericMethod(contract);
                try
                {
                    this.protos[contract] = (string)getProto.Invoke(null, null);
                }
                catch (Exception e)
                {
                    logger.LogString($"\nCannot generate proto file for contract type ={contract},\nException={e.ToString()}\n");
                }
            }

            if (this.protos.Count() == 0)
            {
                logger.LogString($"\nThere's no valid proto contract type in the assembly!\n");
            }

            if (this.refreshProtos != null)
            {
                this.refreshProtos(this);
            }
        }

        public static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
        
        public string TextSerailize(object o, Type t)
        {
            return JsonConvert.SerializeObject(o, Formatting.Indented, defaultJsonSettings);
        }

        public string ProtoBuffSerailize(object o, Type t)
        {
            var mi = typeof(Serializer).GetMethods()
                .Where(x => x.Name == "Serialize" && x.GetParameters().FirstOrDefault()?.ParameterType == typeof(Stream))
                .Single();
            var serializer = mi.MakeGenericMethod(t);
            using (var memoryStream = new MemoryStream())
            {
                serializer.Invoke(null, new[] { memoryStream, o });
                return memoryStream.ToArray().ToHexString();
            }
        }

        public object ProtoBuffDeserailize(byte[] data, Type t)
        {
            try
            {
                return Serializer.Deserialize(t, new MemoryStream(data));
            }
            catch (ProtoException pe)
            {
                logger.LogFormat("Deserialization error:{0}", pe);
            }

            return null;
        }
    }
}
