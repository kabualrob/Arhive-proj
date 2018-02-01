namespace Arhive2018
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.rememberMeChB = new System.Windows.Forms.CheckBox();
            this.radTextBox2 = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radTextBox1 = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radButton3 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.radGroupBox1.Controls.Add(this.rememberMeChB);
            this.radGroupBox1.Controls.Add(this.radTextBox2);
            this.radGroupBox1.Controls.Add(this.radLabel2);
            this.radGroupBox1.Controls.Add(this.radTextBox1);
            this.radGroupBox1.Controls.Add(this.radLabel1);
            this.radGroupBox1.HeaderText = "Учетные данные";
            this.radGroupBox1.Location = new System.Drawing.Point(25, 12);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Size = new System.Drawing.Size(243, 163);
            this.radGroupBox1.TabIndex = 14;
            this.radGroupBox1.Text = "Учетные данные";
            // 
            // rememberMeChB
            // 
            this.rememberMeChB.AutoSize = true;
            this.rememberMeChB.Location = new System.Drawing.Point(14, 134);
            this.rememberMeChB.Name = "rememberMeChB";
            this.rememberMeChB.Size = new System.Drawing.Size(115, 17);
            this.rememberMeChB.TabIndex = 4;
            this.rememberMeChB.Text = "Запомнить меня";
            this.rememberMeChB.UseVisualStyleBackColor = true;
            // 
            // radTextBox2
            // 
            this.radTextBox2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radTextBox2.Location = new System.Drawing.Point(14, 104);
            this.radTextBox2.Name = "radTextBox2";
            this.radTextBox2.PasswordChar = '*';
            this.radTextBox2.Size = new System.Drawing.Size(212, 23);
            this.radTextBox2.TabIndex = 2;
            this.radTextBox2.Click += new System.EventHandler(this.radTextBox2_Click);
            this.radTextBox2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.radTextBox2_KeyUp);
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radLabel2.Location = new System.Drawing.Point(14, 77);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(57, 21);
            this.radLabel2.TabIndex = 2;
            this.radLabel2.Text = "Пароль:";
            // 
            // radTextBox1
            // 
            this.radTextBox1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radTextBox1.Location = new System.Drawing.Point(14, 44);
            this.radTextBox1.Name = "radTextBox1";
            this.radTextBox1.Size = new System.Drawing.Size(212, 23);
            this.radTextBox1.TabIndex = 1;
            this.radTextBox1.Click += new System.EventHandler(this.radTextBox1_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radLabel1.Location = new System.Drawing.Point(14, 17);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(48, 21);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Логин:";
            // 
            // radButton1
            // 
            this.radButton1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radButton1.Image = global::Arhive2018.Properties.Resources.close;
            this.radButton1.Location = new System.Drawing.Point(89, 245);
            this.radButton1.Name = "radButton1";
            this.radButton1.Padding = new System.Windows.Forms.Padding(5);
            this.radButton1.Size = new System.Drawing.Size(92, 44);
            this.radButton1.TabIndex = 16;
            this.radButton1.Text = "&Отмена";
            this.radButton1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radButton3
            // 
            this.radButton3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radButton3.Image = global::Arhive2018.Properties.Resources.login;
            this.radButton3.Location = new System.Drawing.Point(89, 181);
            this.radButton3.Name = "radButton3";
            this.radButton3.Padding = new System.Windows.Forms.Padding(5);
            this.radButton3.Size = new System.Drawing.Size(92, 44);
            this.radButton3.TabIndex = 15;
            this.radButton3.Text = "&Вход";
            this.radButton3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.radButton3.Click += new System.EventHandler(this.radButton3_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 305);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.radButton3);
            this.Controls.Add(this.radGroupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Вход в систему";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton radButton3;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private System.Windows.Forms.CheckBox rememberMeChB;
        private Telerik.WinControls.UI.RadTextBox radTextBox2;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox radTextBox1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
    }
}