using AFResonanceTickets;
using AFResonanceTickets.Configuration;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Program))]

namespace AFResonanceTickets
{
    public class Program : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            builder.Services.AddMediatR(typeof(Program).Assembly);

            builder.Services.AddDependencyInjectorConfiguration(configuration);
        }
    }
}
