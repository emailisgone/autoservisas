using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autoservisas_P3PL1_API
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text))
            {
                MessageBox.Show("Please enter user name", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter password", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CheckLogin();
        }

        private async void CheckLogin()
        {
            if(await Operations.CheckLogin(txtUser.Text, txtPassword.Text))
            {
                MessageBox.Show("Login successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //open userform
                new UserForm(txtUser.Text).Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Bad user name or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
