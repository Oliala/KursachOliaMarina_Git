using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursachOliaMarina.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Sex { get; set; }
        public int Visits { get; set; }

        public User User { get; set; }
    }
}