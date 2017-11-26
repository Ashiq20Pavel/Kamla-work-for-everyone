using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using MetroFramework.Controls;
using System.Windows.Forms;

namespace kamla1
{
    class Seeker : Person
    {
        Database db;
        private string username;
        private string password;
        private string confPassword;
        public List<string> skills;
        public List<string> appliedJobs;

        public Seeker()
            : base()
        {
            this.username = null;
            this.password = null;
            this.confPassword = null;
            skills = new List<string>();
            appliedJobs = new List<string>();
            db = new Database();
        }

        public string Username
        {
            set { this.username = value; }
            get { return this.username; }

        }

        public string Password
        {
            set { this.password = value; }
            get { return this.password; }
        }

        public string ConfPassword
        {
            set { this.confPassword = value; }
            get { return this.confPassword; }
        }

        public Boolean Register()
        {
            if (db.InsertNew(this))
            {
                return true;
            }

            return false;
        }

        public void GetMyinfo(MetroTextBox fname, MetroTextBox lname, MetroTextBox email, MetroTextBox mobile, MetroTextBox address, ListBox box)
        {

            db.ViewEditProfileSeeker(this, fname, lname, email, mobile, address, box);

        }

        public void MatchJobs(ListView obj)
        {
            db.MatchJobsFor(this, obj);
        }

        public void ShowMyAppliedJobs(ListView box)
        {
            db.ShowAppliedJobsFor(this, box);
        }

        public Boolean applyForJob(Job job)
        {
            if (job.AddApplicant(job, this))
            {
                return true;
            }

            else return false;
        }

        public void ViewRecruiterInfo(Recruiter rec, MetroFramework.Controls.MetroLabel name, MetroFramework.Controls.MetroLabel number)
        {
            db.ViewRecruiterInfoToSeeker(rec, name, number);

        }

        public Boolean Report(MetroTextBox title, MetroTextBox description)
        {
            if (db.Report(this, title, description))
            {
                return true;
            }

            else return false;
        }

        public Boolean SendMail(MetroTextBox to, MetroTextBox subject, MetroTextBox body)
        {
            if (db.SendMailTo(to.Text, subject.Text, body.Text, this))
            {
                return true;
            }

            else return false;

        }

        public void CheckMail(ListView view)
        {
            db.CheckForNewMail(this, view);
        }

        public Boolean DeleteMyJobApplication(Job j)
        {
            if (db.DeleteJobApplication(this, j))
            {
                return true;
            }

            else return false;
        }

        public Boolean DeleteMail(string x, string y)
        {
            if (db.DeleteMail(x, y))
            {
                return true;
            }

            else return false;
        }

        public void AvailableForChat(ListView view)
        {
            db.AvailableForChat(this, view);
        }

        public void ViewMyProfile(MetroLabel name, MetroLabel username, MetroLabel email, MetroLabel dob, MetroLabel mobile, MetroLabel address, ListBox box)
        {
            db.ViewMyProfileSeeker(this,name,username,email,dob,mobile,address,box);
        }

        public void ViewMyEditProfile(MetroTextBox fname, MetroTextBox lname, MetroTextBox email, MetroTextBox mobile, MetroTextBox address, ListBox box)
        {
           // db.ViewEditProfileSeeker(this, fname, lname, email, mobile, address, box);
        }

        public Boolean SaveMyEitProfile(MetroTextBox fname, MetroTextBox lname, MetroTextBox email, MetroTextBox mobile, MetroTextBox address, MetroTextBox designation, MetroTextBox organisation)
        {
            if (db.SaveEditProfileSeeker(this, fname, lname, email, mobile, address, designation, organisation))
            {
                return true;
            }

            else return false;
        }


    }
}
