using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kamla1
{
    class Job
    {
        Recruiter rec;
        Database db;
        Random rand;
        private int jobId;
        private string title;
        private string description;
        public List<string> reqSkills; 
        public List<string> appliedSeekerList;
        private string otherRqeuirements;
        private string workdays;
        private DateTime dateApplicationDeadline;
        private string amount;

        public Job(Recruiter rec)
        {
            rand = new Random();
            this.jobId = rand.Next(1, 999999);
            this.title = null;
            this.description = null;
            this.reqSkills = new List<string>();
            appliedSeekerList = new List<string>();
            this.otherRqeuirements = null;
            this.workdays = null;
            this.dateApplicationDeadline = DateTime.Today;
            this.amount = null;
            db = new Database();
            this.rec = rec;
        }

        public string Title
        {
            set { this.title = value; }
            get { return this.title; }
        }

        public string Description
        {
            set { this.description = value; }
            get { return this.description; }
        }

        public string OtherRequirements
        {
            set { this.otherRqeuirements = value; }
            get { return this.otherRqeuirements; }
        }

        public string Workdays
        {
            set { this.workdays = value; }
            get { return this.workdays; }
        }

        public DateTime DateApllicationDeadline
        {
            set { this.dateApplicationDeadline = value; }
            get { return this.dateApplicationDeadline; }
        }

        public string Amount
        {
            set { this.amount = value; }
            get { return this.amount; }
        }


        public Boolean ValidateJob()
        {
            if (this.title != null && this.description != null & this.reqSkills != null && this.dateApplicationDeadline != null)
            {
                if (db.InsertJob(this,rec))
                {
                    return true;
                }

                else return false;
            }

            else return false;
        }

        public int JobId
        {
            set { }
            get { return this.jobId; }
        }

        public void GetDescription(ListView box)
        {
            db.ShowJobDescription(this,box);
        }

        public Boolean AddApplicant(Job job, Seeker obj)
        {
            if (db.AddJobApplicationDetails(this, obj))
            {
                return true;
            }

            else return false;
        }
    }
}
