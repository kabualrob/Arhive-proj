using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Arhive2018.FORMS;
using Arhive2018.TOOL;
using System.Data.SqlClient;
using Arhive2018.Properties;
using Telerik.WinControls.UI;

namespace Arhive2018.FORMS
{
    public partial class UserSettings : Telerik.WinControls.UI.RadForm
    {
        private MainForm mainForm;
        public UserSettings(MainForm mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void UserSettings_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'reportsDataSet4.REPORT_TYPE' table. You can move, or remove it, as needed.
            this.rEPORT_TYPETableAdapter.Fill(this.reportsDataSet4.REPORT_TYPE);
            // TODO: This line of code loads data into the 'usersDataSet.USER' table. You can move, or remove it, as needed.
            this.uSERTableAdapter.Fill(this.usersDataSet.USER);
            arhivePathTB.Text = Settings.Default.ArhivePath;
            logoPathRTB.Text = Settings.Default.LogoPath;

        }

        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                uSERBindingSource.EndEdit();
                this.uSERTableAdapter.Update(this.usersDataSet.USER);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }            

        }

        private void saveBn_Click(object sender, EventArgs e)
        {
            try
            {
                uSERBindingSource.EndEdit();
                this.uSERTableAdapter.Update(this.usersDataSet.USER);
                mainForm.FioCB.DataSource = QueryMachine.SelectUser();
                mainForm.FioCB.DisplayMember = "FIO";
                mainForm.FioCB.ValueMember = "ID";
                MessageBox.Show("Сохранено", @"Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void saveReportsBn_Click(object sender, EventArgs e)
        {
            try
            {
                this.rEPORT_TYPETableAdapter.Update(this.reportsDataSet4.REPORT_TYPE);
                mainForm.reportTypeCB.DataSource =  QueryMachine.SelectReport();
                mainForm.reportTypeCB.DisplayMember = "REPORT";
                mainForm.reportTypeCB.ValueMember = "ID";
                MessageBox.Show("Сохранено", @"Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radGridView2_RowsChanging(object sender, Telerik.WinControls.UI.GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                bool exist = QueryMachine.CheckIfReportInUse(Convert.ToInt32(reportSettingsRGV.CurrentRow.Cells["ID"].Value));
                if (exist)
                {
                    e.Cancel = true;
                    DialogResult result = MessageBox.Show(string.Format("Вид отчета '{0}' используется. Удаление невозможно. Хотите заблокировать?", reportSettingsRGV.CurrentRow.Cells["REPORT"].Value), "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes)
                    {
                        reportSettingsRGV.CurrentRow.Cells["BLOCKED"].Value = 1;
                    }
                }               
            }

        }

        private void radGridView1_RowsChanging(object sender, Telerik.WinControls.UI.GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                bool exist = QueryMachine.CheckIfUserInUse(Convert.ToInt32(usresSettingsRGV.CurrentRow.Cells["ID"].Value));
                if (exist)
                {
                    e.Cancel = true;
                    DialogResult result = MessageBox.Show(string.Format("Пользователь'{0}' присутствует в отчете. Удаление невозможно. Хотите заблокировать?", usresSettingsRGV.CurrentRow.Cells["FIO"].Value), "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes)
                    {
                        usresSettingsRGV.CurrentRow.Cells["BLOCKED"].Value = 1;
                    }
                }
            }

        }

        private void usresSettingsRGV_CellValueChanged(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {

        }

        private void usresSettingsRGV_ContextMenuOpening(object sender, Telerik.WinControls.UI.ContextMenuOpeningEventArgs e)
        {
            RadDropDownMenu menu = new RadDropDownMenu();
            for (int i = 0; i <= e.ContextMenu.Items.Count - 1; i++)
            {
                if ( usresSettingsRGV.CurrentColumn.Name == "DESCRIPTION" || usresSettingsRGV.CurrentColumn.Name == "PASSWORD" || usresSettingsRGV.CurrentColumn.Name == "FIO" || usresSettingsRGV.CurrentColumn.Name == "LOGIN")
                {
                    switch (e.ContextMenu.Items[i].Text)
                    {
                        case "Copy": e.ContextMenu.Items[i].Text = "Копировать"; menu.Items.Add(e.ContextMenu.Items[i]); break;
                        case "Paste": e.ContextMenu.Items[i].Text = "Вставить"; menu.Items.Add(e.ContextMenu.Items[i]); break;
                        case "Edit": e.ContextMenu.Items[i].Text = "Редактировать"; menu.Items.Add(e.ContextMenu.Items[i]); break;
                        case "Clear Value": e.ContextMenu.Items[i].Text = "Очистить значение"; if (usresSettingsRGV.CurrentColumn.Name != "FIO" && usresSettingsRGV.CurrentColumn.Name != "LOGIN") menu.Items.Add(e.ContextMenu.Items[i]); break;
                        case "Delete Row": e.ContextMenu.Items[i].Text = "Удалить строку"; menu.Items.Add(e.ContextMenu.Items[i]); break;
                    }
                    
                }
                if (usresSettingsRGV.CurrentColumn.Name == "ISADMIN" || usresSettingsRGV.CurrentColumn.Name == "BLOCKED")
                {                   
                    if (string.Equals("Delete Row", e.ContextMenu.Items[i].Text))
                    {
                        e.ContextMenu.Items[i].Text = "Удалить строку";
                        menu.Items.Add(e.ContextMenu.Items[i]);
                    }                        
                }
            }
            e.ContextMenu = menu;
        }

        private void reportSettingsRGV_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
           
            RadDropDownMenu menu = new RadDropDownMenu();
            for (int i = 0; i <= e.ContextMenu.Items.Count - 1; i++)
            {
                if (reportSettingsRGV.CurrentColumn.Name == "REPORT" || reportSettingsRGV.CurrentColumn.Name == "DESCRIPTION" )
                {
                    switch (e.ContextMenu.Items[i].Text)
                    {
                        case "Copy": e.ContextMenu.Items[i].Text = "Копировать"; menu.Items.Add(e.ContextMenu.Items[i]); break;
                        case "Paste": e.ContextMenu.Items[i].Text = "Вставить"; menu.Items.Add(e.ContextMenu.Items[i]); break;
                        case "Edit": e.ContextMenu.Items[i].Text = "Редактировать"; menu.Items.Add(e.ContextMenu.Items[i]); break;
                        case "Clear Value": e.ContextMenu.Items[i].Text = "Очистить значение"; if (reportSettingsRGV.CurrentColumn.Name != "REPORT" ) menu.Items.Add(e.ContextMenu.Items[i]); break;
                        case "Delete Row": e.ContextMenu.Items[i].Text = "Удалить строку"; menu.Items.Add(e.ContextMenu.Items[i]); break;
                    }
                }
                if (reportSettingsRGV.CurrentColumn.Name == "BLOCKED")
                {
                    if (string.Equals("Delete Row", e.ContextMenu.Items[i].Text))
                    {
                        e.ContextMenu.Items[i].Text = "Удалить строку";
                        menu.Items.Add(e.ContextMenu.Items[i]);
                    }
                }
            }
            e.ContextMenu = menu;
        }



        private string translateMenu(RadItem menuEng)
        {
            string menuRus = "";
            switch (menuEng.Text)
            {
                case "Copy": menuRus = "Копировать"; break;
                case "Paste": menuRus = "Вставить"; break;
                case "Edit": menuRus = "Редактировать"; break;
                case "Clear Value": menuRus = "Очистить значение"; break;
                case "Delete Row": menuRus = "Удалить строку"; break;
            }
            return menuRus;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
              //  radTextBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void archivePathBn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            DialogResult result = folderDlg.ShowDialog();
           if (result == DialogResult.OK)
            {
                arhivePathTB.Text = folderDlg.SelectedPath;
                
               // Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void LogoPathBn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = " png|*.png";
            openFileDialog1.FileName = "*.png";
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (openFileDialog1.FileName.EndsWith(".png"))
            {
                //var fileName = openFileDialog1.FileName;
                logoPathRTB.Text= openFileDialog1.FileName;
                
                //   System.IO.File.Copy(fileName, Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName) + ".txt"));
                //  System.IO.File.Copy(fileName, string.Format(@"{1}/{0}.pdf", radgridView1.CurrentRow.Cells[1].Value, Settings.Default.ArhivePath), true);

                // DialogResult result = MessageBox.Show(string.Format("Файл {0} успешно загружен", openFileDialog1.FileName), "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // filePathLb.Text = openFileDialog1.FileName;
                // if (result == DialogResult.Yes)
                {
                    //   Transfer_Bn.Enabled = true;
                }
             //   UploadFileBn.Text = "Загружен";
            }
            else MessageBox.Show(string.Format(@"Выбранный файл {0} не соответствует формату *.png", openFileDialog1.FileName), @"Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void pathsSaveBn_Click(object sender, EventArgs e)
        {
            Settings.Default.ArhivePath = arhivePathTB.Text;
            QueryMachine.UpdatePath(arhivePathTB.Text);
            Settings.Default.LogoPath = logoPathRTB.Text;
             QueryMachine.UpdatePathLogo(logoPathRTB.Text);
            MessageBox.Show("Сохранено", @"Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
