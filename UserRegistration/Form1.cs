using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UserRegistration
{
    public partial class Form1 : Form
    {
        //Declare an object of UserInfoEntities
        UserInfoEntities _db;

        public Form1()
        {
            InitializeComponent();

            // Instantiate database
            _db = new UserInfoEntities();
        }

        private void submit_Click(object sender, EventArgs e)
        {
            // save user information in database
            SaveUser();
            
            MessageBox.Show("successful registration");                        
        }        

        private void SaveUser()
        {
            User user = new User();
            user.first_name = first_name.Text.Trim();
            user.last_name = last_name.Text.Trim();
            user.email = email.Text.Trim();
            user.phone = phone.Text.Trim();
            _db.Users.Add(user);
            _db.SaveChanges();
        }
            
        private void phone_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;

            if (!ValidPhoneNumber(phone.Text.Trim(), out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                phone.Select(0, phone.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorProvider1.SetError(phone, errorMsg);
            }            
        }

        public bool ValidPhoneNumber(string phoneNumber, out string errorMessage)
        {
            Regex re = new Regex("^9[0-9]{9}");

            // Confirm that the phone number string is not empty.
            if (phoneNumber.Length == 0)
            {
                errorMessage = "field is required.";
                return false;
            }

            // Confirm that phone number is valid.
            if (phoneNumber.Length == 10)
            {
                if (re.IsMatch(phoneNumber))
                {
                    errorMessage = "";
                    return true;
                }
            }            

            errorMessage = "phone number must be 10-digit and starts with 9";
            return false;
        }

        private void email_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;

            if (!ValidEmailAddress(email.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                email.Select(0, email.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorProvider1.SetError(email, errorMsg);
            }            
        }

        public bool ValidEmailAddress(string emailAddress, out string errorMessage)
        {
            // Confirm that the email address string is not empty.
            if (emailAddress.Length == 0)
            {
                errorMessage = "field is required.";
                return false;
            }

            // Confirm that there is an "@" and a "." in the email address, and in the correct order.
            if (emailAddress.IndexOf("@") > -1)
            {
                if (emailAddress.IndexOf(".", emailAddress.IndexOf("@")) > emailAddress.IndexOf("@"))
                {
                    errorMessage = "";
                    return true;
                }
            }

            errorMessage = "email address must be in valid format.\n" +
               "For example 'someone@example.com' ";
            return false;
        }

        private void last_name_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;

            // Confirm that the last name string is not empty.
            if (last_name.Text.Length == 0)
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                first_name.Select(0, last_name.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                errorMsg = "field is required!";
                errorProvider1.SetError(last_name, errorMsg);
            }
        }

        private void first_name_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;

            // Confirm that the first name string is not empty.
            if (first_name.Text.Length == 0)
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                first_name.Select(0, first_name.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                errorMsg = "field is required!";
                errorProvider1.SetError(first_name, errorMsg);
            }           
        }  
    }
}
