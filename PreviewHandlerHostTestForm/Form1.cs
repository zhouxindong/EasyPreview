using System;
using System.Windows.Forms;
using EasyPreview.Host;
using EasyPreview.PaginalImage;

namespace PreviewHandlerHostTestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + @"TestDoc\";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void browseFileButton_Click(object sender, EventArgs e)
        {
            var dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
                pathToDocumentTextBox.Text = openFileDialog.FileName;
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            ((IPaginalPreviewHandlerHost)this.previewContainer).PreviousPage();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            ((IPaginalPreviewHandlerHost)this.previewContainer).NextPage();
        }

        private void openDocumentButton_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(pathToDocumentTextBox.Text))
            {
                this.previewContainer.DisplayType =
                    this.displayTypeContentRadioButton.Checked
                    ? DisplayType.DisplayContent
                    : DisplayType.DisplayIcon;

                this.previewContainer.CreateLink(pathToDocumentTextBox.Text);

                if (this.previewContainer.PagesNumber > 1)
                {
                    nextButton.Enabled = true;
                    previousButton.Enabled = true;
                }
                else
                {
                    nextButton.Enabled = false;
                    previousButton.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show(
                    string.Format(
                        "Sorry! Document '{0}' does not exist!",
                        pathToDocumentTextBox.Text
                    )
                );
            }
        }
    }
}
