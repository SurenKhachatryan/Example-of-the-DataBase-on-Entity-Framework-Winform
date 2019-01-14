using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDBExemple
{
    public partial class Form1 : Form
    {
        UsersDBEntities db;
        User user;

        public Form1()
        {
            InitializeComponent();
        }

        private void SelectAllUserDataButton_Click(object sender, EventArgs e)
        {
            GetAllData();
        }

        private void GetAllData()
        {
            SelectAllUserDataListBox.Items.Clear();
            using (db = new UsersDBEntities())
            {
                DbSet users = db.Users;
                foreach (User us in users)
                {
                    SelectAllUserDataListBox.Items.Add($"{us.User_Id}\t{us.Login_Name}\t" +
                                                       $"{us.Password}\t{us.Last_Name}\t" +
                                                       $"{us.First_Name}\t{us.Email}\t" +
                                                       $"{us.Phone}");
                }
            }
        }

        private void NewUserButtonSignUp_Click(object sender, EventArgs e)
        {
            using (db = new UsersDBEntities())
            {
                user = new User()
                {
                    Login_Name = NewUserTextBoxLoginName.Text,
                    Password = NewUserTextBoxPassword.Text,
                    Last_Name = NewUserTextBoxLastName.Text,
                    First_Name = NewUserTextBoxFirstName.Text,
                    Phone = NewUserTextBoxPhone.Text,
                    Email = NewUserTextBoxEmail.Text
                };
                db.Users.Add(user);
                db.SaveChanges();
            }
            GetAllData();
        }

        private void UpdateUserDataButton_Click(object sender, EventArgs e)
        {
            using (db = new UsersDBEntities())
            {
                DbSet users = db.Users;
                foreach (User us in users)
                {
                    if (us.User_Id == Convert.ToInt32(UpdateUserDateTextBoxID.Text))
                    {
                        us.Login_Name = UpdateUserDateTextBoxLoginName.Text;
                        us.Password = UpdateUserDateTextBoxPassword.Text;
                        us.Last_Name = UpdateUserDateTextBoxLastName.Text;
                        us.First_Name = UpdateUserDateTextBoxFirstName.Text;
                        us.Phone = UpdateUserDateTextBoxPhone.Text;
                        us.Email = UpdateUserDateTextBoxEmail.Text;
                        break;
                    }
                }
                db.SaveChanges();
                GetAllData();
            }
        }

        private void DeleteUserButton_Click(object sender, EventArgs e)
        {

            GetAllData();
        }

        private void UpdateUserDataButtonGetUserData_Click(object sender, EventArgs e)
        {
            using (db = new UsersDBEntities())
            {
                DbSet users = db.Users;
                foreach (User us in users)
                {
                    if (us.User_Id == Convert.ToInt32(UpdateUserDateTextBoxID.Text))
                    {
                        UpdateUserDateTextBoxLoginName.Text = us.Login_Name;
                        UpdateUserDateTextBoxPassword.Text = us.Password;
                        UpdateUserDateTextBoxLastName.Text = us.Last_Name;
                        UpdateUserDateTextBoxFirstName.Text = us.First_Name;
                        UpdateUserDateTextBoxPhone.Text = us.Phone;
                        UpdateUserDateTextBoxEmail.Text = us.Email;
                        break;
                    }
                }
            }
        }
    }
}
