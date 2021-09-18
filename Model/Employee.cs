using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Employee
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Username { get; set; }
        public string Email { get; set; }
        public int Regi_Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Team { get; set; }
    }
}
