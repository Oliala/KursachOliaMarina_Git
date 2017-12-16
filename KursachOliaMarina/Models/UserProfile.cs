using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KursachOliaMarina.Models
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Sex { get; set; }
        public int Visit { get; set; }

        public User User { get; set; }

        public ICollection<Zakaz> Zakazs { get; set; }
        public UserProfile()
        {
            Zakazs = new List<Zakaz>();
        }
    }
}