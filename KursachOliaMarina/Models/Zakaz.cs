﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KursachOliaMarina.Models
{
    public class Zakaz
    {
        public int Id { get; set; }
        // не знаю что еще, надо подумать

        public int? ProfileId { get; set; }
        public UserProfile Profile { get; set; }
    }
}