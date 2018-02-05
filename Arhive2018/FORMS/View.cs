using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace Arhive2018.FORMS
{
    public partial class View : Telerik.WinControls.UI.RadForm
    {
        private string File;
        public View(string file)
        {
            File = file;
            InitializeComponent();
        }

        private void View_Load(object sender, EventArgs e)
        {
            // var file = string.Format(@"D:\arhive\{0}.pdf", Id);
            if (File.ToUpper().EndsWith(".PDF"))
            {
                
                radPdfViewerNavigator1.Visible = true;
                radPdfViewer1.Visible = true;
                  pictureBox1.Visible = false;
                // radPdfViewer1.Show();
                //  radPdfViewerNavigator1.Show();
                // pictureBox1.Hide();
                this.radPdfViewer1.LoadDocument(File);
            }
            if (File.ToUpper().EndsWith(".JPG"))
            {
                pictureBox1.Visible = true;
               // radPdfViewer1.Visible = false;
                radPdfViewerNavigator1.Visible = false;
                pictureBox1.Image = new Bitmap(File);
            }

        }

        private void View_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.radPdfViewer1.UnloadDocument();
        }
    }
}
