using AFResonanceTickets;
using AFResonanceTickets.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Program))]

namespace AFResonanceTickets
{
    public class Program : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //IConfiguration configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var configuration = builder.GetContext().Configuration;
            builder.Services.AddMediatR(typeof(Program).Assembly);

            builder.Services.AddDependencyInjectorConfiguration(configuration);
        }
    }
}
