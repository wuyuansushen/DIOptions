using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DIOptions
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            
            await host.RunAsync();
            
        }
        static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostOut =
             Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(
                (host_build_context, confBuilder) => {
                    confBuilder.Sources.Clear();
                    confBuilder.AddJsonFile("json1.json"); }
                );
                hostOut.ConfigureServices(
                (host_context,services) =>
                {
                    services.Configure<SettingsOptions>(host_context.Configuration.GetSection(SettingsOptions.Settings));
                    services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<SettingsOptions>, ValidateSettingOptions>());

                    services.AddOptions<SettingsOptions>().
                    Bind(host_context.Configuration.GetSection(SettingsOptions.Settings)).ValidateDataAnnotations()
                    .Validate(
                        (setOptions) => {
                            if (setOptions.Scale != 0)
                                return setOptions.VerbosityLevel > setOptions.Scale;
                            else
                                return true;
                        },@"Verbosity isn't largert than Scale."
                        );

                    var SerProvider = services.BuildServiceProvider();

                    var i = SerProvider.GetRequiredService<IOptions<SettingsOptions>>();
                    try
                    {
                        SettingsOptions options = i.Value;
                    }
                    catch (OptionsValidationException ex)
                    {
                        foreach (string exp in ex.Failures)
                        {
                            Console.WriteLine($"There is \n{exp}\n");
                        }

                    }
                }
                );
            return hostOut;

        }
    }
}
