﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCApp.Models
{
    public class Banka
    {
        public long Id { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public string Swift { get; set; }
    }
}