namespace yy.ProtoInspector
{
    public partial class ProtoControl 
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxProtos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLoadAssembly = new System.Windows.Forms.Button();
            this.tabPageHex = new System.Windows.Forms.TabPage();
            this.protoRaw = new System.Windows.Forms.TextBox();
            this.tabPageText = new System.Windows.Forms.TabPage();
            this.protoEditor = new System.Windows.Forms.RichTextBox();
            this.tabControlProtoBuf = new System.Windows.Forms.TabControl();
            this.tabPageJson = new System.Windows.Forms.TabPage();
            this.tabPageHex.SuspendLayout();
            this.tabPageText.SuspendLayout();
            this.tabControlProtoBuf.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxProtos
            // 
            this.comboBoxProtos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProtos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxProtos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxProtos.FormattingEnabled = true;
            this.comboBoxProtos.Location = new System.Drawing.Point(111, 25);
            this.comboBoxProtos.Name = "comboBoxProtos";
            this.comboBoxProtos.Size = new System.Drawing.Size(1253, 32);
            this.comboBoxProtos.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "PbType";
            // 
            // buttonLoadAssembly
            // 
            this.buttonLoadAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLoadAssembly.Location = new System.Drawing.Point(1380, 21);
            this.buttonLoadAssembly.Name = "buttonLoadAssembly";
            this.buttonLoadAssembly.Size = new System.Drawing.Size(162, 37);
            this.buttonLoadAssembly.TabIndex = 3;
            this.buttonLoadAssembly.Text = "Load Types";
            this.buttonLoadAssembly.UseVisualStyleBackColor = true;
            this.buttonLoadAssembly.Click += new System.EventHandler(this.buttonLoadAssembly_Click);
            // 
            // tabPageHex
            // 
            this.tabPageHex.Controls.Add(this.protoRaw);
            this.tabPageHex.Location = new System.Drawing.Point(4, 4);
            this.tabPageHex.Name = "tabPageHex";
            this.tabPageHex.Size = new System.Drawing.Size(1537, 1052);
            this.tabPageHex.TabIndex = 2;
            this.tabPageHex.Text = "Hex";
            this.tabPageHex.UseVisualStyleBackColor = true;
            // 
            // protoRaw
            // 
            this.protoRaw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoRaw.Location = new System.Drawing.Point(0, 0);
            this.protoRaw.Multiline = true;
            this.protoRaw.Name = "protoRaw";
            this.protoRaw.ReadOnly = true;
            this.protoRaw.Size = new System.Drawing.Size(1537, 1052);
            this.protoRaw.TabIndex = 1;
            // 
            // tabPageText
            // 
            this.tabPageText.Controls.Add(this.protoEditor);
            this.tabPageText.Location = new System.Drawing.Point(4, 4);
            this.tabPageText.Name = "tabPageText";
            this.tabPageText.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageText.Size = new System.Drawing.Size(1537, 1052);
            this.tabPageText.TabIndex = 0;
            this.tabPageText.Text = "JSONText";
            this.tabPageText.UseVisualStyleBackColor = true;
            // 
            // protoEditor
            // 
            this.protoEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.protoEditor.Location = new System.Drawing.Point(3, 3);
            this.protoEditor.Name = "protoEditor";
            this.protoEditor.Size = new System.Drawing.Size(1531, 1046);
            this.protoEditor.TabIndex = 3;
            this.protoEditor.Text = "";
            // 
            // tabControlProtoBuf
            // 
            this.tabControlProtoBuf.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControlProtoBuf.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlProtoBuf.Controls.Add(this.tabPageText);
            this.tabControlProtoBuf.Controls.Add(this.tabPageJson);
            this.tabControlProtoBuf.Controls.Add(this.tabPageHex);
            this.tabControlProtoBuf.Location = new System.Drawing.Point(1, 64);
            this.tabControlProtoBuf.Name = "tabControlProtoBuf";
            this.tabControlProtoBuf.SelectedIndex = 0;
            this.tabControlProtoBuf.Size = new System.Drawing.Size(1545, 1089);
            this.tabControlProtoBuf.TabIndex = 4;
            // 
            // tabPageJson
            // 
            this.tabPageJson.Location = new System.Drawing.Point(4, 4);
            this.tabPageJson.Name = "tabPageJson";
            this.tabPageJson.Size = new System.Drawing.Size(1537, 1052);
            this.tabPageJson.TabIndex = 3;
            this.tabPageJson.Text = "JSON";
            this.tabPageJson.UseVisualStyleBackColor = true;
            // 
            // ProtoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlProtoBuf);
            this.Controls.Add(this.buttonLoadAssembly);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxProtos);
            this.Name = "ProtoControl";
            this.Size = new System.Drawing.Size(1549, 1154);
            this.tabPageHex.ResumeLayout(false);
            this.tabPageHex.PerformLayout();
            this.tabPageText.ResumeLayout(false);
            this.tabControlProtoBuf.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBoxProtos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLoadAssembly;
        private System.Windows.Forms.TabPage tabPageHex;
        private System.Windows.Forms.TabPage tabPageText;
        private System.Windows.Forms.TabControl tabControlProtoBuf;
        private System.Windows.Forms.TextBox protoRaw;
        private System.Windows.Forms.RichTextBox protoEditor;
        private System.Windows.Forms.TabPage tabPageJson;
    }
}
