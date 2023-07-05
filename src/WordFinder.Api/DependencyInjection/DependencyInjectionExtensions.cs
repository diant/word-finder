using FluentValidation;
using MediatR;
using WordFinder.Api.Middlewares;

namespace WordFinder.Api.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddWordFinder(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Program>();

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssemblyContaining<Program>();
            });
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
