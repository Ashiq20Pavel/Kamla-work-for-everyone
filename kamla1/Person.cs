using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kamla1
{
    class Person
    {
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string emailAddress;
        private string mobileNumber;
        private string gender;
        private string address;

        public Person()
        {
            this.firstName = null;
            this.lastName = null;
            this.dateOfBirth = DateTime.Today;
            this.emailAddress = null;
            this.mobileNumber = null;
            this.gender = null;
            this.address = null;
        }

       

        public string FirstName
        {
            set { this.firstName = value; }
            get { return this.firstName; }
        }

        public string LastName
        {
            set { this.lastName = value; }
            get { return this.lastName; }
        }

        public DateTime DateOfBirth
        {
            set { this.dateOfBirth = value; }
            get { return this.dateOfBirth; }
        }

        public string EmailAddress
        {
            set { this.emailAddress = value; }
            get { return this.emailAddress; }
        }

        public string MobileNumber
        {
            set { this.mobileNumber = value; }
            get { return this.mobileNumber; }
        }

        public string Gender
        {
            set { this.gender = value; }
            get { return this.gender; }
        }

        public string Address
        {
            set { this.address = value; }
            get { return this.address; }
        }

    }
}
