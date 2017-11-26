using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using System.Threading;
namespace kamla1
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

        Database database;
        Seeker seeker;
        Recruiter recruiter;
        Admin admin;
        string temp;
        public string seekerName;
        public string recruiterName;
        public static Thread ChatThreadSeeker;
        public static Thread ChatThreadRecruiter;

        public Form1()
        {
            InitializeComponent();
            BlankPanel.Visible = true;
            MainPanel.Visible = false;
            RecruiterSignUp.Visible = false;
            SeekerSignUp.Visible = false;
            recruiter = new Recruiter();
            seeker = new Seeker();
            database = new Database();
            admin = new Admin();
            ChatThreadSeeker = new Thread(new ThreadStart(ChatLoad));
            ChatThreadRecruiter = new Thread(new ThreadStart(ChatLoad));
        }
        void ChatLoad()
        {
            Application.Run(new ChatWindow());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BlankPanel.Visible = false;
            MainPanel.Visible = true;
            
            RecruiterSignUp.Visible = false;
            SeekerSignUp.Visible = false;
  
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void SeekerProfileLogOutBtn_Click(object sender, EventArgs e)
        {
             this.MainPanel.Visible = true;
             this.SeekerProfile.Visible = false;
        }

        private void SeekerSignInBtn_Click(object sender, EventArgs e)
        {
            seeker.FirstName = TextFirstNameS.Text;
            seeker.LastName = TextLastNameS.Text;
            seeker.Username = TextUsernameS.Text;
            seeker.ConfPassword = TextConfirmPasswordS.Text;
            seeker.Password = TextPasswordS.Text;
            seeker.DateOfBirth = DateTimeDobS.Value.Date;
            seeker.EmailAddress = TextEmailS.Text;
            seeker.MobileNumber = TextMobileS.Text;

            if (RadioMaleS.Checked == true)
            {
                seeker.Gender = RadioMaleS.Text;
            }

            else if (RadioFemaleS.Checked == true)
            {
                seeker.Gender = RadioFemaleS.Text;
            }

            seeker.Address = TextAddressS.Text;

            foreach (var i in ListBoxSkill.Items)
            {
                seeker.skills.Add(i.ToString());
            }

            if (seeker.Register())
            {
                MetroFramework.MetroMessageBox.Show(this, "Press ok for going to the login page", "Your Registration has been done Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.SeekerSignUp.Visible = false;
                this.MainPanel.Visible = true;
            }
        }
            

        private void RecruiterProfileLogOutBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterProfile.Visible = false;
            this.MainPanel.Visible = true;
           
        }

        private void RecruiterSignUpSignInBtn_Click(object sender, EventArgs e)
        {
            recruiter.FirstName = TextFirstNameR.Text;
            recruiter.LastName = TextLastNameR.Text;
            recruiter.Username = TextUsernameR.Text;
            recruiter.Password = TextPasswordR.Text;
            recruiter.ConfPassword = TextConfirmPasswordR.Text;
            recruiter.DateOfBirth = DateTimeDobR.Value.Date;
            recruiter.EmailAddress = TextEmailR.Text;
            recruiter.MobileNumber = TextMobileR.Text;

            if (RadioMaleR.Checked)
            {
                recruiter.Gender = RadioMaleR.Text;
            }

            else if (RadioFemaleR.Checked)
            {
                recruiter.Gender = RadioFemaleR.Text;
            }

            recruiter.Organisation = TextOrganisationR.Text;
            recruiter.Designation = TextDesignationR.Text;
            recruiter.Address = TextAddressR.Text;
            if (recruiter.Register())
            {
                MetroFramework.MetroMessageBox.Show(this, "Press ok for going to the login page", "Your Registration has been done Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.RecruiterSignUp.Visible = false;
                this.MainPanel.Visible = true;
            }

            else
            {
                MessageBox.Show("Register Failed!");
            }
            
           
        }

        private void AdminLoginBackBtn_Click(object sender, EventArgs e)
        {
            
            this.MainPanel.Visible = true;
        }

        private void AdminBtn_Click(object sender, EventArgs e)
        {
            this.MainPanel.Visible = false;
           
        }

        private void AdminProfileLogOutBtn_Click(object sender, EventArgs e)
        {
            this.AdminProfile.Visible = false;
            this.MainPanel.Visible = true;
            this.LoginUserNameTextBox.Text = null;
            this.LoginPasswordTextBox.Text = null;
        }

        private void AdminProfileSearchBtn_Click(object sender, EventArgs e)
        {
            this.AdminSearchPanel.Visible = true;
        }

        private void AdminSearchBackBtn_Click_1(object sender, EventArgs e)
        {
            this.AdminSearchPanel.Visible = false;
        }

        private void RecruiterSearchBackBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterSearchPanel.Visible = false;
        }

        private void RecruiterSearchBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterSearchPanel.Visible = true;
        }

        private void RecruiterProfileEditBactBtn_Click(object sender, EventArgs e)
        {
            RecruiterGayeb();
        }

        private void RecruiterProfileBtn1_Click(object sender, EventArgs e)
        {
            this.RecruiterMainProfilePanel.Visible = false;
            this.RecruiterHomePanel.Visible = false;
            this.RecruiterChangePasswordPanel.Visible = false;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = true;
            this.PostJobPanel.Visible = false;

            recruiter.Username = LoginUserNameTextBox.Text;
            recruiter.ViewMyEditProfile(ERFirst,ERLast,EREmail,ERMobile,ERAddress,ERDesignation,EROrganisation);
        }

        private void AdminProfileEditBackBtn_Click(object sender, EventArgs e)
        {
            AdminGayeb();
          
        }

        private void AdminProfileEditBtn_Click(object sender, EventArgs e)
        {
            this.AdminChangePasswordPanel.Visible = true;
            this.RecruiterListPanel.Visible = false;
            this.SeekerListPanel.Visible = false;
            this.AdminNotificationPanel.Visible = false;
            this.AdminSendMailPanel.Visible = false;
        }

        private void SeekerProfileEditBackBtn_Click(object sender, EventArgs e)
        {
            SeekerGayeb();
        }
        public void SeekerGayeb()
        {
            this.SeekerChatListPanel.Visible = false;
            this.SRecruiterProfile.Visible = false;
            this.SeekerHomePanel.Visible = true;
            this.SeekerChangePasswordPanel.Visible = false;
            this.SeekerProfileEditPanel.Visible = false;
            this.SeekerMessegeBoxPanel.Visible = false;
         
            this.SeekerSendMailPanel.Visible = false;
            this.SeekerReportPanel.Visible = false;
            this.SeekerMainProfilePanel.Visible = false;
        }

        private void SeekerProfileEditBtn_Click(object sender, EventArgs e)
        {
            this.SeekerChatListPanel.Visible = false;
            this.SRecruiterProfile.Visible = false;
            this.SeekerMainProfilePanel.Visible = false;
            this.SeekerChangePasswordPanel.Visible = false;
            this.SeekerAppliedJobsPanel.Visible = false;
            this.SeekerHomePanel.Visible = false;
            this.SeekerProfileEditPanel.Visible = true;
            this.SeekerMessegeBoxPanel.Visible = false;
          
            this.SeekerSendMailPanel.Visible = false;
            this.SeekerReportPanel.Visible = false;

            seeker.Username = LoginUserNameTextBox.Text;
            seeker.GetMyinfo(TextEditFirstNameS, TextEditLastNameS, TextEditEmailS, TextEditMobileS, TextEditAddressS, ListBoxEditSkillS);
        }

        private void SeekerSearchPanelBackBtn_Click(object sender, EventArgs e)
        {
            this.SeekerSearchPanel.Visible = false;
        }

        private void SeekerProfileSearchBtn_Click(object sender, EventArgs e)
        {
            this.SeekerSearchPanel.Visible = true;
        }

        private void AdminProfileRecruiterListBtn_Click(object sender, EventArgs e)
        {
            this.AdminChangePasswordPanel.Visible = false;
            this.RecruiterListPanel.Visible = true;
            this.SeekerListPanel.Visible = false;
            this.AdminNotificationPanel.Visible = false;
            this.AdminSendMailPanel.Visible = false;

            admin.ShowRecruitersList(ListViewRecruiterListAdmin);
          
        }

        private void RecruiterListBackBtn_Click(object sender, EventArgs e)
        {
            AdminGayeb();
        }

        private void SeekerListBackBtn_Click(object sender, EventArgs e)
        {
            AdminGayeb();
        }

        private void AdminProfileSeekerListBtn_Click(object sender, EventArgs e)
        {
            this.AdminChangePasswordPanel.Visible = false;
            this.RecruiterListPanel.Visible = false;
            this.SeekerListPanel.Visible = true;
            this.AdminNotificationPanel.Visible = false;
            this.AdminSendMailPanel.Visible = false;
            admin.ShowSeekersList(ListViewSeekerListAdmin);
        }

        private void MainPanelLoginBtn_Click(object sender, EventArgs e)
        {
            this.LoginPanel.Visible = true;
        }

        private void AdminLoginBackBtn_Click_1(object sender, EventArgs e)
        {
            this.LoginPanel.Visible = false;
        }

        private void AdminLoginBtn_Click_1(object sender, EventArgs e)
        {
            string type;
            ChatWindow.name = LoginUserNameTextBox.Text;
            if (database.Login(LoginUserNameTextBox.Text, LoginPasswordTextBox.Text, out type))
            {
                if (type.Contains("Recruiter"))
                {
                    recruiter.Username = LoginUserNameTextBox.Text;
                    MainPanel.Visible = false;
                    RecruiterProfile.Visible = true;
                    recruiter.ShowMyPostedJobs(MyPostedJob);
                }

                else if (type.Contains("Seeker"))
                {
                    seeker.Username = LoginUserNameTextBox.Text;
                    MainPanel.Visible = false;
                    SeekerProfile.Visible = true;
                    seeker.MatchJobs(ListViewAvailableJobs);
                }

                else if (type.Contains("Admin"))
                {
                    admin.Username = LoginUserNameTextBox.Text;
                    MainPanel.Visible = false;
                    AdminProfile.Visible = true;
                   
                }

            }

            else
            {
                MetroMessageBox.Show(this,"Login Failed");
            }

        }

     

        private void SeekerMsgBoxBackBtn_Click(object sender, EventArgs e)
        {
            SeekerGayeb();
            seeker.Username = LoginUserNameTextBox.Text;

            if (ListViewSeekerInbox.SelectedItems.Count>0)
           {
               if (seeker.DeleteMail(ListViewSeekerInbox.SelectedItems[0].SubItems[0].Text, ListViewSeekerInbox.SelectedItems[0].SubItems[1].Text))
                {
                MessageBox.Show("Deleted!");
                ListViewSeekerInbox.Items.Clear();
                }

                else
                {
                MessageBox.Show("Error!");
                }
           }

        }

        private void SeekerProfileBtn_Click(object sender, EventArgs e)
        {
            this.SeekerSearchPanel.Visible = false;
            this.SeekerProfileEditPanel.Visible = false;
            this.SeekerMessegeBoxPanel.Visible = false;
           
            this.SeekerSendMailPanel.Visible = false;
            this.SeekerReportPanel.Visible = false;
        }

        private void SeekerNotificationBackBtn_Click(object sender, EventArgs e)
        {
            SeekerGayeb();
        }

        private void metroTile12_Click(object sender, EventArgs e)
        {
            this.SeekerChatListPanel.Visible = false;
            this.SRecruiterProfile.Visible = false;
            this.SeekerMainProfilePanel.Visible = false;
            this.SeekerChangePasswordPanel.Visible = false;
            this.SeekerAppliedJobsPanel.Visible = false;
            this.SeekerHomePanel.Visible = false;
            this.SeekerProfileEditPanel.Visible = false;
            this.SeekerMessegeBoxPanel.Visible = false;
          
            this.SeekerSendMailPanel.Visible = false;
            this.SeekerReportPanel.Visible = false;
        }

        private void SeekerSendMailBackBtn_Click(object sender, EventArgs e)
        {
            SeekerGayeb();

            seeker.Username = LoginUserNameTextBox.Text;

            if (MailToSeeker.Text == "admin@admin" || (ComboReceiverType.SelectedIndex == 0 || ComboReceiverType.SelectedIndex == 1))
            {
                if (seeker.SendMail(MailToSeeker, MailSubjectSeeker, MailBodySeeker))
                {
                    MessageBox.Show("Mail Sent To: " + MailToSeeker.Text);
                }

                else
                {
                    MessageBox.Show("Error!");
                }
            }

            else
            {
                MessageBox.Show("Must Select Receiver User Account Type!");
            }
           

            
            

        }

        private void metroTile13_Click(object sender, EventArgs e)
        {
            this.SeekerChatListPanel.Visible = false;
            this.SRecruiterProfile.Visible = false;
            this.SeekerMainProfilePanel.Visible = false;
            this.SeekerChangePasswordPanel.Visible = false;
            this.SeekerAppliedJobsPanel.Visible = false;
            this.SeekerHomePanel.Visible = false;
            this.SeekerProfileEditPanel.Visible = false;
            this.SeekerMessegeBoxPanel.Visible = false;
           
            this.SeekerSendMailPanel.Visible = true;
            this.SeekerReportPanel.Visible = false;
        }

        private void SeekerReportBackBtn_Click(object sender, EventArgs e)
        {
            SeekerGayeb();
        }

        private void SeekerReportBtn_Click(object sender, EventArgs e)
        {
            this.SeekerChatListPanel.Visible = false;
            this.SRecruiterProfile.Visible = false;
            this.SeekerMainProfilePanel.Visible = false;
            this.SeekerChangePasswordPanel.Visible = false;
            this.SeekerAppliedJobsPanel.Visible = false;
            this.SeekerHomePanel.Visible = false;
            this.SeekerProfileEditPanel.Visible = false;
            this.SeekerMessegeBoxPanel.Visible = false;
        
            this.SeekerSendMailPanel.Visible = false;
            this.SeekerReportPanel.Visible = true;
        }

        private void SeekerProfileMessageBoxBtn_Click(object sender, EventArgs e)
        {
            this.SeekerChatListPanel.Visible = false;
            this.SRecruiterProfile.Visible = false;
            this.SeekerMainProfilePanel.Visible = false;
            this.SeekerChangePasswordPanel.Visible = false;
            this.SeekerAppliedJobsPanel.Visible = false;
            this.SeekerHomePanel.Visible = false;
            this.SeekerProfileEditPanel.Visible = false;
            this.SeekerMessegeBoxPanel.Visible = true;
       
            this.SeekerSendMailPanel.Visible = false;
            this.SeekerReportPanel.Visible = false;

            seeker.Username = LoginUserNameTextBox.Text;
            seeker.CheckMail(ListViewSeekerInbox);
        }

        private void SeekerProfileBtn1_Click(object sender, EventArgs e)
        {
            this.SeekerChatListPanel.Visible = false;
            this.SRecruiterProfile.Visible = false;
            this.SeekerChangePasswordPanel.Visible = false;
            this.SeekerAppliedJobsPanel.Visible = false;
            this.SeekerHomePanel.Visible = false;
            this.SeekerProfileEditPanel.Visible = false;
            this.SeekerMessegeBoxPanel.Visible = false; 
            this.SeekerSendMailPanel.Visible = false;
            this.SeekerReportPanel.Visible = false;
            this.SeekerMainProfilePanel.Visible = true;

            seeker.Username = LoginUserNameTextBox.Text;
            seeker.ViewMyProfile(SeekerName, SeekerUserName, SeekerEmail, SeekerDob, SeekerMobile, SeekerAddress, SeekerSkillListBox);


        }

        private void RecruiterNotificationBoxBackBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSearchPanel.Visible = false;
        }

        private void RecruiterMessageBoxBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterMainProfilePanel.Visible = false;
            this.RecruiterHomePanel.Visible = false;
            this.RecruiterChangePasswordPanel.Visible = false;
            this.RecruiterMessageBoxPanel.Visible = true;
            this.RecruiterSendMailPanel.Visible = false;
         
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = false;
            this.PostJobPanel.Visible = false;
        }

        private void RecruiterNotificationBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterMainProfilePanel.Visible = false;
            this.RecruiterHomePanel.Visible = false;
            this.RecruiterChangePasswordPanel.Visible = false;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
           
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = false;
            this.PostJobPanel.Visible = false;
           
        }

        private void NotificationBackBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterHomePanel.Visible = true;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
           
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = false;
        }

        //private void RecruiterSendMailBtn1_Click(object sender, EventArgs e)
        //{
        //    this.RecruiterHomePanel.Visible = false;
        //    this.RecruiterChangePasswordPanel.Visible = false;
        //    this.RecruiterMessageBoxPanel.Visible = false;
        //    this.RecruiterSendMailPanel.Visible = true;
        //    this.RecruiterNotificationPanel1.Visible = false;
        //    this.RecruiterReportPanel.Visible = false;
        //    this.RecruiterProfileEditPanel.Visible = false;
        //    this.PostJobPanel.Visible = false;
        //}

        private void RecruiterMessageBoxBackBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterSendMailPanel.Visible = false;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSearchPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
        }

        private void RecruiterNotificationBackBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSearchPanel.Visible = false;
           
            this.RecruiterNotificationPanel.Visible = false;
            this.RecruiterReportPanel.Visible = false;
        }

        private void RecruiterProfileBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterSearchPanel.Visible = false;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
          
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = false;
            this.PostJobPanel.Visible = false;
            
        }

        private void RecruiterReportBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterMainProfilePanel.Visible = false;
            this.RecruiterHomePanel.Visible = false;
            this.RecruiterChangePasswordPanel.Visible = false;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
           
            this.RecruiterReportPanel.Visible = true;
            this.RecruiterProfileEditPanel.Visible = false;
            this.PostJobPanel.Visible = false;

        }

        private void RecruiterBackBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterHomePanel.Visible = true;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
          
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = false;
        }

        private void RecruiterMsgBoxBackBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterHomePanel.Visible = true;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
            this.RecruiterNotificationPanel.Visible = false;
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = false;
        }

        private void RecruterNotificaitonBackBtn1_Click(object sender, EventArgs e)
        {
            this.RecruiterHomePanel.Visible = true;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
           
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = false;
        }

        private void metroTile24_Click(object sender, EventArgs e)
        {
            this.AdminSearchPanel.Visible = false;
            this.AdminChangePasswordPanel.Visible = false;
            this.RecruiterListPanel.Visible = false;
            this.SeekerListPanel.Visible = false;
            this.AdminNotificationPanel.Visible = false;
            this.AdminSendMailPanel.Visible = false;
           
        }

        private void AdminNotificationBtn_Click(object sender, EventArgs e)
        {
            this.AdminChangePasswordPanel.Visible = false;
            this.RecruiterListPanel.Visible = false;
            this.SeekerListPanel.Visible = false;
            this.AdminNotificationPanel.Visible = true;
            this.AdminSendMailPanel.Visible = false;

            admin.ViewReports(ListViewReport);
        }

        private void AdminSendMailBtn_Click(object sender, EventArgs e)
        {
            this.AdminChangePasswordPanel.Visible = false;
            this.RecruiterListPanel.Visible = false;
            this.SeekerListPanel.Visible = false;
            this.AdminNotificationPanel.Visible = false;
            this.AdminSendMailPanel.Visible = true;
        }

        private void AdminSendMailBackBtn_Click(object sender, EventArgs e)
        {
            this.AdminChangePasswordPanel.Visible = false;
            this.RecruiterListPanel.Visible = false;
            this.SeekerListPanel.Visible = false;
            this.AdminNotificationPanel.Visible = false;
            this.AdminSendMailPanel.Visible = false;
        }

        private void AdminNotificationBackBtn_Click(object sender, EventArgs e)
        {
            AdminGayeb();
        }

        private void RecruiterProfileBtn2_Click(object sender, EventArgs e)
        {
            this.RecruiterMainProfilePanel.Visible = true;
            this.RecruiterChangePasswordPanel.Visible = false;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = false;
            this.RecruiterHomePanel.Visible = false;
            recruiter.Username = LoginUserNameTextBox.Text;
            recruiter.ViewMyProfile(RecruiterName, RecruiterUserName, RecruiterEmail, RecruiterDob, RecruiterMobile, RecruiterAddress, RecruiterOrganisation, RecruiterDesignation);
         
        }

        private void metroTile25_Click(object sender, EventArgs e)
        {
            this.SeekerSignUp.Visible = true;
            this.MainPanel.Visible = false;
        }

        private void RecruiterSignUpB_Click(object sender, EventArgs e)
        {
            this.RecruiterSignUp.Visible = true;
            this.MainPanel.Visible = false;
        }
        int t1 = 41;
        int t2 = 41;
        private void SignUpBtn1_MouseHover(object sender, EventArgs e)
        {
            this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t2);
            timer1.Start();
        }

        private void SignUpBtn1_MouseLeave(object sender, EventArgs e)
        {
            timer1.Stop();
            t1 = 41;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (t1 > 126)
            {
                timer1.Stop();
            }
            else
            {
                this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t1);
                t1 += 5;
            }
        }

        private void Form1_MouseHover(object sender, EventArgs e)
        {
            this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t2);
        }

        private void MainPanel_MouseHover(object sender, EventArgs e)
        {
            this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t2);
        }

        private void RecruiteSignUpBackBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterSignUp.Visible = false;
            this.MainPanel.Visible = true;
        }

        private void SeekerSignUpBackBtn_Click(object sender, EventArgs e)
        {
            this.MainPanel.Visible = true;
            this.SeekerSignUp.Visible = false;
        }

        private void LoginPanel_MouseHover(object sender, EventArgs e)
        {
            this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t2);
        }

        private void MainDescriptionPanel_MouseHover(object sender, EventArgs e)
        {
            this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t2);
        }

        private void metroTile20_MouseHover(object sender, EventArgs e)
        {
            this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t2);
        }

        private void LoginUserNameTextBox_MouseHover(object sender, EventArgs e)
        {
            this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t2);
        }

        private void LoginPasswordTextBox_MouseHover(object sender, EventArgs e)
        {
            this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t2);
        }

        private void AdminLoginBackBtn_MouseHover(object sender, EventArgs e)
        {
            this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t2);

        }

        private void AdminLoginBtn_MouseHover(object sender, EventArgs e)
        {
            this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t2);

        }

        private void MainPanelLoginBtn_MouseHover(object sender, EventArgs e)
        {
            this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t2);

        }

        private void metroPanel11_MouseHover(object sender, EventArgs e)
        {
            this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t2);

        }

        private void metroPanel14_MouseHover(object sender, EventArgs e)
        {
            this.SignUpPanel.Size = new Size(this.SignUpPanel.Size.Width, t2);

        }

        private void PostJobBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterMainProfilePanel.Visible = false;
            this.RecruiterHomePanel.Visible = false;
            this.RecruiterChangePasswordPanel.Visible = false;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
            
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = false;
            this.PostJobPanel.Visible = true;
            
        }

        private void PostJobPanelBackBtn_Click(object sender, EventArgs e)
        {
            RecruiterGayeb();
        }

        private void RecruiterPostJobBtn_Click(object sender, EventArgs e)
        {
            recruiter.Username = LoginUserNameTextBox.Text;
            Job job = new Job(recruiter);

            foreach (var i in ListViewRequiredSkills.Items)
            {
                job.reqSkills.Add(i.ToString());
            }

            job.Title = JobTitleTextBox.Text;
            job.Description = JobDescriptionTextBox.Text;
            job.Workdays = WorkDaysTextBox.Text;
            job.DateApllicationDeadline = DateTimeDeadline.Value.Date;
            job.Amount = PaymentAmountTextBox.Text;

            if (job.ValidateJob())
            {
                MetroFramework.MetroMessageBox.Show(this, "Please wait for the admin approval", "Your Request for job post has beet sent successfully to the admin.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {

                MetroFramework.MetroMessageBox.Show(this, "Error", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void metroTile19_Click(object sender, EventArgs e)
        {
            this.MainPanel.Visible = false;
            this.RecruiterProfile.Visible = true;
        }

        private void SeekerHomeBtn_Click(object sender, EventArgs e)
        {
            this.SeekerChatListPanel.Visible = false;
            this.SRecruiterProfile.Visible = false;
            this.SeekerMainProfilePanel.Visible = false;
            this.SeekerChangePasswordPanel.Visible = false;
            this.SeekerHomePanel.Visible = true;
            this.SeekerAppliedJobsPanel.Visible = false;
            this.SeekerProfileEditPanel.Visible = false;
            this.SeekerMessegeBoxPanel.Visible = false;
          
            this.SeekerSendMailPanel.Visible = false;
            this.SeekerReportPanel.Visible = false;
            seeker.Username = LoginUserNameTextBox.Text;
            seeker.MatchJobs(ListViewAvailableJobs);
        }

        private void SeekerApplyJobBtn_Click(object sender, EventArgs e)
        {
            Job j = new Job(new Recruiter());
            j.Title = ListViewAvailableJobs.SelectedItems[0].Text;
            seeker.Username = LoginUserNameTextBox.Text;
            if (seeker.applyForJob(j))
            {
                MetroFramework.MetroMessageBox.Show(this, "Application Done" ,"Applied For Job: " + j.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Already Applied", "Already Applied For The Job: " + j.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Seekerbtn2_Click(object sender, EventArgs e)
        {
            this.MainPanel.Visible = false;
            this.SeekerProfile.Visible = true;
        }

        private void AppliedJobsBtn_Click(object sender, EventArgs e)
        {
            this.SeekerChatListPanel.Visible = false;
            this.SRecruiterProfile.Visible = false;
            this.SeekerMainProfilePanel.Visible = false;
            this.SeekerChangePasswordPanel.Visible = false;
            this.SeekerAppliedJobsPanel.Visible = true;
            this.SeekerHomePanel.Visible = false;
            this.SeekerProfileEditPanel.Visible = false;
            this.SeekerMessegeBoxPanel.Visible = false;
           
            this.SeekerSendMailPanel.Visible = false;
            this.SeekerReportPanel.Visible = false;
            seeker.Username = LoginUserNameTextBox.Text;
            seeker.ShowMyAppliedJobs(ListViewAppliedJobs);

        }

        private void SeekerAppliedJobsBackBtn_Click(object sender, EventArgs e)
        {
            this.SeekerAppliedJobsPanel.Visible = false;
            this.SeekerHomePanel.Visible = true;
            this.SeekerProfileEditPanel.Visible = false;
            this.SeekerMessegeBoxPanel.Visible = false;
           
            this.SeekerSendMailPanel.Visible = false;
            this.SeekerReportPanel.Visible = false;
        }

        private void SeekerDeleteJobBtn_Click(object sender, EventArgs e)
        {
            Job job = new Job(new Recruiter());
            job.Title = ListViewAppliedJobs.SelectedItems[0].Text;
            seeker.Username = LoginUserNameTextBox.Text;
            if (seeker.DeleteMyJobApplication(job))
            {   
                MetroFramework.MetroMessageBox.Show(this, "", "Job Application Deleted Successfully.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                seeker.ShowMyAppliedJobs(ListViewAppliedJobs);
            }

            else 
            {
                MetroFramework.MetroMessageBox.Show(this, "", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void SeekerReportPanelBtn1_Click(object sender, EventArgs e)
        {
            seeker.Username=LoginUserNameTextBox.Text;

            if (seeker.Report(ReportTitleSeeker, ReportDescriptionSeeker))
            {
                MetroFramework.MetroMessageBox.Show(this, "Soon the Action will be taken.", "Report Submited Succefully to the Admin Panel.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("Error");
            }
           
        }

        private void SeekerProfileEditBtn1_Click(object sender, EventArgs e)
        {
            MetroFramework.MetroMessageBox.Show(this, "", "Profile Edited Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SeekerChangePasswordBtn1_Click(object sender, EventArgs e)
        {
            seeker.Username = LoginUserNameTextBox.Text;
            if (database.ChangePasswordFor(seeker, SeekerOldPassword, SeekerNewPassword))
            {
                MetroFramework.MetroMessageBox.Show(this, "", "Password Has been Changed Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MetroFramework.MetroMessageBox.Show(this, "", "OOPS!! Some Parameters Mismatched, Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void SeekerChangePasswordBtn_Click(object sender, EventArgs e)
        {
            this.SeekerChatListPanel.Visible = false;
            this.SRecruiterProfile.Visible = false;
            this.SeekerMainProfilePanel.Visible = false;
            this.SeekerChangePasswordPanel.Visible = true;
            this.SeekerAppliedJobsPanel.Visible = false;
            this.SeekerHomePanel.Visible = false;
            this.SeekerProfileEditPanel.Visible = false;
            this.SeekerMessegeBoxPanel.Visible = false;
           
            this.SeekerSendMailPanel.Visible = false;
            this.SeekerReportPanel.Visible = false;
        }

        private void SeekerChangePasswordBackBtn_Click(object sender, EventArgs e)
        {
            SeekerGayeb();
        }

        private void RecruiterChangePasswordBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterMainProfilePanel.Visible = false;
            this.RecruiterHomePanel.Visible = false;
            this.RecruiterChangePasswordPanel.Visible = true;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
          
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = false;
            this.PostJobPanel.Visible = false;
        }

        private void RecruiterChangePasswordBackBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterHomePanel.Visible = true;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
          
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = false;
            this.PostJobPanel.Visible = false;
        }

        private void RecruiterChangePasswordBtn1_Click(object sender, EventArgs e)
        {
            recruiter.Username = LoginUserNameTextBox.Text;
            if (database.ChangePasswordFor(recruiter, TextBoxOldPassRec, TextBoxNewPassRec))
            {
                MetroFramework.MetroMessageBox.Show(this, "", "Password Has been Changed Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MetroFramework.MetroMessageBox.Show(this, "", "OOPS!! Some Parameters Mismatched, Please Try Again", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void RecruiterEditProfileBtn_Click(object sender, EventArgs e)
        {
            recruiter.Username = LoginUserNameTextBox.Text;

            if (recruiter.SaveMyEitProfile(ERFirst, ERLast, EREmail, ERMobile, ERAddress, ERDesignation, EROrganisation))
            {
                MetroFramework.MetroMessageBox.Show(this, "", "Profile Edited Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MetroFramework.MetroMessageBox.Show(this, "", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void RecruiterHomeBtn_Click(object sender, EventArgs e)
        {
            this.RecruiterMainProfilePanel.Visible = false;
            this.RecruiterHomePanel.Visible = true;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
     
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = false;
            this.PostJobPanel.Visible = false;
        }

        private void AdminChangePasswordBtn_Click(object sender, EventArgs e)
        {
            admin.Username = LoginUserNameTextBox.Text;
            if (database.ChangePasswordFor(admin, AdminPreviousPasswordTextBox, AdminPasswordTextBox))
            {
                MessageBox.Show("Password Changed");
            }

            else
            {
                MessageBox.Show("Error!");
            }
        }

        private void AdminAddAdminBtn_Click(object sender, EventArgs e)
        {
            this.CreateAdminPanel.Visible = true;
            this.AdminChangePasswordPanel.Visible = false;
            this.RecruiterListPanel.Visible = false;
            this.SeekerListPanel.Visible = false;
            this.AdminNotificationPanel.Visible = false;
            this.AdminSendMailPanel.Visible = false;
        }
        private void AdminGayeb()
        {
            this.CreateAdminPanel.Visible = false;
            this.AdminChangePasswordPanel.Visible = false;
            this.RecruiterListPanel.Visible = false;
            this.SeekerListPanel.Visible = false;
            this.AdminNotificationPanel.Visible = false;
            this.AdminUnapprovedJobsPanel.Visible = false;
            this.AdminBlockedUserPanel.Visible = false;
        }

        private void AddAdminBackBtn_Click(object sender, EventArgs e)
        {
            AdminGayeb();
        }

        private void TextConfirmPasswordR_Leave(object sender, EventArgs e)
        {
            database.MatchPassword(TextPasswordR, TextConfirmPasswordR);
        }

        private void TextConfirmPasswordS_Leave(object sender, EventArgs e)
        {
            database.MatchPassword(TextPasswordS, TextConfirmPasswordS);
        }

        private void SeekerSignUpAddBtn_Click(object sender, EventArgs e)
        {
            ListBoxSkill.Items.Add(TextSkillS.Text);
            TextSkillS.Clear();
        }

  

        private void AddEdit_Click(object sender, EventArgs e)
        {
            seeker.Username=LoginUserNameTextBox.Text;

            database.AddSkillToTable(seeker, TextEditSkillS);
            ListBoxEditSkillS.Items.Add(TextEditSkillS.Text);
            TextEditSkillS.Clear();
        }

        private void ButtonAddRequiredSkills_Click(object sender, EventArgs e)
        {
            ListViewRequiredSkills.Items.Add(TextBoxSkillRequired.Text);
            TextBoxSkillRequired.Clear();
        }

        //private void RecruiterMyPostedJobsButton_Click(object sender, EventArgs e)
        //{
        //    this.RecruiterHomePanel.Visible = false;
        //    this.RecruiterChangePasswordPanel.Visible = false;
        //    this.RecruiterMessageBoxPanel.Visible = false;
        //    this.RecruiterSendMailPanel.Visible = false;
        //    this.RecruiterNotificationPanel1.Visible = false;
        //    this.RecruiterReportPanel.Visible = false;
        //    this.RecruiterProfileEditPanel.Visible = false;
        //    this.PostJobPanel.Visible = false;
        //}

        private void MyPostedJob_SelectedIndexChanged(object sender, EventArgs e)
        {
            recruiter.Username = LoginUserNameTextBox.Text;
            Job job = new Job(recruiter);

            if (MyPostedJob.SelectedItems.Count > 0)
            {
                job.Title = MyPostedJob.SelectedItems[0].Text;
                this.temp = MyPostedJob.SelectedItems[0].Text;
                recruiter.ShowApplicantForJob(job, ApplicantList);
            }

        }

        private void SeekerConfirmNewPassword_Leave(object sender, EventArgs e)
        {
            if (!database.MatchPassword(SeekerNewPassword, SeekerConfirmNewPassword))
            {
                MetroMessageBox.Show(this, "Confirm Password Mismatched");
                SeekerConfirmNewPassword.Clear();
            }

            else
            { 
                
            }
        }

        private void ListViewAvailableJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            Job job = new Job(new Recruiter());
            if (ListViewAvailableJobs.SelectedItems.Count > 0)
            {
                ListViewAvailableJobsDescription.Clear();
                job.Title = ListViewAvailableJobs.SelectedItems[0].Text;
                job.GetDescription(ListViewAvailableJobsDescription);
            }
        }

        private void SeekerShowRecruiterProfileBtn_Click(object sender, EventArgs e)
        {
            recruiter.Username = ListViewAvailableJobs.SelectedItems[0].SubItems[1].Text;
            MessageBox.Show(recruiter.Username);
            //seeker.ViewRecruiterInfo(recruiter);
        }

        private void ComboReceiverType_DropDownClosed(object sender, EventArgs e)
        {
            if (database.ReceiverEmailValidation(MailToSeeker, ComboReceiverType))
            {

            }

            else
            {
                MessageBox.Show("Email not registered as KAMLA user");
                MailToSeeker.Clear();
            }
        }

        private void ListViewSeekerInbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewSeekerInboxOpen.Clear();

            if (ListViewSeekerInbox.SelectedItems.Count > 0)
            {
                database.ViewMailDescription(ListViewSeekerInbox.SelectedItems[0].SubItems[0].Text, ListViewSeekerInbox.SelectedItems[0].SubItems[1].Text, ListViewSeekerInboxOpen);
            }
            
        }

        private void ListViewAppliedJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewAppliedJobsDescription.Clear();

            Job job = new Job(new Recruiter());
            if (ListViewAppliedJobs.SelectedItems.Count > 0)
            {
                ListViewAppliedJobsDescription.Clear();
                job.Title = ListViewAppliedJobs.SelectedItems[0].Text;
                job.GetDescription(ListViewAppliedJobsDescription);
            }
        }

        private void AdminConfirmPasswordTextBox_Leave(object sender, EventArgs e)
        {
            if (!database.MatchPassword(AdminPasswordTextBox, AdminConfirmPasswordTextBox))
            {
                MessageBox.Show("ERROR");
                AdminConfirmPasswordTextBox.Clear();
            }

            else
            { 
            
            }
        }

        private void ListViewReports_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //ListViewReport.Clear();

            //admin.ViewReportsDescreption(ListViewReport, ListViewReports.SelectedItems[0].Text);
        }

        private void ButtonBlockSeeker_Click(object sender, EventArgs e)
        {
            if (admin.AdminBlockUser(ListViewSeekerListAdmin.SelectedItems[0].SubItems[1].Text))
            {
                MessageBox.Show("Blocked: " + ListViewSeekerListAdmin.SelectedItems[0].Text);
            }

            else
            {
                MessageBox.Show("Error!");
            }
        }

        private void AdminRecruiterBlockBtn_Click(object sender, EventArgs e)
        {
            if (admin.AdminBlockUser(ListViewRecruiterListAdmin.SelectedItems[0].SubItems[1].Text))
            {
                MessageBox.Show("Blocked: " + ListViewRecruiterListAdmin.SelectedItems[0].Text);
            }

            else
            {
                MessageBox.Show("Error!");
            }
        }

        private void ButtonReportRecruiter_Click(object sender, EventArgs e)
        {
            recruiter.Username = LoginUserNameTextBox.Text;

            if (recruiter.Report(ReportTitleRecruiter, ReportRecruiterDescription))
            {
                MetroFramework.MetroMessageBox.Show(this, "Soon the Action will be taken.", "Report Submited Succefully to the Admin Panel.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("Error");
            }
        }

        private void ComboTypeRec_DropDownClosed(object sender, EventArgs e)
        {
            if (database.ReceiverEmailValidation(MailToRecruiter, ComboTypeRec))
            {

            }

            else
            {
                MessageBox.Show("Email not registered as KAMLA user");
                MailToRecruiter.Clear();
            }

        }

        private void SendEmailRecruiter_Click(object sender, EventArgs e)
        {
            recruiter.Username = LoginUserNameTextBox.Text;

            if (recruiter.SendMail(MailToRecruiter, MailSubjectRecruiter, MailBodyRecruiter))
            {
                MessageBox.Show("Mail Sent To: " + MailToRecruiter.Text);
            }

            else
            {
                MessageBox.Show("Error!");
            }
        }


        private void RecruiterGayeb()
        {
            this.RecruiterChatListPanel.Visible = false;
            this.RSeekerProfilePanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
           
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterHomePanel.Visible = true;
            this.RecruiterProfileEditPanel.Visible = false;
            this.RecruiterChangePasswordPanel.Visible = false;
            this.PostJobPanel.Visible = false;
            this.RecruiterMainProfilePanel.Visible = false;
        }
        private void RecruiterMessageBox_Click(object sender, EventArgs e)
        {
            RecruiterGayeb();
            this.RecruiterHomePanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = true;
            this.RecruiterMainProfilePanel.Visible = false;
            recruiter.Username = LoginUserNameTextBox.Text;
            recruiter.CheckMail(ListViewInboxRec);
        }

        private void ListViewInboxRec_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ListViewInboxRec_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ListViewDetailedInboxRec.Clear();
            if (ListViewInboxRec.SelectedItems.Count > 0)
            {
                database.ViewMailDescription(ListViewInboxRec.SelectedItems[0].Text, ListViewInboxRec.SelectedItems[0].SubItems[1].Text, ListViewDetailedInboxRec);
            }
            
           
        }

        private void TextBoxConfPassRec_Leave(object sender, EventArgs e)
        {
            if (!database.MatchPassword(TextBoxNewPassRec, TextBoxConfPassRec))
            {
                MetroMessageBox.Show(this, "Confirm Password Mismatched");
                TextBoxConfPassRec.Clear();
            }

            else
            {

            }
        }

        private void ButtonUnapprovedJobs_Click(object sender, EventArgs e)
        {
           
            this.AdminChangePasswordPanel.Visible = false;
            this.RecruiterListPanel.Visible = false;
            this.SeekerListPanel.Visible = false;
            this.AdminNotificationPanel.Visible = false;
            this.AdminSendMailPanel.Visible = false;
            this.AdminUnapprovedJobsPanel.Visible = true;
            database.ViewUnapprovedJobs(ListViewUnapprovedJobs);
           
        }

        private void UnapprovedJobsBackBtn_Click(object sender, EventArgs e)
        {
            AdminGayeb();
        }

        private void UnapprovedJobsApproveBtn_Click(object sender, EventArgs e)
        {
            if (ListViewUnapprovedJobs.SelectedItems.Count > 0)
            {
                Job job = new Job(new Recruiter());
                job.Title = ListViewUnapprovedJobs.SelectedItems[0].Text;

                if (database.ApproveJob(job))
                {
                    MessageBox.Show("Job Approved");
                    database.ViewUnapprovedJobs(ListViewUnapprovedJobs);
                }

                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void AdminBlockedUsers_Click(object sender, EventArgs e)
        {
            AdminGayeb();
            this.AdminBlockedUserPanel.Visible = true;
            database.ViewBlockedUser(ListViewBlockedUsers);

        }

        private void AdminBlockedUserBackBtn_Click(object sender, EventArgs e)
        {
            AdminGayeb();

            if (ListViewBlockedUsers.SelectedItems.Count > 0)
            {
                if (database.UnblockUser(ListViewBlockedUsers.SelectedItems[0].Text))
                {
                    MessageBox.Show("User: " + ListViewBlockedUsers.SelectedItems[0].Text + " Unblocked");
                }

                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void MyPostedJobsBtn_Click(object sender, EventArgs e)
        {
            recruiter.Username = LoginUserNameTextBox.Text;
            recruiter.ShowMyPostedJobs(MyPostedJob);

            this.RecruiterMainProfilePanel.Visible = false;
            this.RecruiterHomePanel.Visible = true;
            this.RecruiterMessageBoxPanel.Visible = false;
            this.RecruiterSendMailPanel.Visible = false;
          
            this.RecruiterReportPanel.Visible = false;
            this.RecruiterProfileEditPanel.Visible = false;
            this.PostJobPanel.Visible = false;
        }

        private void ButtonApproveApplicant_Click(object sender, EventArgs e)
        {
            if (ApplicantList.SelectedItems.Count > 0)
            {
                seeker.Username = ApplicantList.SelectedItems[0].SubItems[1].Text;
                recruiter.Username = LoginUserNameTextBox.Text;
                Job job = new Job(recruiter);
                job.Title = temp;

                if (recruiter.HireSeekerForJob(seeker, job))
                {
                    MessageBox.Show("Hired: " + ApplicantList.SelectedItems[0].Text);
                }

                else
                {
                    MessageBox.Show("Error");
                }
            
            }
            

        }

        private void SeekerProfileBackBtn_Click(object sender, EventArgs e)
        {
            SeekerGayeb();
        }

        private void RecruiterProfileBackBtn_Click(object sender, EventArgs e)
        {
            RecruiterGayeb();
        }

        private void SeekerApliedJobsRecruiterProfileBtn_Click(object sender, EventArgs e)
        {
            this.SRecruiterProfile.Visible = true;
            this.SeekerMainProfilePanel.Visible = false;
            this.SeekerChangePasswordPanel.Visible = false;
            this.SeekerHomePanel.Visible = false;
            this.SeekerAppliedJobsPanel.Visible = false;
            this.SeekerProfileEditPanel.Visible = false;
            this.SeekerMessegeBoxPanel.Visible = false;
           
            this.SeekerSendMailPanel.Visible = false;
            this.SeekerReportPanel.Visible = false;

           
        }

        private void SRecruiterProfileBackBtn_Click(object sender, EventArgs e)
        {
            SeekerGayeb();
        }

        private void metroTile28_Click(object sender, EventArgs e)
        {
            this.SeekerChatListPanel.Visible = true;
            this.SRecruiterProfile.Visible = false;
            this.SeekerMainProfilePanel.Visible = false;
            this.SeekerChangePasswordPanel.Visible = false;
            this.SeekerHomePanel.Visible = false;
            this.SeekerAppliedJobsPanel.Visible = false;
            this.SeekerProfileEditPanel.Visible = false;
            this.SeekerMessegeBoxPanel.Visible = false;
            this.SeekerSendMailPanel.Visible = false;
            this.SeekerReportPanel.Visible = false;

            seeker.AvailableForChat(ListViewAvailableChatSeeker);
          
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            SeekerGayeb();
        }

        private void ViewProfileApplicant_Click(object sender, EventArgs e)
        {
            RecruiterGayeb();
            this.RSeekerProfilePanel.Visible = true;
          
            this.RecruiterHomePanel.Visible = false;
        }

        private void RSeekerProfileBackBtn_Click(object sender, EventArgs e)
        {
            RecruiterGayeb();
        }

        private void RecruiterChatListBtn_Click(object sender, EventArgs e)
        {
            RecruiterGayeb();
            this.RecruiterChatListPanel.Visible = true;
         
            this.RecruiterHomePanel.Visible = false;

            recruiter.Username = LoginUserNameTextBox.Text;
            recruiter.AvailableForChat(ListViewAvailableChatRecruiter);
        }

        private void RecruiterChatListBackBtn_Click(object sender, EventArgs e)
        {
            RecruiterGayeb();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            RecruiterGayeb();
        }

        private void ButtonChatNowSeeker_Click_1(object sender, EventArgs e)
        {
            if (ListViewAvailableChatSeeker.SelectedItems.Count > 0)
            {
                ChatWindow.other = ListViewAvailableChatSeeker.SelectedItems[0].SubItems[1].Text;

                ChatThreadSeeker.Start();
            }

            
        }

        private void ChatNowButtonRecruiter_Click_1(object sender, EventArgs e)
        {
            if (ListViewAvailableChatRecruiter.SelectedItems.Count > 0)
            {
                ChatWindow.other = ListViewAvailableChatRecruiter.SelectedItems[0].SubItems[1].Text;
                ChatThreadRecruiter.Start();
            }

            

           
        }

        private void ButtonStartServer_Click(object sender, EventArgs e)
        {
            Thread x = new Thread(new ThreadStart(LoadServer));
            x.Start();
        }

        private void LoadServer()
        {
            Application.Run(new Server.Server());
        }

        private void RemoveSkills_Click(object sender, EventArgs e)
        {
            seeker.Username=LoginUserNameTextBox.Text;
            database.RemoveSkillFromTable(seeker, ListBoxEditSkillS);
            ListBoxEditSkillS.Items.Remove(ListBoxEditSkillS.SelectedItem);
        }

        private void ButtonReportDelete_Click(object sender, EventArgs e)
        {
            database.DeleteReport(ListViewReport);
        }


     


        

      

       

    }
}
