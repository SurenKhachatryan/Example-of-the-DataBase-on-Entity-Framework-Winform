using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace MyDBExemple
{
    public partial class Form1 : Form
    {
        #region DB Prop
        private UsersDBEntities db;
        private User user;
        #endregion

        #region TextBoxArrays
        private TextBox[] updateUserDataTextBoxArr;
        private TextBox[] newUserTextBoxsArr;
        #endregion


        public Form1()
        {
            InitializeComponent();

            newUserTextBoxsArr = new TextBox[]
            {
                NewUserTextBoxFirstName,
                NewUserTextBoxLastName,
                NewUserTextBoxLoginName,
                NewUserTextBoxPassword,
                NewUserTextBoxEmail,
                NewUserTextBoxPhone
            };

            updateUserDataTextBoxArr = new TextBox[]
            {
                UpdateUserDataTextBoxFirstName,
                UpdateUserDataTextBoxLastName,
                UpdateUserDataTextBoxLoginName,
                UpdateUserDataTextBoxPassword,
                UpdateUserDataTextBoxEmail,
                UpdateUserDataTextBoxPhone
            };

        }

        /// <summary>
        /// Возвращает все данные всех пользователей
        /// </summary>
        private void SelectAllUserDataButton_Click(object sender, EventArgs e)
        {
            Get_All_Data();
        }

        /// <summary>
        /// Возвращает все данные о всех пользователей и добавляет в ListBox
        /// </summary>
        private void Get_All_Data()
        {
            using (db = new UsersDBEntities())
            {
                SelectAllUserDataGridView.Rows.Clear();
                DbSet users = db.Users;
                foreach (User us in users)
                {
                    SelectAllUserDataGridView.Rows.Add(new string[] { us.User_Id.ToString(), us.First_Name,
                                                                      us.Last_Name, us.Login_Name, us.Password,
                                                                      us.Email, us.Phone });
                }
            }
        }

        /// <summary>
        /// Создает нового пользователя
        /// </summary>
        private void New_User_Button_Sign_Up_Click(object sender, EventArgs e)
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
            ClearTextBoxs.Clear(newUserTextBoxsArr);
            Get_All_Data();
        }

        /// <summary>
        /// Обнавляет данные Пользователя по User_Id
        /// </summary>
        private void Update_User_Data_Button_Click(object sender, EventArgs e)
        {
            using (db = new UsersDBEntities())
            {
                int userID = Convert.ToInt32(UpdateUserDataTextBoxID.Text);
                user = db.Users
                   .Where(us => us.User_Id == userID)
                   .FirstOrDefault();

                user.Login_Name = UpdateUserDataTextBoxLoginName.Text;
                user.Password = UpdateUserDataTextBoxPassword.Text;
                user.Last_Name = UpdateUserDataTextBoxLastName.Text;
                user.First_Name = UpdateUserDataTextBoxFirstName.Text;
                user.Phone = UpdateUserDataTextBoxPhone.Text;
                user.Email = UpdateUserDataTextBoxEmail.Text;

                db.SaveChanges();
            }
            ClearTextBoxs.Clear(updateUserDataTextBoxArr);
            Get_All_Data();
        }

        /// <summary>
        /// Удоляет пользователя по User_Id
        /// </summary>
        private void Delete_User_Button_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(DeleteUserTextBoxID.Text);
            using (db = new UsersDBEntities())
            {
                user = db.Users
                    .Where(us => us.User_Id == userID)
                    .FirstOrDefault();

                db.Users.Remove(user);
                db.SaveChanges();
            }
            DeleteUserTextBoxID.Text = string.Empty;
            Get_All_Data();
        }

        /// <summary>
        /// Возвращает данные о пользователя по User_Id  
        /// </summary>
        private void Update_User_Data_Button_Get_User_Data_Click(object sender, EventArgs e)
        {
            using (db = new UsersDBEntities())
            {
                int user_ID = Convert.ToInt32(UpdateUserDataTextBoxID.Text);
                user = db.Users
                    .Where(us => us.User_Id == user_ID)
                    .FirstOrDefault();

                UpdateUserDataTextBoxLoginName.Text = user.Login_Name;
                UpdateUserDataTextBoxPassword.Text = user.Password;
                UpdateUserDataTextBoxLastName.Text = user.Last_Name;
                UpdateUserDataTextBoxFirstName.Text = user.First_Name;
                UpdateUserDataTextBoxPhone.Text = user.Phone;
                UpdateUserDataTextBoxEmail.Text = user.Email;
            }
            Get_All_Data();
        }
    }
}
