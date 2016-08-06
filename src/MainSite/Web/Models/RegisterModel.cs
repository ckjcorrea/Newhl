﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newhl.MainSite.Common.DomainModel;

namespace Newhl.MainSite.Web.Models
{
    /// <summary>
    /// The class defines the inputs for the Register all
    /// </summary>
    public class RegisterModel
    {
        public string EmailAddress { get; set; }
               
        public AMFUserLogin PlayerInfo { get; set; }
    }
}