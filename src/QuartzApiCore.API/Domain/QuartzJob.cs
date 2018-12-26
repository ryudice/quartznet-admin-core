using System;
using System.Collections.Generic;
using QuartzApiCore.API.Services;

namespace QuartzApiCore.API.Domain
{
    public class QuartzJob
    {
        public String Name { get; set; }
        public string Group { get; set; }
        public IList<QuartzTrigger> Triggers { get; set; }
        public string[] Parameters { get; set; }
    }
}