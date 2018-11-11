namespace PreviewHandlerTestForm
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._cmb_extensions = new System.Windows.Forms.ComboBox();
            this._btn_preview = new System.Windows.Forms.Button();
            this._panel_preview = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // _cmb_extensions
            // 
            this._cmb_extensions.BackColor = System.Drawing.Color.White;
            this._cmb_extensions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cmb_extensions.FormattingEnabled = true;
            this._cmb_extensions.Items.AddRange(new object[] {
            ".bmp",
            ".doc",
            ".docx",
            ".gif",
            ".htm",
            ".jpg",
            ".pdf",
            ".rtf",
            ".tif",
            ".xls",
            ".xlsx",
            ".xml"});
            this._cmb_extensions.Location = new System.Drawing.Point(12, 12);
            this._cmb_extensions.Name = "_cmb_extensions";
            this._cmb_extensions.Size = new System.Drawing.Size(200, 21);
            this._cmb_extensions.TabIndex = 0;
            // 
            // _btn_preview
            // 
            this._btn_preview.Location = new System.Drawing.Point(234, 12);
            this._btn_preview.Name = "_btn_preview";
            this._btn_preview.Size = new System.Drawing.Size(75, 23);
            this._btn_preview.TabIndex = 1;
            this._btn_preview.Text = "Preview";
            this._btn_preview.UseVisualStyleBackColor = true;
            this._btn_preview.Click += new System.EventHandler(this._btn_preview_Click);
            // 
            // _panel_preview
            // 
            this._panel_preview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._panel_preview.BackColor = System.Drawing.Color.White;
            this._panel_preview.Location = new System.Drawing.Point(12, 48);
            this._panel_preview.Name = "_panel_preview";
            this._panel_preview.Size = new System.Drawing.Size(556, 340);
            this._panel_preview.TabIndex = 2;
            this._panel_preview.Resize += new System.EventHandler(this._panel_preview_Resize);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 400);
            this.Controls.Add(this._panel_preview);
            this.Controls.Add(this._btn_preview);
            this.Controls.Add(this._cmb_extensions);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox _cmb_extensions;
        private System.Windows.Forms.Button _btn_preview;
        private System.Windows.Forms.Panel _panel_preview;
    }
}

