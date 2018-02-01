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
        private string Id;
        public View(string id)
        {
            Id = id;
            InitializeComponent();
        }

        private void View_Load(object sender, EventArgs e)
        {
            var file = string.Format(@"D:\arhive\{0}.pdf", Id);
            this.radPdfViewer1.LoadDocument(string.Format(@"D:\arhive\{0}.pdf", Id));
        }

        private void View_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.radPdfViewer1.UnloadDocument();
        }
    }
}
