using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.Windows.Documents.Flow.FormatProviders.Docx;
//using Telerik.WinForms.Documents.FormatProviders.OpenXml.Docx;
//using Telerik.WinForms.Documents.Model;
//using Telerik.Windows;
using Telerik.Windows.Documents.Flow.Model;
using Microsoft.Office;

namespace Arhive2018.FORMS
{
    public partial class View : Telerik.WinControls.UI.RadForm
    {
        private string FilePath;
        public View(string file)
        {
            FilePath = file;
            InitializeComponent();
        }

        private void View_Load(object sender, EventArgs e)
        {
            // var file = string.Format(@"D:\arhive\{0}.pdf", Id);
            if (FilePath.ToUpper().EndsWith(".PDF"))
            {

                radPdfViewerNavigator1.Visible = true;
                radPdfViewer1.Visible = true;
                pictureBox1.Visible = false;
                // radPdfViewer1.Show();
                //  radPdfViewerNavigator1.Show();
                // pictureBox1.Hide();
                try
                {
                    this.radPdfViewer1.LoadDocument(FilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            if (FilePath.ToUpper().EndsWith(".JPG"))
            {
                pictureBox1.Visible = true;
                // radPdfViewer1.Visible = false;
                radPdfViewerNavigator1.Visible = false;
                pictureBox1.Image = new Bitmap(FilePath);
            }
            if (FilePath.ToUpper().EndsWith(".DOCX"))
            {
                Process.Start(FilePath);
               // this.Application.Documents.Open(FilePath);
                DocxFormatProvider provider = new DocxFormatProvider();
                using (Stream input = File.OpenRead(FilePath))
                {
                    RadFlowDocument document = provider.Import(input);
                    radRichTextEditor1.Insert( "sfdsfsdfdsf");
                }

            }
        }

        private void View_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.radPdfViewer1.UnloadDocument();
        }
    }
}
