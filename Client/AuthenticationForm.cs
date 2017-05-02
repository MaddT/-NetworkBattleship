using System;
using System.Windows.Forms;

namespace Client
{
    public partial class AuthenticationForm : Form
    {
        bool signup = false;
        MainForm mainForm;

        public AuthenticationForm(MainForm mform)
        {
            InitializeComponent();
            mainForm = mform;
        }

        private void AuthenticationForm_Load(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            lblConfirm.Visible = false;
            tbConfirmPass.Visible = false;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            signup = true;
            btnLogin.Text = "Зарегистрировать";
            this.Text = "Регистрация";
            lblConfirm.Visible = true;
            tbConfirmPass.Visible = true;
            btnSignUp.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (signup)
            {
                btnLogin.Text = "Войти";
                this.Text = "Вход";
                signup = false;
                lblConfirm.Visible = false;
                tbConfirmPass.Visible = false;
                btnSignUp.Enabled = true;
            }
            else
                mainForm.AllClose();
        }

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {
            if (tbUserName.Text.Trim() == string.Empty ||
                tbPassword.Text.Trim() == string.Empty)
                btnLogin.Enabled = false;
            else
                btnLogin.Enabled = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbUserName.Text.Trim().Length < 4)
            {
                MessageBox.Show("Имя пользователя должно иметь минимум 4 символа.");
                return;
            }
            if (tbPassword.Text.Length < 6)
            {
                MessageBox.Show("Пароль должен иметь минимум 6 символов.");
                return;
            }
            if (!signup)
                mainForm.LoginUser(tbUserName.Text.Trim(), tbPassword.Text);
            else
                if (tbPassword.Text == tbConfirmPass.Text)
                mainForm.RegisterUser(tbUserName.Text.Trim(), tbPassword.Text);
            else
                MessageBox.Show("Подтверждение пароля не совпадает!");
        }

        private void AuthenticationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.AllClose();
        }

        public void LoginError()
        {
            MessageBox.Show("Логин или пароль введены неправильно!" +
                "\r\nВведите другие данные.");
        }
    }
}
