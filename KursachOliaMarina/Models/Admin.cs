﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursachOliaMarina.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public IEnumerable<Menu> Menus { get; set; }

    }
}