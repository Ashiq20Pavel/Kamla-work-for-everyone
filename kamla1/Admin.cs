using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework.Controls;
using System.Windows.Forms;

namespace kamla1
{
    class Admin:Person
    {
        Database db;

        private string username;
        private string password;
        private string confPassword;
        private string gender;

        public Admin()
            : base()
        {
            this.username = null;
            this.password = null;
            this.confPassword = null;
            this.gender = null;
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
            set { this.gender = value; }
            get { return this.gender; }
        }

        public string ConfPassword
        {
           set { this.confPassword=value; }
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

        public void ShowSeekersList(ListView view)
        {
            db.ViewRegisteredSeekersList(view);
        }

        public void ShowRecruitersList(ListView view)
        {
            db.ViewRegisteredRecruitersList(view);
        }

        public void ViewReports(ListView view)
        {
            db.ShowAvailableReports(view);
        }

        public void ViewReportsDescreption(ListView view, string title)
        {
           db.ViewReport(view,title);
        }

        public Boolean AdminBlockUser(string username)
        {
            if (db.BlockUser(username))
            {
                return true;
            }

            else return false;
        }

        public Boolean AdminUnblockUser(string username)
        {
            if (db.UnblockUser(username))
            {
                return true;
            }

            else return false;
        }

    }
}
