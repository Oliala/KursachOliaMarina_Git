﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursachOliaMarina.Models
{
    public class Zakaz
    {
        public int Id { get; set; }
        public string ZakazName { get; set; }

        public DateTime DateOfZakaz { get; set; }

        // не знаю что еще, надо подумать

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}