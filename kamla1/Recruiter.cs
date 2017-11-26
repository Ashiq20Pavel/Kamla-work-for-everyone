using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework.Controls;
using System.Windows.Forms;

namespace kamla1
{
    class Recruiter : Person
    {
        Database db;
        private string designation;
        private string organisation;
        private string username;
        private string password;
        private string confPassword;

        public Recruiter()
            : base()
        {
            this.organisation = null;
            this.designation = null;
            this.username = null;
            this.password = null;
            this.confPassword = null;
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

        public string Designation
        {
            set { this.designation = value; }
            get { return this.designation; }
        }

        public string Organisation
        {
            set { this.organisation = value; }
            get { return this.organisation; }
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

        public void ShowMyPostedJobs(ListView view)
        {
            db.ShowPostedJobsFor(this, view);
        }

        public void ShowMyJobDescription(ListView box, Job job)
        {
            db.ShowJobDescription(job, box);
        }

        public void ShowApplicantForJob(Job job, ListView view)
        {
            view.Items.Clear();
            db.ShowApplicantsForJob(job, view);

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

        public Boolean HireSeekerForJob(Seeker s, Job j)
        {
            if (db.HireSeekerFor(s, j))
            {
                return true;
            }

            else return false;
        }

        public void AvailableForChat(ListView view)
        {
            db.AvailableForChat(this, view);
        }

        public void ViewMyProfile(MetroLabel name, MetroLabel username, MetroLabel email, MetroLabel dob, MetroLabel mobile, MetroLabel address, MetroLabel organisation, MetroLabel designation)
        {
            db.ViewMyProfileRecruiter(this, name, username, email, dob, mobile, address, organisation, designation);
        }

        public void ViewMyEditProfile(MetroTextBox fname, MetroTextBox lname, MetroTextBox email, MetroTextBox mobile, MetroTextBox address, MetroTextBox designation, MetroTextBox organisation)
        {
            db.ViewEditProfileRecruiter(this, fname, lname, email, mobile, address, designation, organisation);
        }

        public Boolean SaveMyEitProfile(MetroTextBox fname, MetroTextBox lname, MetroTextBox email, MetroTextBox mobile, MetroTextBox address, MetroTextBox designation, MetroTextBox organisation)
        {
            if (db.SaveEditProfileRecruiter(this, fname, lname, email, mobile, address, designation, organisation))
            {
                return true;
            }

            else return false;
        }

    }
}
