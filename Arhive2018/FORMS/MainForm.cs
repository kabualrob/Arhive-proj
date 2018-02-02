using Arhive2018.Entities;
using Arhive2018.Properties;
using Arhive2018.TOOL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.Export;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace Arhive2018.FORMS
{
    public partial class MainForm : Telerik.WinControls.UI.RadForm
    {

        private User _user;
        public MainForm(User user)
        {
            _user = user;
            InitializeComponent();
            if (!user.IsAdmin)//Проверка если пользователь не администратор то скрываем пункты меню кроме экспорта
            {
                radMenu1.Items[0].Visibility = ElementVisibility.Hidden;
                radMenu1.Items[1].Visibility = ElementVisibility.Hidden;
                radMenu1.Items[2].Visibility = ElementVisibility.Hidden;
            }
            UserLb.Text = user.Fio;
            splitContainer1.SplitterDistance = 0;
            radGroupBox1.Visible = false;
            //записываем в приложение пути хранения файлов по умолчанию из БД
            Settings.Default.ArhivePath = QueryMachine.GetArhivePath();
            Settings.Default.LogoPath = QueryMachine.GetLogoPath();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //загрузка видов отчета
            this.rEPORT_TYPETableAdapter.Fill(this.aRHIVE1DataSet3.REPORT_TYPE);
            // загрузка пользователей
            this.uSERTableAdapter.Fill(this.aRHIVE1DataSet2.USER);
            // загрузка основного архива
            this.aRHIVE_VIEWTableAdapter.Fill(this.aRHIVE1DataSet.ARHIVE_VIEW);
            //  this.StyleCell(this.radGridView1.Rows[1].Cells[8]);
            setRowNumbers();//устанавливает номера по порядку в таблице
            numberUpDn.Maximum = radgridView1.Rows.Count;//максимальный номер строки
        }

        private void setRowNumbers()
        {
            for (int i = 0; i < radgridView1.Rows.Count; i++)
            {
                radgridView1.Rows[i].Cells[0].Value = i+1;
            }
        }
        //Получение информации по выбранной позиции
        private void GetPositionInfo(int position)
        {
            if (radgridView1.Rows.Count > 0 && radgridView1.Rows.Count>= position)
            {
                if (radgridView1.Rows[position - 1].Cells[0].Value != null)
                    numberUpDn.Value = Convert.ToInt32(radgridView1.Rows[position - 1].Cells[0].Value);
                objectTB.Text = radgridView1.Rows[position-1].Cells[2].Value.ToString();
                agreementNumberTB.Text= radgridView1.Rows[position - 1].Cells[3].Value.ToString();
                if(radgridView1.Rows[position - 1].Cells[4].Value!=null)
                releaseDateDTP.Value = DateTime.ParseExact(radgridView1.Rows[position - 1].Cells[4].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if(radgridView1.Rows[position - 1].Cells[5].Value.ToString()!="")
                FioCB.Text = radgridView1.Rows[position - 1].Cells[5].Value.ToString();
                else FioCB.SelectedIndex = -1;
                if (radgridView1.Rows[position - 1].Cells[6].Value.ToString() != "")
                    reportTypeCB.Text = radgridView1.Rows[position - 1].Cells[6].Value.ToString();
                else reportTypeCB.SelectedIndex = -1;
                quantityTB.Text = radgridView1.Rows[position - 1].Cells[7].Value.ToString();
                commentRTB.Text = radgridView1.Rows[position - 1].Cells[8].Value.ToString();
                string path = string.Format(@"{1}\{0}.pdf", radgridView1.Rows[position - 1].Cells[1].Value.ToString(), Settings.Default.ArhivePath);
                if (File.Exists(path))
                {
                    UploadFileBn.Text = "Загружен";
                }
                else
                    UploadFileBn.Text = "Загрузить";
            }
        }

        ImageList imagesList1 = new ImageList();
        /* кастомный вид ячейки - не используется
        Font myFont = new Font(new FontFamily("Calibri"), 12.0F, FontStyle.Bold);
        private void StyleCell(GridViewCellInfo cell)
        {
            cell.Style.Font = myFont;
            cell.Style.CustomizeFill = true;
            cell.Style.GradientStyle = GradientStyles.Solid;
            cell.Style.BackColor = Color.FromArgb(162, 215, 255);
        }*/
        private void radGridView1_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            imagesList1.Images.Add(Arhive2018.Properties.Resources.close);
            imagesList1.Images.Add(Arhive2018.Properties.Resources.magnifier);
            imagesList1.Images.Add(Arhive2018.Properties.Resources.download);
         //   GridCommandCellElement cmdCell = e.CellElement as GridCommandCellElement;
         //добавление иконок к просмотру
            if (/*cmdCell != null && */e.RowIndex!=-1 && e.Column.Name == "VIEW")
            {
                var file = string.Format(@"{1}\{0}.pdf", radgridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), Settings.Default.ArhivePath);
                if (!File.Exists(file))
                {
                  //  this.SetStyle(e.CellElement);
                    // cmdCell.Image = null;
                    e.CellElement.Image = imagesList1.Images[0];
                    //  e.CellElement.Image = new Bitmap(Arhive2018.Properties.Resources.close); ;
                }
                else
                {
                    e.CellElement.Image = imagesList1.Images[1];;
                }
            }
            //добавление иконок к загрузки
            if (e.RowIndex != -1 && e.Column.Name == "DOWNLOAD")
            {
                var file = string.Format(@"{1}\{0}.pdf", radgridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), Settings.Default.ArhivePath);
                if (!File.Exists(file))
                {
                    e.CellElement.Image = imagesList1.Images[0];
                }
                else
                {
                    e.CellElement.Image = imagesList1.Images[2];
                }
            }
        }
        //Просмотр и загрузка файла по нажатию на иконки
        private void radGridView1_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            { 
            if (e.Column != null && e.Column.Name == "VIEW")
            {
                var file = string.Format(@"{1}\{0}.pdf", radgridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), Settings.Default.ArhivePath);
                if (File.Exists(file))
                {
                    Form frm = new View(radgridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    frm.Show();
                }
                else MessageBox.Show((string.Format("Файл {0} не найден!", file)), "Предупреждение", MessageBoxButtons.OK);

            }
            else if (e.Column != null && e.Column.Name == "DOWNLOAD")
            {
                    saveFileDialog1.Filter = " Portable Document Format|*.pdf";
                    saveFileDialog1.Title = "Сохранить в pdf";
                    string fileName = radgridView1.CurrentRow.Cells["OBJECT"].Value.ToString();
                    foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                    {
                        fileName = fileName.Replace(c, '_');
                    }
                    saveFileDialog1.FileName = string.Format("{1} {0:yyyy-MM-dd_HH-mm}", DateTime.Now, fileName);
                    saveFileDialog1.ShowDialog();
                    var file = string.Format(@"{1}\{0}.pdf", radgridView1.Rows[e.RowIndex].Cells[1].Value.ToString(), Settings.Default.ArhivePath);
                if (File.Exists(file))
                {
                    WebClient webClient = new WebClient();                       
                    webClient.DownloadFile(file, saveFileDialog1.FileName);
                    }
                else MessageBox.Show((string.Format("Файл {0} не найден!", file)), "Предупреждение", MessageBoxButtons.OK);

            }
            else
            {
                objectTB.Text = radgridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
         }
        }

        private void UploadFileBn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Portable Document Format|*.pdf";
            openFileDialog1.FileName = "*.pdf";
            openFileDialog1.ShowDialog();

        }
        //выбор файла pdf
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (openFileDialog1.FileName.EndsWith(".pdf"))
            {
                 filePathLb.Text = openFileDialog1.FileName;
            }
            else MessageBox.Show(string.Format(@"Выбранный файл {0} не соответствует формату Pdf",openFileDialog1.FileName), @"Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //экспорт архива в pdf
        private void exportToPdfBn_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = " Portable Document Format|*.pdf";
            saveFileDialog1.Title = "Сохранить в pdf";
            saveFileDialog1.FileName = string.Format("Архив от {0:yyyy-MM-dd_HH-mm}", DateTime.Now);
            saveFileDialog1.ShowDialog();
            Telerik.WinControls.Export.GridViewPdfExport pdfExporter = new Telerik.WinControls.Export.GridViewPdfExport(this.radgridView1);
            pdfExporter.FileExtension = ".pdf";
            radgridView1.Columns["VIEW"].IsVisible = false;
            radgridView1.Columns["DOWNLOAD"].IsVisible = false;
            radgridView1.Columns["COMMENT"].IsVisible = false;
            pdfExporter.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
            pdfExporter.ShowHeaderAndFooter = true;
            pdfExporter.HeaderHeight = 30;
            pdfExporter.HeaderFont = new Font("Arial", 22);
            if(File.Exists(Settings.Default.LogoPath))
            pdfExporter.Logo = System.Drawing.Image.FromFile(Settings.Default.LogoPath);
            pdfExporter.LeftHeader = "[Logo]";
            pdfExporter.LogoAlignment = ContentAlignment.MiddleLeft;
            pdfExporter.LogoLayout = Telerik.WinControls.Export.LogoLayout.Fit;

         /*   pdfExporter.MiddleHeader = "Middle header";
            pdfExporter.RightHeader = "Right header";
            pdfExporter.ReverseHeaderOnEvenPages = true;

            pdfExporter.FooterHeight = 30;
            pdfExporter.FooterFont = new Font("Arial", 22);
            pdfExporter.LeftFooter = "Left footer";
            pdfExporter.MiddleFooter = "Middle footer";
            pdfExporter.RightFooter = "Right footer";
            pdfExporter.ReverseFooterOnEvenPages = true;

            pdfExporter.SummariesExportOption = SummariesOption.ExportAll;
            pdfExporter.ExportSettings.Description = "Document Description";*/
            pdfExporter.Scale = 0.7;
            pdfExporter.FitToPageWidth = true;
            //string fileName = "D:\\ExportedData.pdf";
            pdfExporter.RunExport(saveFileDialog1.FileName, new Telerik.WinControls.Export.PdfExportRenderer());
            radgridView1.Columns["VIEW"].IsVisible = true;
            radgridView1.Columns["DOWNLOAD"].IsVisible = true;
            radgridView1.Columns["COMMENT"].IsVisible = true;

            /*  GridViewSpreadExport spreadExporter = new GridViewSpreadExport(this.radGridView1);
              SpreadExportRenderer exportRenderer = new SpreadExportRenderer();
                spreadExporter.ExportFormat = SpreadExportFormat.Pdf;
              radGridView1.Columns["VIEW"].IsVisible = false;
              radGridView1.Columns["DOWNLOAD"].IsVisible = false;
              spreadExporter.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
              spreadExporter.ShowHeaderAndFooter = true;
              spreadExporter.RunExport("D:\\exportedFile.pdf", exportRenderer);
              radGridView1.Columns["VIEW"].IsVisible = true;
              radGridView1.Columns["DOWNLOAD"].IsVisible = true;*/
        }

        private void exportToExcelBn_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = " Excel|*.xlsx";
            saveFileDialog1.Title = "Сохранить как Excel";
            saveFileDialog1.FileName = string.Format("Архив от {0:yyyy-MM-dd_HH-mm}",DateTime.Now); 
            saveFileDialog1.ShowDialog();
            GridViewSpreadExport spreadExporter = new GridViewSpreadExport(this.radgridView1);
            SpreadExportRenderer exportRenderer = new SpreadExportRenderer();
            //  spreadExporter.ChildViewExportMode= GridViewSpreadExport
            //  spreadExporter.ExportFormat = SpreadExportFormat.Pdf;
            radgridView1.Columns["VIEW"].IsVisible = false;
            radgridView1.Columns["DOWNLOAD"].IsVisible = false;
            spreadExporter.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
            if(File.Exists(saveFileDialog1.FileName))
            {
                File.Delete(saveFileDialog1.FileName);
            }
            spreadExporter.RunExport(saveFileDialog1.FileName, exportRenderer,"Архив");
            radgridView1.Columns["VIEW"].IsVisible = true;
            radgridView1.Columns["DOWNLOAD"].IsVisible = true;
        }


        private void objectTB_Enter(object sender, EventArgs e)
        {
            if (objectTB.ForeColor == Color.Gray)
            {
                objectTB.Clear();
                objectTB.ForeColor = Color.Black;
            }

        }

        private void numberUpDn_ValueChanged(object sender, EventArgs e)
        {

            if (radgridView1.Rows.Count >= numberUpDn.Value)
            {
                GetPositionInfo(Convert.ToInt32(numberUpDn.Value));
                radgridView1.Rows[Convert.ToInt32(numberUpDn.Value) - 1].IsCurrent = true;
            }
        }

        private void MasterTemplate_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.radgridView1.SelectedRows.Count > 0 /*&& radgridView1.Rows.Count >= numberUpDn.Value*/)
                {
                    if (radgridView1.Rows.Count < numberUpDn.Value)
                    {
                        numberUpDn.Maximum = numberUpDn.Maximum - 1;
                    }
                    int selectedIndex = radgridView1.SelectedRows[0].Index;
                    GetPositionInfo(selectedIndex+1);//получает информацию о выделенной строке в таблице (по умолчанию 1)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void logOUtLb_Click(object sender, EventArgs e)
        {
            var login = new Login();
            Hide();
            login.ShowDialog();
            Close();
        }

        private void saveRecordBn_Click(object sender, EventArgs e)
        {
            int newId = 0;
            if (radgridView1.CurrentRow!=null && Convert.ToInt32(numberUpDn.Value) == Convert.ToInt32(radgridView1.CurrentRow.Cells[0].Value))
            {
                newId =Convert.ToInt32(numberUpDn.Value);
                QueryMachine.UpdateRow(Convert.ToInt32(radgridView1.CurrentRow.Cells[1].Value), objectTB.Text, agreementNumberTB.Text, releaseDateDTP.Value, Convert.ToInt32(FioCB.SelectedValue), Convert.ToInt32(reportTypeCB.SelectedValue), quantityTB.Text, commentRTB.Text);               
            }
            else
            {

                QueryMachine.InsertRow( objectTB.Text, agreementNumberTB.Text, releaseDateDTP.Value, Convert.ToInt32(FioCB.SelectedValue), Convert.ToInt32(reportTypeCB.SelectedValue), quantityTB.Text, commentRTB.Text,out newId,_user.Id,DateTime.Now);
                
            }
            if (filePathLb.Text != "путь:" && filePathLb.Text.EndsWith("pdf"))
            {
                System.IO.File.Copy(filePathLb.Text, string.Format(@"{1}/{0}.pdf", newId, Settings.Default.ArhivePath), true);
            }
            this.aRHIVE_VIEWTableAdapter.Fill(this.aRHIVE1DataSet.ARHIVE_VIEW);
            setRowNumbers();//устанавливает номера по порядку в таблице
            numberUpDn.Maximum = radgridView1.Rows.Count;
            GetPositionInfo(Convert.ToInt32(numberUpDn.Value));
            radgridView1.Rows[Convert.ToInt32(numberUpDn.Value) - 1].IsCurrent = true;
            filePathLb.Text = "путь:";
            MessageBox.Show("Сохранено");
        }

        private void DeleteBn_Click(object sender, EventArgs e)
        {
            if (radgridView1.CurrentRow != null && Convert.ToInt32(numberUpDn.Value) == Convert.ToInt32(radgridView1.CurrentRow.Cells[0].Value))
            {
                DialogResult result = MessageBox.Show(string.Format("Удалить текущую запись?", openFileDialog1.FileName), "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    QueryMachine.DeleteRow(Convert.ToInt32(radgridView1.CurrentRow.Cells[1].Value));
                    this.aRHIVE_VIEWTableAdapter.Fill(this.aRHIVE1DataSet.ARHIVE_VIEW);
                    setRowNumbers();//устанавливает номера по порядку в таблице
                    numberUpDn.Maximum = radgridView1.Rows.Count;
                }
            }
            else
                MessageBox.Show("Невозможно удалить новую запись!");
        }
        //Переименование и ограничение контекстного меню таблицв по нажатиж на правую клавишу мыши
        private void radgridView1_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            if (e.ContextMenu.Items.Count > 0)
            { 
            RadDropDownMenu menu = new RadDropDownMenu();
            for (int i = 0; i <= e.ContextMenu.Items.Count - 1; i++)
            {
                if (e.ContextMenu.Items[i].Text == "Copy")
                    e.ContextMenu.Items[i].Text = "Копировать";

                menu.Items.Add(e.ContextMenu.Items[i]);
            }
            e.ContextMenu = menu;
        }
        }

        private void radgridView1_MouseHover(object sender, EventArgs e)
        {
           // this.Cursor = Cursors.Hand;
        }

        private void radgridView1_CellMouseMove(object sender, MouseEventArgs e)
        {

        }
        Cursor originalCursor;
        private void radgridView1_MouseMove(object sender, MouseEventArgs e)
        {
            RadElement element = this.radgridView1.RootElement.ElementTree.GetElementAtPoint(e.Location);
            GridDataCellElement cell = element as GridDataCellElement;
            if (cell == null && element != null)
            {
                cell = element.FindAncestor<GridDataCellElement>();
            }
            if (cell != null && (cell.ColumnInfo.Name == "VIEW" || cell.ColumnInfo.Name == "DOWNLOAD"))
            {
                if (originalCursor == null)
                {
                    originalCursor = this.radgridView1.Cursor;
                    this.radgridView1.Cursor = Cursors.Hand;
                }

            }
            else
            {
                if (originalCursor != null)
                {
                    this.radgridView1.Cursor = originalCursor;
                    originalCursor = null;
                }
            }
        }
        //создать новую запись
        private void NewRecordRMI_Click(object sender, EventArgs e)
        {
            if (radGroupBox1.Visible == false)
            {
                splitContainer1.SplitterDistance = 130;
                radGroupBox1.Visible = true;
            }
            objectTB.Text = "";
            agreementNumberTB.Text = "";
            releaseDateDTP.Value = DateTime.Now;
            FioCB.SelectedIndex = -1;
            reportTypeCB.SelectedIndex = -1;
            quantityTB.Text = "";
            commentRTB.Text = "";
            UploadFileBn.Text = "Загрузить";
            radgridView1.CurrentRow = null;
            if (radgridView1.Rows.Count == numberUpDn.Maximum)
            {
                numberUpDn.Maximum = numberUpDn.Maximum + 1;
                numberUpDn.Value = numberUpDn.Maximum;
            }
        }
        //просмотреть запись
        private void ShowRecordRMI_Click(object sender, EventArgs e)
        {
            if (radGroupBox1.Visible == true)
            {
                splitContainer1.SplitterDistance = 0;
                radGroupBox1.Visible = false;
            }
            else
            {
                splitContainer1.SplitterDistance = 130;
                radGroupBox1.Visible = true;
            }
        }
        //Настройки
        private void SettingsRMI_Click(object sender, EventArgs e)
        {
            var settings = new UserSettings(this);
            settings.Show();
        }

        /* void spreadExporter_CellFormatting(object sender, Telerik.WinControls.Export.CellFormattingEventArgs e)
{
if (e.GridRowInfoType == typeof(GridViewTableHeaderRowInfo))
{
e.CellStyleInfo.Underline = true;

if (e.GridCellInfo.RowInfo.HierarchyLevel == 0)
{
e.CellStyleInfo.BackColor = Color.DeepSkyBlue;
}
else if (e.GridCellInfo.RowInfo.HierarchyLevel == 1)
{
e.CellStyleInfo.BackColor = Color.LightSkyBlue;
}
}

if (e.GridRowInfoType == typeof(GridViewHierarchyRowInfo))
{
if (e.GridCellInfo.RowInfo.HierarchyLevel == 0)
{
e.CellStyleInfo.IsItalic = true;
e.CellStyleInfo.FontSize = 12;
e.CellStyleInfo.BackColor = Color.GreenYellow;
}
else if (e.GridCellInfo.RowInfo.HierarchyLevel == 1)
{
e.CellStyleInfo.ForeColor = Color.DarkGreen;
e.CellStyleInfo.BackColor = Color.LightGreen;
}
}
}*/
    }
}
