using Arhive2018.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Arhive2018.TOOL;
using Arhive2018.FORMS;
using Arhive2018.Entities;

namespace Arhive2018
{
    public partial class Login : Telerik.WinControls.UI.RadForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            if (rememberMeChB.Checked)
            {
                Settings.Default.UserName = radTextBox1.Text;
                Settings.Default.Password = radTextBox2.Text;
                Settings.Default.Save();
            }
            LoginMe();
        }
        private void LoginMe()
        {
            int id = 0;
            string name = "",description="";
            bool isAdmin = false, blocked = true; ;

            QueryMachine.CheckUser(radTextBox1.Text, radTextBox2.Text, out name, out isAdmin, out blocked, out  id, out  description);
            if (name != "")
            {
                if (!blocked)
                {
                    var user = new User(id,name,description,isAdmin,blocked);
                    var main = new MainForm(user);
                    Hide();
                    main.ShowDialog();
                    Close();
                }
                else MessageBox.Show(string.Format(@"Пользователь {0} заблокирован. Обратитесь к администратору",name), @"Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else MessageBox.Show(@"Неправильное имя и/или пароль", @"Вход в систему", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void radTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) LoginMe();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            radTextBox1.Text = Settings.Default.UserName;
            radTextBox2.Text = Settings.Default.Password;
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radTextBox1_Click(object sender, EventArgs e)
        {
            radTextBox1.SelectAll();
        }

        private void radTextBox2_Click(object sender, EventArgs e)
        {
            radTextBox2.SelectAll();
        }
    }
}
