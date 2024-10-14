using AFResonanceTickets.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AFResonanceTickets
{
    public class Program : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            builder.Services.AddDependencyInjectorConfiguration(configuration);
        }
    }
}
