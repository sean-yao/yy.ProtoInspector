using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fiddler;

namespace yy.ProtoInspector
{
    public abstract class ProtoViewer : Inspector2
    {
        private ProtoControl protoControl;
        private byte[] originalBody;
        private Encoding encoding = CONFIG.oHeaderEncoding;

        protected HTTPHeaders OriginalHeaders { get; set; }

        protected ProtoViewer()
        {
        }

        public virtual byte[] body
        {
            get
            {
                if (!protoControl.ProtoEditor.Modified)
                {
                    return null;
                }

                string text = protoControl.ProtoRaw.Text;
                text = text.Replace("\n", "\r\n");
                byte[] bytes = encoding.GetBytes(text);
                if (!Parser.FindEntityBodyOffsetFromArray(bytes, out int _, out int entityBodyOffset, out HTTPHeaderParseWarnings _))
                {
                    return null;
                }
                if (1 > bytes.Length - entityBodyOffset)
                {
                    return Utilities.emptyByteArray;
                }

                byte[] array = new byte[bytes.Length - entityBodyOffset];
                Buffer.BlockCopy(bytes, entityBodyOffset, array, 0, array.Length);
                return array;
            }
            set
            {
                this.originalBody = value;
                DoRefresh();
            }
        }


        private string MimeType()
        {
            if (this.OriginalHeaders == null)
            {
                return string.Empty;
            }

            return this.OriginalHeaders["Content-Type"]?.Trim();
        }


        public bool bDirty => protoControl.ProtoEditor.Modified;

        public bool bReadOnly
        {
            get { return this.protoControl.ProtoEditor.ReadOnly; }
            set
            {
                var currentValue = this.protoControl.ProtoEditor.ReadOnly;
                this.protoControl.ProtoEditor.ReadOnly = value;
                this.protoControl.ProtoEditor.BackColor = (value ? CONFIG.colorDisabledEdit : Color.FromKnownColor(KnownColor.Window));
                this.protoControl.ProtoEditor.DetectUrls = value;
                if (currentValue != value)
                {
                    DoRefresh();
                }
            }
        }

        public override void AddToTab(TabPage o)
        {
            this.protoControl = new ProtoControl(this,this.CreateJsonViewer());
            o.Text = "Protobuf";
            o.Controls.Add(this.protoControl);
            o.Controls[0].Dock = DockStyle.Fill;
        }
                

        public virtual void Clear()
        {
            this.protoControl.Clear();
            this.originalBody = null;
        }
        
        public override int GetOrder()
        {
            return 101;
        }

        private bool IsContentCompressed()
        {
            if (this.OriginalHeaders == null)
            {
                return false;
            }

            var te = this.OriginalHeaders["Transfer-Encoding"];
            if (te?.ToLowerInvariant()?.Trim() == "gzip")
            {
                return true;
            }

            var ce = this.OriginalHeaders["Content-Encoding"];
            if (ce?.ToLowerInvariant()?.Trim() == "gzip")
            {
                return true;
            }

            return false;
        }

        private void DoRefresh()
        {
            bool compressed = this.IsContentCompressed();
            int score = this.ScoreForContentType(this.MimeType());
            this.protoControl.SetContent(score, compressed, this.OriginalHeaders, this.originalBody);
        }

        public override int ScoreForContentType(string sMIMEType)
        {
            if (sMIMEType.OICStartsWith("application/octet-stream"))
            {
                return 60;
            }

            if (sMIMEType.OICStartsWith("application/protobuf"))
            {
                return 95;
            }

            if (sMIMEType.OICStartsWith("application/vnd.google.protobuf"))
            {
                return 95;
            }

            if (sMIMEType.OICStartsWith("application/vnd.google.octet-stream-compressible"))
            {
                return 75;
            }

            if (sMIMEType.OICStartsWith("application/x-protobuf"))
            {
                return 75;
            }

            return -1;
        }

        public override void SetFontSize(float flSizeInPoints)
        {
            if (this.protoControl != null)
            {
                this.protoControl.ProtoEditor.Font = new Font(this.protoControl.ProtoEditor.Font.FontFamily, flSizeInPoints);
                this.protoControl.ProtoRaw.Font = new Font(this.protoControl.ProtoRaw.Font.FontFamily, flSizeInPoints);
            }
        }

        protected abstract dynamic CreateJsonViewer();
    }
}
