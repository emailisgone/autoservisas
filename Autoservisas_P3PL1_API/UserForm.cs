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
    public partial class UserForm : Form
    {
        private string userName;
        List<Client> clients;
        Cars clientCars;
        public UserForm(string userName)
        {
            InitializeComponent();
            this.userName = userName;
            //background task
            Task.Run(async delegate () 
            {
                clients = await Operations.GetClientList();
                foreach (var item in clients)
                {
                    lstUsers.Invoke(new MethodInvoker(() => 
                    { 
                        lstUsers.Items.Add(item.FullName + " " + item.Email);
                    }));
                }
            });
        }

        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void GetClientCars()
        {
            if (lstUsers.SelectedIndex == -1) return;
            //get client id
            var id = clients[lstUsers.SelectedIndex].Id;
            lstCars.Items.Clear();
            Task.Run(async delegate ()
            {
                clientCars = new Cars();
                clientCars.AllCars = await Operations.GetClientCars(id);
                lstCars.Invoke(new MethodInvoker(delegate ()
                {
                    foreach (var item in clientCars.AllCars)
                    {
                        lstCars.Items.Add(item.Model + " " + item.Licence);
                    }
                }));
            });
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetClientCars();
        }

        private void lstCars_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCars.SelectedIndex == -1) return;
            int index = lstCars.SelectedIndex;
            txtName.Text = clientCars.AllCars[index].Name;
            txtModel.Text = clientCars.AllCars[index].Model;
            txtYear.Text = clientCars.AllCars[index].Year.ToString();
            txtLicence.Text = clientCars.AllCars[index].Licence;
            txtEngine.Text = clientCars.AllCars[index].Engine.ToString();
            txtFuel.Text = clientCars.AllCars[index].Fuel;
            txtPower.Text = clientCars.AllCars[index].Power.ToString();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstCars.SelectedIndex == -1) return;

            if(await Operations.DeleteClientCar(clientCars.AllCars[lstCars.SelectedIndex].Licence))
            {
                MessageBox.Show("Car deleted");
                GetClientCars();
            }
        }

        private async void btnNew_Click(object sender, EventArgs e)
        {
            if (lstUsers.SelectedIndex == -1)
            {
                MessageBox.Show("Select client first");
                return;
            }
            var car = new ClientCar()
            {
                Name = txtName.Text,
                Model = txtModel.Text,
                Year = Convert.ToUInt16(txtYear.Text),
                Licence = txtLicence.Text,
                Engine = Convert.ToDecimal(txtEngine.Text),
                Fuel = txtFuel.Text,
                Power = Convert.ToUInt16(txtPower.Text)
            };
            if(await Operations.CreateNewClientCar(car, clients[lstUsers.SelectedIndex].Id))
            {
                MessageBox.Show("Car added.");
                GetClientCars();
            }
        }
    }
}
