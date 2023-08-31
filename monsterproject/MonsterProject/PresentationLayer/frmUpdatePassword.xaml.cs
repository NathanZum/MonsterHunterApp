using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for frmUpdatePassword.xaml
    /// </summary>
    public partial class frmUpdatePassword : Window
    {
        WPFUser _user = null;
        UserManager _userManager = null;
        bool _newUser = false;

        public frmUpdatePassword(WPFUser user, UserManager userManager, bool newUser = false)
        {
            _user = user;
            _userManager = userManager;
            _newUser = newUser;

            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string oldPassword = txtOldPassword.Password;
            string newPassword = txtNewPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;

            
            if (username == "")
            {
                MessageBox.Show("You need to enter your email.");
                txtUsername.Focus();
                txtUsername.SelectAll();
                return;
            }
            if (oldPassword == "")
            {
                MessageBox.Show("You need to enter your current password.");
                txtOldPassword.Focus();
                txtOldPassword.SelectAll();
                return;
            }
            if (newPassword == "")
            {
                MessageBox.Show("You need to enter your new password.");
                txtNewPassword.Focus();
                txtNewPassword.SelectAll();
                return;
            }
            if (newPassword == "newuser" || newPassword == oldPassword)
            {
                MessageBox.Show("Your password cannot be your old password");
                txtNewPassword.Focus();
                txtNewPassword.SelectAll();
            }
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm do not match.");
                txtNewPassword.Clear();
                txtConfirmPassword.Clear();
                txtNewPassword.Focus();
                return;
            }
            
            try
            {
                if (_userManager.ResetPassword(_user, username, newPassword, oldPassword))
                {
                    MessageBox.Show("Password changed.");
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Bad email or password.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Update Failed." + "\n\n" + ex.InnerException.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (_newUser)
            {
                var result = MessageBox.Show("If you do not set a password you will be logged out.",
                                               "Cancel Update?", MessageBoxButton.YesNo,
                                               MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    this.DialogResult = false;
                }
            }
            else
            {
                this.DialogResult = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnSubmit.IsDefault = true;

            if (_newUser)
            {
                txtInstructions.Text = "First login! You must update your password\nor be logged out.";
                txtUsername.Text = _user.UserName;
                txtUsername.IsEnabled = false;
                txtOldPassword.Password = "newuser";
                txtOldPassword.IsEnabled = false;
                txtNewPassword.Focus();
            }
            else
            {
                txtInstructions.Text = "Please fill out all fields to change your password";
                txtUsername.Focus();
            }
        }
    }
}
