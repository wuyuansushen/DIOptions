using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DIOptions
{
    public class SettingsOptions
    {
        public const string Settings = nameof(Settings);

        [RegularExpression(@"^[a-z !A-Z-']{1,40}$")]
        public string SiteTitle { get; set; }

        //[Range(0,1000,ErrorMessage ="Value for {0} must be between {1} and {2}")]
        [Range(minimum:0,maximum:1000,ErrorMessage =("Value for {0} must be between {1} and {2}."))]
        public int Scale { get; set; }

        public int VerbosityLevel { get; set; }
    }
}
