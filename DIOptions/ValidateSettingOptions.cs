using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIOptions
{
    class ValidateSettingOptions : IValidateOptions<SettingsOptions>
    {
        public SettingsOptions _settingsOptions{get; private set;}

        public ValidateSettingOptions(IConfiguration conf)
        {
            _settingsOptions = conf.GetSection(SettingsOptions.Settings).Get<SettingsOptions>();
        }


        public ValidateOptionsResult Validate(string name,SettingsOptions options)
        {
            string result = "";
            var rx = new Regex(@"^[0-9a-zA-Z -'\s]{1,40}$");
            var match = rx.Match(options.SiteTitle);
            if(String.IsNullOrEmpty(match.Value))
            {
                result += $"{options.SiteTitle} don't match any";
            }
            if (options.Scale < 0 || options.Scale > 1000)
                result += $"{options.Scale} isn't in range 0-1000";
            if(_settingsOptions.Scale is 0 && _settingsOptions.VerbosityLevel<=_settingsOptions.Scale)
            {
                result += "Verbosity must be > than Scales.";
            }

            return result != null ? ValidateOptionsResult.Fail(result) : ValidateOptionsResult.Success;
        }
    }
}
