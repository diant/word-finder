using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WordFinder.CLI.Commands;

var builder = new HostBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<MainCmd>());
        services.AddValidatorsFromAssemblyContaining<MainCmd>();
    });

try
{
    return await builder.RunCommandLineApplicationAsync<MainCmd>(args);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    return 1;
}