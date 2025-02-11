﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Echo.Model
{
    public class AccountModel
    {
        public string socialName { get; set; }
        public string serial     { get; set; }
        public string token      { get; set; }
        public string login      { get; set; }
        public int    status     { get; set; }
        public int    donate     { get; set; }
        public bool   slot_3     { get; set; }
        public bool   slot_4     { get; set; }
    }
}
