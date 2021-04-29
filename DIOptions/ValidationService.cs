using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DIOptions
{
    public class ValidationService
    {
        private readonly IOptions<SettingsOptions> _config;
        private readonly ILogger<ValidationService> _logger;

        public ValidationService(IOptions<SettingsOptions> config,ILogger<ValidationService> logger)
        {
            _config = config;
            _logger = logger;

            try
            {
                SettingsOptions options = config.Value;
            }
            catch(OptionsValidationException ex)
            {
                foreach(string exp in ex.Failures)
                {
                    _logger.LogError(exp);
                }
            }
        }
    }
} 
