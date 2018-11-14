using EasyPreview.Host;

namespace PreviewHandlerHostTestForm
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
            this.components = new System.ComponentModel.Container();
            this.previewContainer = new EasyPreview.Host.PreviewContainer();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fullPathToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pathToDocumentLabel = new System.Windows.Forms.Label();
            this.browseFileButton = new System.Windows.Forms.Button();
            this.pathToDocumentTextBox = new System.Windows.Forms.TextBox();
            this.displayTypeContentRadioButton = new System.Windows.Forms.RadioButton();
            this.displayTypeIconRadioButton = new System.Windows.Forms.RadioButton();
            this.openDocumentButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // previewContainer
            // 
            this.previewContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewContainer.DisplayType = EasyPreview.Host.DisplayType.DisplayIcon;
            this.previewContainer.Location = new System.Drawing.Point(12, 96);
            this.previewContainer.Name = "previewContainer";
            this.previewContainer.Size = new System.Drawing.Size(569, 287);
            this.previewContainer.TabIndex = 0;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // pathToDocumentLabel
            // 
            this.pathToDocumentLabel.AutoSize = true;
            this.pathToDocumentLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pathToDocumentLabel.Location = new System.Drawing.Point(9, 7);
            this.pathToDocumentLabel.Name = "pathToDocumentLabel";
            this.pathToDocumentLabel.Size = new System.Drawing.Size(112, 13);
            this.pathToDocumentLabel.TabIndex = 13;
            this.pathToDocumentLabel.Text = "Full path to document:";
            this.fullPathToolTip.SetToolTip(this.pathToDocumentLabel, "Some preview handlers (MS Office for example) need full path to documents!");
            // 
            // browseFileButton
            // 
            this.browseFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseFileButton.Location = new System.Drawing.Point(506, 21);
            this.browseFileButton.Name = "browseFileButton";
            this.browseFileButton.Size = new System.Drawing.Size(75, 23);
            this.browseFileButton.TabIndex = 14;
            this.browseFileButton.Text = "Browse...";
            this.browseFileButton.UseVisualStyleBackColor = true;
            this.browseFileButton.Click += new System.EventHandler(this.browseFileButton_Click);
            // 
            // pathToDocumentTextBox
            // 
            this.pathToDocumentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathToDocumentTextBox.Location = new System.Drawing.Point(12, 23);
            this.pathToDocumentTextBox.Name = "pathToDocumentTextBox";
            this.pathToDocumentTextBox.Size = new System.Drawing.Size(488, 20);
            this.pathToDocumentTextBox.TabIndex = 12;
            this.pathToDocumentTextBox.Text = ".\\Samples\\test.tif";
            // 
            // displayTypeContentRadioButton
            // 
            this.displayTypeContentRadioButton.AutoSize = true;
            this.displayTypeContentRadioButton.Checked = true;
            this.displayTypeContentRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.displayTypeContentRadioButton.Location = new System.Drawing.Point(295, 61);
            this.displayTypeContentRadioButton.Name = "displayTypeContentRadioButton";
            this.displayTypeContentRadioButton.Size = new System.Drawing.Size(98, 17);
            this.displayTypeContentRadioButton.TabIndex = 17;
            this.displayTypeContentRadioButton.TabStop = true;
            this.displayTypeContentRadioButton.Text = "Display content";
            this.displayTypeContentRadioButton.UseVisualStyleBackColor = true;
            // 
            // displayTypeIconRadioButton
            // 
            this.displayTypeIconRadioButton.AutoSize = true;
            this.displayTypeIconRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.displayTypeIconRadioButton.Location = new System.Drawing.Point(196, 61);
            this.displayTypeIconRadioButton.Name = "displayTypeIconRadioButton";
            this.displayTypeIconRadioButton.Size = new System.Drawing.Size(82, 17);
            this.displayTypeIconRadioButton.TabIndex = 16;
            this.displayTypeIconRadioButton.Text = "Display icon";
            this.displayTypeIconRadioButton.UseVisualStyleBackColor = true;
            // 
            // openDocumentButton
            // 
            this.openDocumentButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.openDocumentButton.Location = new System.Drawing.Point(12, 49);
            this.openDocumentButton.Name = "openDocumentButton";
            this.openDocumentButton.Size = new System.Drawing.Size(162, 41);
            this.openDocumentButton.TabIndex = 15;
            this.openDocumentButton.Text = "Open linked document";
            this.openDocumentButton.UseVisualStyleBackColor = true;
            this.openDocumentButton.Click += new System.EventHandler(this.openDocumentButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.nextButton.Enabled = false;
            this.nextButton.Location = new System.Drawing.Point(280, 389);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 19;
            this.nextButton.Text = "Next >>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // previousButton
            // 
            this.previousButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.previousButton.Enabled = false;
            this.previousButton.Location = new System.Drawing.Point(199, 389);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(75, 23);
            this.previousButton.TabIndex = 18;
            this.previousButton.Text = "<< Previous";
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 424);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.previousButton);
            this.Controls.Add(this.displayTypeContentRadioButton);
            this.Controls.Add(this.displayTypeIconRadioButton);
            this.Controls.Add(this.openDocumentButton);
            this.Controls.Add(this.browseFileButton);
            this.Controls.Add(this.pathToDocumentLabel);
            this.Controls.Add(this.pathToDocumentTextBox);
            this.Controls.Add(this.previewContainer);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PreviewContainer previewContainer;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolTip fullPathToolTip;
        private System.Windows.Forms.Button browseFileButton;
        private System.Windows.Forms.Label pathToDocumentLabel;
        private System.Windows.Forms.TextBox pathToDocumentTextBox;
        private System.Windows.Forms.RadioButton displayTypeContentRadioButton;
        private System.Windows.Forms.RadioButton displayTypeIconRadioButton;
        private System.Windows.Forms.Button openDocumentButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button previousButton;
    }
}

