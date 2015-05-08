using Embed.TestApp.Model.Entities;
using Embed.TestApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Embed.TestApp
{
    public partial class UsersForm : Form, IUsersView
    {
        public UsersForm(IUserService service, IUnitOfWork unitOfWork)
        {
            InitializeComponent();

            var presentor = new UsersPresenter(this, service,unitOfWork);
        }

        public event EventHandler AddUser;
        public event EventHandler SaveOrUpdateUser;
        public event EventHandler DeleteUser;
        public event EventHandler SelectionChanged;


        public void ShowMessage(string title, string message)
        {
            MessageBox.Show(message, message);
        }

        public void ClearUserForm()
        {
            usersGrid.ClearSelection();
        }

        public void LoadUsers(IEnumerable<User> users)
        {
            usersGrid.DataSource = users.ToList();
            if (!users.Any())
                ClearUserForm();
        }

        public void LoadUser(User user)
        {
            this.Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            DateOfBirth = user.DateOfBirth;
        }

        public User GetUser()
        {
            return new User
            {
                Id = this.Id,
                FirstName = this.FirstName,
                LastName = this.LastName,
                UserName = this.UserName,
                DateOfBirth = this.DateOfBirth,
            };
        }


        public int Id
        {
            get;
            set;
        }

        public string UserName
        {
            get
            {
                return txtUserName.Text;
            }
            set
            {
                txtUserName.Text = value; 
            }
        }

        public string FirstName
        {
            get
            {
                return txtFirstName.Text;
            }
            set
            {
                txtFirstName.Text = value;
            }
        }

        public string LastName
        {
            get
            {
                return txtLastName.Text;
            }
            set
            {
                txtLastName.Text = value;
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return dtDateOfBirth.Value;
            }
            set
            {
                dtDateOfBirth.Value = value;
            }
        }

        private void UsersForm_Shown(object sender, EventArgs e)
        {
             
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if (AddUser != null)
                AddUser(sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveOrUpdateUser != null)
                SaveOrUpdateUser(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DeleteUser != null)
                DeleteUser(sender, e);

        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void usersGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (usersGrid.SelectedRows.Count > 0)
            {
                var user = (User)usersGrid.SelectedRows[0].DataBoundItem;

                this.Id = user.Id;
            }else{
                this.Id = 0;
            }


            if (SelectionChanged != null)
                SelectionChanged(sender, e);
        }

        private void usersGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
