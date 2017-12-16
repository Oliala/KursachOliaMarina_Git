using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursachOliaMarina.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public string LName { get; set; }
        public string FName { get; set; }
        public string Sex { get; set; }
        public int Visit { get; set; }

        public IEnumerable<Zakaz> Zakazs { get; set; }
    }
}