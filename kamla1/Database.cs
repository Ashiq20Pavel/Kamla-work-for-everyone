using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MetroFramework.Controls;
using System.Windows.Forms;

namespace kamla1
{
    class Database
    {
        LINQDataContext db;

        public Database()
        {
            db = new LINQDataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=H:\Study\C#\Code\Demo-8\kamla1\KamlaDB.mdf;Integrated Security=True");
            
        }

        public Boolean InsertNew(Object any)
        {
            if (any is Recruiter)
            {
                Recruiter obj = (Recruiter)any;
                TabRecruiter rec = new TabRecruiter();

                rec.fname = obj.FirstName;
                rec.lname = obj.LastName;
                rec.uname = obj.Username;
                rec.mobile = obj.MobileNumber;
                rec.email = obj.EmailAddress;
                rec.address = obj.Address;
                rec.designation = obj.Designation;
                rec.organisation = obj.Organisation;
                rec.dob = obj.DateOfBirth;
                rec.gender = obj.Gender;

                db.TabRecruiters.InsertOnSubmit(rec);
                db.SubmitChanges();

                if (this.InserLogin(obj))
                {
                    return true;
                }

                else return false;
            }

            else if (any is Seeker)
            {
                Seeker obj = (Seeker)any;

                TabSeeker seek = new TabSeeker();

                seek.fname = obj.FirstName;
                seek.lname = obj.LastName;
                seek.uname = obj.Username;
                seek.mobile = obj.MobileNumber;
                seek.email = obj.EmailAddress;
                seek.address = obj.Address;
                seek.dob = obj.DateOfBirth;
                seek.gender = obj.Gender;

                db.TabSeekers.InsertOnSubmit(seek);
                db.SubmitChanges();

                foreach (var i in obj.skills)
                {
                    db.TabSkills.InsertOnSubmit(new TabSkill() { uname = obj.Username + obj.skills.IndexOf(i), skill = i, user = obj.Username });
                }

                db.SubmitChanges();

                if (this.InserLogin(obj))
                {
                    return true;
                }

                else return false;
            }


            return false;
        }

        public Boolean InserLogin(Object any)
        {
            if (any is Recruiter)
            {
                Recruiter obj = (Recruiter)any;
                Login log = new Login();

                log.uname = obj.Username;
                log.pass = obj.Password;
                log.type = Convert.ToString(typeof(Recruiter));
                log.status = "valid";

                db.Logins.InsertOnSubmit(log);
                db.SubmitChanges();

                return true;
            }

            else if (any is Seeker)
            {
                Seeker obj = (Seeker)any;

                Login log = new Login();

                log.uname = obj.Username;
                log.pass = obj.Password;
                log.type = Convert.ToString(typeof(Seeker));
                log.status = "valid";

                db.Logins.InsertOnSubmit(log);
                db.SubmitChanges();

                return true;
            }

            else return false;

        }

        public Boolean Login(string username, string password, out string type)
        {
            var v = from a in db.Logins
                    where username == a.uname && password == a.pass
                    select a;

            try
            {
                Login log = v.First();

                if (log != null)
                {
                    type = log.type;
                    return true;
                }

                else
                {
                    type = "";
                    return false;
                }
            }

            catch (Exception)
            {
                type = "";
                return false;
            }

        }

        public Boolean InsertJob(Job obj, Recruiter rec)
        {
            if (obj != null && rec != null && CheckUserStatus(rec))
            {
                TabJob job = new TabJob();
                job.JId = obj.JobId;
                job.PostedBy = rec.Username;
                job.JTitle = obj.Title;
                job.JDescription = obj.Description;
                job.JWorkDay = obj.Workdays;
                job.JApplicationDeadline = obj.DateApllicationDeadline;
                job.JAmount = obj.Amount;

                db.TabJobs.InsertOnSubmit(job);
                db.SubmitChanges();

                foreach (var i in obj.reqSkills)
                {
                    db.TabJobSkills.InsertOnSubmit(new TabJobSkill() { PostedBy = rec.Username + "-" + obj.JobId.ToString() + "-" + obj.reqSkills.IndexOf(i), JSkillRequiremets = i, JId = obj.JobId });
                }

                db.SubmitChanges();

                return true;
            }

            else return false;
        }

        public void MatchJobsFor(Seeker obj, ListView view)
        {
            view.Items.Clear();

            List<Object> list = new List<Object>();

            var v = from reqSkills in db.TabJobSkills
                    join availableSkills in db.TabSkills
                    on reqSkills.JSkillRequiremets equals availableSkills.skill
                    where availableSkills.uname.Contains(obj.Username.ToString())
                    select new { JId = reqSkills.JId };


            foreach (var item in v)
            {
                list.Add(item.JId);
            }

            var p = from job in db.TabJobs
                    //select new { Title = job.JTitle, Id = job.JId, PostedBy = job.PostedBy , ApplicationDeadline=job.JApplicationDeadline};
                    where job.status != null
                    select job;

            foreach (var item in p)
            {

                System.Windows.Forms.ListViewItem lv = new System.Windows.Forms.ListViewItem(item.JTitle);
                lv.SubItems.Add(item.PostedBy);
                lv.SubItems.Add(item.JApplicationDeadline.ToShortDateString());
                view.Items.Add(lv);

            }

        }

        public void ShowJobDescription(Job job, ListView box)
        {
            box.Items.Clear();

            var v = from j in db.TabJobs
                    where j.JTitle == job.Title
                    select j;

            var p = from r in db.TabRecruiters
                    where r.uname == v.First().PostedBy
                    select r;

            box.Items.Add("Job Title: " + v.First().JTitle);
            box.Items.Add("Job Description: " + v.First().JDescription);
            box.Items.Add("Application Deadline: " + v.First().JApplicationDeadline);
            box.Items.Add("Payment Amount: " + v.First().JAmount);
            box.Items.Add("Contact Email: " + p.First().email);
            box.Items.Add("Contact Mobile: " + p.First().mobile);
            box.Items.Add("Organisation: " + p.First().organisation);
        }

        public Boolean AddJobApplicationDetails(Job job, Seeker sek)
        {
            if (job != null && sek != null && CheckUserStatus(sek))
            {
                var v = from m in db.TabJobApplications
                        where m.JId == this.GetJobIdByTitle(job.Title) && m.Applicant == sek.Username
                        select m;

                var p = from q in db.TabJobs
                        where q.JId == Convert.ToInt32(GetJobIdByTitle(job.Title))
                        select q;

                if (v.Count() == 0)
                {
                    TabJobApplication application = new TabJobApplication();
                    application.JId = this.GetJobIdByTitle(job.Title);
                    application.Applicant = sek.Username;
                    application.JTitle = job.Title;
                    application.PostedBy = p.First().PostedBy;



                    db.TabJobApplications.InsertOnSubmit(application);
                    db.SubmitChanges();

                    return true;

                }

                else return false;

            }

            else return false;

        }

        public string GetJobIdByTitle(string title)
        {
            var v = from id in db.TabJobs
                    where id.JTitle == title
                    select id.JId;

            return Convert.ToString(v.First());
        }

        public void ShowPostedJobsFor(Recruiter rec, ListView view)
        {
            view.Items.Clear();

            if (rec != null)
            {
                string temp="";

                var v = from j in db.TabJobs
                        where j.PostedBy == rec.Username
                        select j;

                var p = from s in db.TabJobApplications
                        where s.JId == GetJobIdByTitle(v.First().JTitle)
                        select s;

                if (p == null)
                {
                    temp = p.First().Applicant; 
                }

               
                foreach (var item in v)
                {
                    System.Windows.Forms.ListViewItem lv = new System.Windows.Forms.ListViewItem(item.JTitle);
                    lv.SubItems.Add(item.JApplicationDeadline.ToShortDateString());
                    lv.SubItems.Add(temp);
                    view.Items.Add(lv);
                    
                }

            }

        }

        public void ViewRecruiterInfoToSeeker(Recruiter rec, MetroFramework.Controls.MetroLabel name, MetroFramework.Controls.MetroLabel number)
        {
            var v = from r in db.TabRecruiters
                    where r.uname == rec.Username
                    select r;

            foreach (var re in v)
            {
                name.Text = re.fname + re.lname;
                number.Text = re.mobile;

            }
        }

        public Boolean MatchPassword(MetroFramework.Controls.MetroTextBox Password, MetroFramework.Controls.MetroTextBox ConfirmPassword)
        {
            if (Password.Text.Equals(ConfirmPassword.Text))
            {
                return true;
            }

            else
            {
                ConfirmPassword.Clear();
                return false;
            }
        }

        public void ShowAppliedJobsFor(Seeker seeker, ListView view)
        {
            view.Items.Clear();

            if (seeker != null)
            {
                var v = from j in db.TabJobApplications
                        where j.Applicant == seeker.Username
                        select j;

                foreach (var item in v)
                {
                    ListViewItem lv = new ListViewItem(item.JTitle);
                    lv.SubItems.Add(item.PostedBy);
                    lv.SubItems.Add(item.Status);

                    view.Items.Add(lv);
                }
            }

            else { view.Items.Add("Empty"); }
        }

        public void ShowApplicantsForJob(Job job, ListView view)
        {
            view.Items.Clear();

            var v = from applicant in db.TabJobApplications
                    where applicant.JId == this.GetJobIdByTitle(job.Title)
                    select applicant.Applicant;

            var u = from seeker in db.TabSeekers
                    where seeker.uname == v.First().ToString()
                    select seeker;

            if (u != null && v != null)
            {
                try 
                {
                    string name = u.First().fname + " " + u.First().lname;
                    string username = v.First().ToString();
                    string email = u.First().email;

                    foreach (var item in v)
                    {
                        ListViewItem lv = new ListViewItem(name);
                        lv.SubItems.Add(username);
                        lv.SubItems.Add(email);
                        view.Items.Add(lv);
                    }
                }

                catch(Exception)
                {
                
                }
                
            }

            
        }

        public Boolean ChangePasswordFor(Person obj, MetroTextBox oldPass, MetroTextBox newPass)
        {
            if (obj != null)
            {
                Login login;

                if (obj is Recruiter)
                {
                    Recruiter rec = (Recruiter)obj;

                    var v = from a in db.Logins
                            where a.uname == rec.Username && a.pass == oldPass.Text
                            select a;

                    if (v.Count() == 1)
                    {
                        login = v.First();

                        login.uname = rec.Username;
                        login.pass = newPass.Text;

                        db.SubmitChanges();
                    }

                    return true;

                }

                else if (obj is Seeker)
                {
                    Seeker seek = (Seeker)obj;

                    var v = from a in db.Logins
                            where a.uname == seek.Username && a.pass == oldPass.Text
                            select a;

                    if (v.Count() == 1)
                    {
                        login = v.First();

                        login.uname = seek.Username;
                        login.pass = newPass.Text;

                        db.SubmitChanges();
                    }

                    return true;

                }

                else if (obj is Admin)
                {
                    Admin admin = (Admin)obj;

                    var v = from a in db.Logins
                            where a.uname == admin.Username && a.pass == oldPass.Text
                            select a;

                    if (v.Count() == 1)
                    {
                        login = v.First();

                        login.uname = admin.Username;
                        login.pass = newPass.Text;

                        db.SubmitChanges();
                    }

                    return true;
                }
            }
            return false;

        }

        public Boolean Report(Person obj, MetroTextBox title, MetroTextBox description)
        {
            if (obj != null)
            {
                TabReport report = new TabReport();

                if (obj is Recruiter)
                {
                    Recruiter rec = (Recruiter)obj;

                    report.ReportTitle = title.Text;
                    report.ReportDescription = description.Text;
                    report.ReportedBy = rec.Username;
                    report.ReporterType = "Recruiter";

                    db.TabReports.InsertOnSubmit(report);
                    db.SubmitChanges();

                    return true;

                }

                else if (obj is Seeker)
                {
                    Seeker seek = (Seeker)obj;

                    report.ReportTitle = title.Text;
                    report.ReportDescription = description.Text;
                    report.ReportedBy = seek.Username;
                    report.ReporterType = "Seeker";

                    db.TabReports.InsertOnSubmit(report);
                    db.SubmitChanges();

                    return true;

                }
            }

            return false;
        }

        public Boolean ReceiverEmailValidation(MetroTextBox email, MetroComboBox box)
        {
            if (email.Text == "admin@admin")
            {
                return true;
            }

            if (email != null && box.SelectedIndex == 0)
            {
                var v = from a in db.TabRecruiters
                        where a.email == email.Text
                        select a;

                if (v.Count() == 1)
                {
                    return true;
                }

                else return false;
            }

            else if (email != null && box.SelectedIndex == 1)
            {
                var v = from a in db.TabSeekers
                        where a.email == email.Text
                        select a;

                if (v.Count() == 1)
                {
                    return true;
                }

                else return false;
            }



            else
            {
                return false;
            }

        }

        public Boolean SendMailTo(string to, string subject, string body, Person by)
        {
            if (body != null)
            {
                TabMail mail = new TabMail();

                if (by is Seeker)
                {
                    Seeker seek = (Seeker)by;

                    mail.Sender = this.GetMailAddress(seek);
                    mail.Receiver = to;
                    mail.Subject = subject;
                    mail.Body = body;

                    db.TabMails.InsertOnSubmit(mail);
                    db.SubmitChanges();

                    return true;
                }

                else if (by is Recruiter)
                {
                    Recruiter rec = (Recruiter)by;

                    mail.Sender = this.GetMailAddress(rec);
                    mail.Receiver = to;
                    mail.Subject = subject;
                    mail.Body = body;

                    db.TabMails.InsertOnSubmit(mail);
                    db.SubmitChanges();

                    return true;
                }

                else if (by is Admin)
                {
                    mail.Sender = "admin@admin";
                    mail.Receiver = to;
                    mail.Subject = subject;
                    mail.Body = body;

                    db.TabMails.InsertOnSubmit(mail);
                    db.SubmitChanges();

                    return true;
                }

                else return false;
            }


            else return false;
        }

        public string GetMailAddress(Person p)
        {
            if (p is Seeker)
            {
                Seeker s = (Seeker)p;

                var v = from a in db.TabSeekers
                        where s.Username == a.uname
                        select a.email;

                return v.First().ToString();
            }

            else if (p is Recruiter)
            {
                Recruiter r = (Recruiter)p;

                var v = from a in db.TabRecruiters
                        where r.Username == a.uname
                        select a.email;

                return v.First().ToString();
            }

            else return null;

        }

        public void CheckForNewMail(Person p, ListView view)
        {
            view.Items.Clear();

            if (p is Recruiter)
            {
                Recruiter r = (Recruiter)p;

                var v = from mail in db.TabMails
                        where mail.Receiver == GetMailAddress(r)
                        select mail;

                foreach (var item in v)
                {
                    ListViewItem lv = new ListViewItem(item.Subject);
                    lv.SubItems.Add(item.Sender);
                    view.Items.Add(lv);
                }
            }

            else if (p is Seeker)
            {
                Seeker s = (Seeker)p;

                var v = from mail in db.TabMails
                        where mail.Receiver == GetMailAddress(s)
                        select mail;

                foreach (var item in v)
                {
                    ListViewItem lv = new ListViewItem(item.Subject);
                    lv.SubItems.Add(item.Sender);
                    view.Items.Add(lv);
                }
            }
        }

        public void ViewMailDescription(string subject, string sender, ListView view)
        {
            view.Items.Clear();

            var v = from mail in db.TabMails
                    where mail.Id == GetMailId(subject, sender)
                    select mail;

            foreach (var item in v)
            {
                view.Items.Add("From: " + item.Sender);
                view.Items.Add("Subject: " + item.Subject);
                view.Items.Add("Description: " + item.Body);
            }
        }

        public int GetMailId(string subject, string sender)
        {
            var v = from id in db.TabMails
                    where id.Subject == subject && id.Sender == sender
                    select id.Id;
            var a = v.First();
            return Convert.ToInt32(a);
        }

        public Boolean DeleteJob(Job job)
        {
            if (job != null)
            {
                var v = from j in db.TabJobs
                        where j.JId == Convert.ToInt32(GetJobIdByTitle(job.Title))
                        select j;

                var w = from k in db.TabJobApplications
                        where k.JId == GetJobIdByTitle(job.Title)
                        select k;

                var x = from l in db.TabJobSkills
                        where l.JId == Convert.ToInt32(GetJobIdByTitle(job.Title))
                        select l;

                if (v.Count() == 1)
                {
                    TabJob tab = v.First();
                    db.TabJobs.DeleteOnSubmit(tab);

                    if (x != null)
                    {
                        foreach (var item in x)
                        {
                            TabJobSkill tabSkill = item;
                            db.TabJobSkills.DeleteOnSubmit(tabSkill);
                        }
                    }

                    if (w != null)
                    {
                        foreach (var item in w)
                        {
                            TabJobApplication tabApp = item;
                            db.TabJobApplications.DeleteOnSubmit(tabApp);
                        }
                    }

                    db.SubmitChanges();
                    return true;
                }

                else return false;
            }

            else return false;



        }

        public Boolean DeleteJobApplication(Seeker s, Job j)
        {
            if (s != null && j != null)
            {
                var v = from application in db.TabJobApplications
                        where application.Applicant == s.Username && application.JId == GetJobIdByTitle(j.Title)
                        select application;

                TabJobApplication app = v.First();

                db.TabJobApplications.DeleteOnSubmit(app);
                db.SubmitChanges();

                return true;
            }

            else return false;

        }

        public Boolean CheckUserStatus(Person p)
        {
            if (p is Recruiter)
            {
                Recruiter r = (Recruiter)p;

                var v = from status in db.Logins
                        where status.uname == r.Username
                        select status.status;

                if (v.First().ToString() == "valid")
                {
                    return true;
                }

                else return false;
            }

            else if (p is Seeker)
            {
                Seeker s = (Seeker)p;

                var v = from status in db.Logins
                        where status.uname == s.Username
                        select status.status;

                if (v.First().ToString() == "valid")
                {
                    return true;
                }

                else return false;
            }

            else return false;


        }

        public Boolean CheckJobApproval(Job job)
        {
            if (job != null)
            {
                var v = from j in db.TabJobs
                        where j.JId == Convert.ToInt32(GetJobIdByTitle(job.Title))
                        select j;

                if (v.First().status == "Approved")
                {
                    return true;
                }

                else return false;
            }

            else return false;
        }

        public void ViewRegisteredSeekersList(ListView view)
        {
            view.Items.Clear();

            var v = from list in db.TabSeekers
                    select list;

            foreach (var item in v)
            {
                ListViewItem lv = new ListViewItem(item.fname + " " + item.lname);
                lv.SubItems.Add(item.uname);
                lv.SubItems.Add(item.email);

                view.Items.Add(lv);
            }

        }

        public void ViewRegisteredRecruitersList(ListView view)
        {
            view.Items.Clear();

            var v = from list in db.TabRecruiters
                    select list;

            foreach (var item in v)
            {
                ListViewItem lv = new ListViewItem(item.fname + " " + item.lname);
                lv.SubItems.Add(item.uname);
                lv.SubItems.Add(item.email);

                view.Items.Add(lv);
            }

        }

        public void ShowAvailableReports(ListView view)
        {
            view.Items.Clear();

            var v = from reports in db.TabReports
                    select reports;

            foreach (var item in v)
            {
                ListViewItem lv = new ListViewItem(item.ReportTitle);
                lv.SubItems.Add(item.ReporterType + " - " + item.ReportedBy);
                lv.SubItems.Add(item.ReportDescription);
                view.Items.Add(lv);

            }
        }

        public void ViewReport(ListView view, string title)
        {
            view.Items.Clear();

            var v = from report in db.TabReports
                    where report.Id == GetReportIdByTitle(title)
                    select report;

            foreach (var item in v)
            {
                view.Items.Add("Title: " + item.ReportTitle);
                view.Items.Add("Descreption: " + item.ReportDescription);
            }

        }

        public void DeleteReport(ListView view)
        {
            var v = from reports in db.TabReports
                    where reports.Id == GetReportIdByTitle(view.SelectedItems[0].Text)
                    select reports;

            TabReport tab = v.First();

            db.TabReports.DeleteOnSubmit(tab);
            db.SubmitChanges();

            ShowAvailableReports(view);
        }

        public int GetReportIdByTitle(string title)
        {
            var v = from report in db.TabReports
                    where report.ReportTitle == title
                    select report.Id;

            return Convert.ToInt32(v.First());
        }

        public Boolean BlockUser(string title)
        {
            if (title != null)
            {
                var v = from user in db.Logins
                        where user.uname == title
                        select user;

                Login log = v.First();

                log.status = "invalid";
                db.SubmitChanges();

                if (log.type.Contains("Recruiter"))
                {
                    var u = from r in db.TabRecruiters
                            where r.uname == title
                            select r.email;

                    SendMailTo(u.First().ToString(), "Blocked!", "You are temporarily blocked by admin. You can not post any new Jobs! Please contact admin@admin", new Admin());
                }

                else if (log.type.Contains("Seeker"))
                {
                    var u = from r in db.TabSeekers
                            where r.uname == title
                            select r.email;

                    SendMailTo(u.First().ToString(), "Blocked!", "You are temporarily blocked by admin. You can not Apply for Jobs! Please contact admin@admin", new Admin());


                }



                return true;
            }

            else return false;
        }

        public void ViewBlockedUser(ListView view)
        {
            view.Items.Clear();
            var v = from user in db.Logins
                    where user.status == "invalid"
                    select user;

            foreach (var item in v)
            {
                ListViewItem lv = new ListViewItem(item.uname);
                lv.SubItems.Add(item.type);
                view.Items.Add(lv);
            }

        }

        public Boolean UnblockUser(string title)
        {
            if (title != null)
            {
                var v = from user in db.Logins
                        where user.uname == title
                        select user;

                Login log = v.First();

                log.status = "valid";
                db.SubmitChanges();

                if (log.type.Contains("Recruiter"))
                {
                    var u = from r in db.TabRecruiters
                            where r.uname == title
                            select r.email;

                    SendMailTo(u.First().ToString(), "Unblocked!", "You are unblocked now thank you for your patience :)", new Admin());
                }

                else if (log.type.Contains("Seeker"))
                {
                    var u = from r in db.TabSeekers
                            where r.uname == title
                            select r.email;

                    SendMailTo(u.First().ToString(), "Unblocked!", "You are unblocked now thank you for your patience :)", new Admin());


                }

                return true;
            }

            else return false;
        }

        public void ViewUnapprovedJobs(ListView view)
        {
            view.Items.Clear();
            var v = from job in db.TabJobs
                    where job.status == null
                    select job;

            foreach (var item in v)
            {
                ListViewItem lv = new ListViewItem(item.JTitle);
                lv.SubItems.Add(item.PostedBy);

                view.Items.Add(lv);
            }
        }

        public Boolean ApproveJob(Job job)
        {
            if (job != null)
            {
                var v = from j in db.TabJobs
                        where j.JId == Convert.ToInt32(GetJobIdByTitle(job.Title))
                        select j;

                TabJob tab = v.First();
                tab.status = "approved";

                db.SubmitChanges();

                return true;
            }

            else return false;
        }

        public Boolean DeleteMail(string subject, string sender)
        {
            if (subject != null && sender != null)
            {
                var v = from mail in db.TabMails
                        where mail.Subject == subject && mail.Sender == sender
                        select mail;

                TabMail m = v.First();

                db.TabMails.DeleteOnSubmit(m);
                db.SubmitChanges();

                return true;
            }

            else return false;
        }

        public Boolean HireSeekerFor(Seeker s, Job j)
        {
            if (s != null && j != null)
            {
                var v = from job in db.TabJobApplications
                        where job.Applicant == s.Username && job.JId == GetJobIdByTitle(j.Title)
                        select job;

                TabJobApplication tab = v.First();

                tab.Status = "Hired";

                db.SubmitChanges();

                var w = from sek in db.TabSeekers
                        where sek.uname == s.Username
                        select sek;

                SendMailTo(w.First().email, "Congratulations!", "You have been seleced for the job: " + j.Title, new Admin());

                return true;
            }

            else return false;
        }

       public void AvailableForChat(Person p, ListView view)
       {
           view.Items.Clear();
           if (p is Recruiter)
           { 
               Recruiter r = (Recruiter)p;

               var v = from a in db.TabJobApplications
                       where a.PostedBy == r.Username && a.Status!=null
                       select a;

               

               foreach (var item in v)
               { 
                   var q = from b in db.TabSeekers
                       where b.uname==v.First().Applicant
                       select b;

                   var w = from c in db.TabJobs
                           where c.JId.ToString() == item.JId
                           select c;

                   ListViewItem lv = new ListViewItem(q.First().fname + " " + q.First().lname);
                   lv.SubItems.Add(item.Applicant);
                   lv.SubItems.Add(w.First().JTitle);

                   view.Items.Add(lv);
               }
           }

               else if(p is Seeker)
               {
                   Seeker s = (Seeker)p;

                   var v = from a in db.TabJobApplications
                           where a.Applicant == s.Username && a.Status!=null
                           select a;

                   foreach (var item in v)
                   {
                       var q = from b in db.TabRecruiters
                               where b.uname == v.First().PostedBy
                               select b;

                       var w = from c in db.TabJobs
                               where c.JId.ToString() == item.JId
                               select c;

                       ListViewItem lv = new ListViewItem(q.First().fname + " " + q.First().lname);
                       lv.SubItems.Add(item.PostedBy);
                       lv.SubItems.Add(w.First().JTitle);

                       view.Items.Add(lv);
                   }

               
               
               }

           }

       public void ViewMyProfileRecruiter(Recruiter r, MetroLabel name, MetroLabel username, MetroLabel email, MetroLabel dob, MetroLabel mobile, MetroLabel address, MetroLabel organisation, MetroLabel designation)
       {  
                var v = from a in db.TabRecruiters
                        where a.uname == r.Username
                        select a;

                name.Text = v.First().fname + " " + v.First().lname;
                username.Text = v.First().uname;
                email.Text = v.First().email;
                dob.Text = v.First().dob.ToShortDateString();
                mobile.Text = v.First().mobile;
                address.Text = v.First().address;
                organisation.Text = v.First().organisation;
                designation.Text = v.First().designation;

       }

       public void ViewEditProfileRecruiter(Recruiter r, MetroTextBox fname, MetroTextBox lname, MetroTextBox email, MetroTextBox mobile, MetroTextBox address, MetroTextBox designation, MetroTextBox organisation)
       {
           var v = from a in db.TabRecruiters
                   where a.uname == r.Username
                   select a;

           TabRecruiter tab = v.First();

           fname.Text = tab.fname;
           lname.Text = tab.lname;
           email.Text = tab.email;
           mobile.Text = tab.mobile;
           address.Text = tab.address;
           designation.Text = tab.designation;
           organisation.Text = tab.organisation;

       }

       public Boolean SaveEditProfileRecruiter(Recruiter r, MetroTextBox fname, MetroTextBox lname, MetroTextBox email, MetroTextBox mobile, MetroTextBox address, MetroTextBox designation, MetroTextBox organisation)
       {
           if (r != null)
           {
               var v = from a in db.TabRecruiters
                       where a.uname == r.Username
                       select a;

               TabRecruiter tab = v.First();

               tab.fname = fname.Text;
               tab.lname = lname.Text;
               tab.email = email.Text;
               tab.mobile = mobile.Text;
               tab.address = address.Text;
               tab.designation = designation.Text;
               tab.organisation = organisation.Text;

               db.SubmitChanges();

               return true;
           }

           else return false;

       }

       public void ViewMyProfileSeeker(Seeker r, MetroLabel name, MetroLabel username, MetroLabel email, MetroLabel dob, MetroLabel mobile, MetroLabel address, ListBox box)
       {
           box.Items.Clear();
           var v = from a in db.TabSeekers
                   where a.uname == r.Username
                   select a;

           name.Text = v.First().fname + " " + v.First().lname;
           username.Text = v.First().uname;
           email.Text = v.First().email;
           dob.Text = v.First().dob.ToShortDateString();
           mobile.Text = v.First().mobile;
           address.Text = v.First().address;

           var p = from b in db.TabSkills
                   where b.user==r.Username
                   select b;

           foreach (var item in p)
           {
               box.Items.Add(item.skill);
           }
            
       }

       public void ViewEditProfileSeeker(Seeker r, MetroTextBox fname, MetroTextBox lname, MetroTextBox email, MetroTextBox mobile, MetroTextBox address, ListBox box)
       {
           box.Items.Clear();
           var v = from a in db.TabSeekers
                   where a.uname == r.Username
                   select a;

           TabSeeker tab = v.First();

           fname.Text = tab.fname;
           lname.Text = tab.lname;
           email.Text = tab.email;
           mobile.Text = tab.mobile;
           address.Text = tab.address;

           var p = from b in db.TabSkills
                   where b.user == r.Username
                   select b;

           foreach (var item in p)
           {
               box.Items.Add(item.skill);
           }



       }

       public Boolean SaveEditProfileSeeker(Seeker r, MetroTextBox fname, MetroTextBox lname, MetroTextBox email, MetroTextBox mobile, MetroTextBox address, MetroTextBox designation, MetroTextBox organisation)
       {
           if (r != null)
           {
               var v = from a in db.TabSeekers
                       where a.uname == r.Username
                       select a;

               TabSeeker tab = v.First();

               tab.fname = fname.Text;
               tab.lname = lname.Text;
               tab.email = email.Text;
               tab.mobile = mobile.Text;
               tab.address = address.Text;

               db.SubmitChanges();

               return true;
           }

           else return false;

       }

       public void RemoveSkillFromTable(Seeker s,ListBox box)
       {
           var v = from skill in db.TabSkills
                   where skill.user == s.Username && skill.skill == box.SelectedItem
                   select skill;

           TabSkill tab = v.First();

           db.TabSkills.DeleteOnSubmit(tab);
           db.SubmitChanges();
       
       }

       public void AddSkillToTable(Seeker s, MetroTextBox box)
       { 
           TabSkill skill = new TabSkill();

           skill.uname=s.Username;
           skill.skill=box.Text;
           skill.user=s.Username;

           db.TabSkills.InsertOnSubmit(skill);
           db.SubmitChanges();
       }

       


       }

    }
       

