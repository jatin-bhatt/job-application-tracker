using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application {
    public static class DependencyInjection {

        public static IServiceCollection AddApplication(this IServiceCollection service, IConfiguration configuration) {

            service.AddMediatR(o => {
                o.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            });
            return service;
        }
    }
}
