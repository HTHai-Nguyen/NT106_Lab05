using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Lab05
{
    public partial class Ex01 : Form
    {
        public Ex01()
        {
            InitializeComponent();
        }

        private string smtp_pass = "1234qwer";
        private CheckBox chkIsHtml;
        

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(txtFrom.Text) ||
                string.IsNullOrWhiteSpace(txtTo.Text) ||
                //string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Create SMTP client for local network (internal mail)
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    // Get email details from form
                    string mailFrom = txtFrom.Text.Trim();
                    string mailTo = txtTo.Text.Trim();
                    //string password = txtPassword.Text.Trim();

                    // Set up network credentials
                    //var basicCredential = new NetworkCredential(mailFrom, password);

                    // Configure SMTP 
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("21520808@gm.edu.vn", smtp_pass) ;

                    // Create mail message
                    using (MailMessage message = new MailMessage())
                    {
                        MailAddress fromAddress = new MailAddress(mailFrom);

                        message.From = fromAddress;
                        message.Subject = txtSubject.Text.Trim();

                        // Support for HTML emails
                        message.IsBodyHtml = chkIsHtml.Checked;
                        message.Body = richTextBody.Text;

                        message.To.Add(mailTo);

                        

                        // Send email
                        smtpClient.Send(message);

                        MessageBox.Show("Email sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear form after successful send
                        
                    }
                }
            }
            catch (Exception ex)
            {
                // Detailed error handling
                MessageBox.Show($"Error sending email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            txtFrom.Clear();
            txtTo.Clear();
            txtSubject.Clear();
            richTextBody.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            new Dashboard().Show();
            this.Hide();
        }
    }
}
