using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lecture4_azure_fn
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var kvUrl = new Uri(Environment.GetEnvironmentVariable("KeyVaultUrl"));
            var secretClient = new SecretClient(kvUrl, new DefaultAzureCredential());
            var cs = secretClient.GetSecret("connection").Value.Value;
            //builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(cs));

        }
    }
}
