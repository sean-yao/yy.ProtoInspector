using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fiddler;

namespace yy.ProtoInspector
{
    public partial class ProtoControl: UserControl        
    {
        ProtoSerializer serializer;
        dynamic jsonViewer;
        Inspector2 owner;
        public ProtoControl(Inspector2 owner, dynamic jsonViewer)
        {
            
            InitializeComponent();
            this.owner = owner;
            this.serializer = new ProtoSerializer(this.RefreshProtos);
            this.jsonViewer = jsonViewer;
            this.jsonViewer.AddToTab(this.tabPageJson);
        }

        private void RefreshProtos(ProtoSerializer protoSerializer)
        {
            this.comboBoxProtos.Items.Clear();
            foreach(var k in protoSerializer.LoadedProtos.Keys)
            {
                this.comboBoxProtos.Items.Add(k);
            }

            var defaultType = this.owner is IRequestInspector2 ? "ClientToServerMessage" : "ClientToServerResponse";
            this.comboBoxProtos.SelectedItem = protoSerializer.LoadedProtos.Keys.FirstOrDefault(x => x.Name.EndsWith(defaultType));
        }

        internal RichTextBox ProtoEditor => this.protoEditor;

        internal TextBox ProtoRaw => this.protoRaw;

        internal void Clear()
        {
            this.ProtoEditor.Clear();
            this.ProtoRaw.Clear();            
        }

        internal void SetContent(int score, bool compressed, HTTPHeaders headers, byte[] originalBody)
        {
            this.Clear();

            if (compressed)
            {
                originalBody = Utility.DecompressBody(headers, originalBody);
            }

            var hex = originalBody.ToHexString();
            this.ProtoRaw.Text = hex;       
            
            if(score < 0)
            {
                this.ProtoEditor.Text = "NOT a protobuf!!!";
                this.protoEditor.ForeColor = Color.Red;
                return;
            }
            
            if(this.comboBoxProtos.SelectedItem == null)
            {
                this.ProtoEditor.Text = "Please selected a protobuf type first!";
                this.protoEditor.ForeColor = Color.Red;
                return;
            }

            var t = (Type)this.comboBoxProtos.SelectedItem;
            var o = this.serializer.ProtoBuffDeserailize(originalBody, t);
            var json = this.serializer.TextSerailize(o, t);
            this.ProtoEditor.Text = json;
            this.protoEditor.ForeColor = Color.FromArgb(0,25,25,25);            
            this.jsonViewer.bReadOnly = true;
            this.jsonViewer.body = Encoding.UTF8.GetBytes(json);            
        }

        private void buttonLoadAssembly_Click(object sender, EventArgs e)
        {
            if (this.serializer == null)
            {
                return;
            }

            this.serializer.PromptToLoadAssembly();
        }
    }
}
