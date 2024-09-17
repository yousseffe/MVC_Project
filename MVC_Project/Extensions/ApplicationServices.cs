using Microsoft.Extensions.DependencyInjection;
using MVC_Project.BLL.Interface;
using MVC_Project.BLL.Repositories;

namespace MVC_Project.Extensions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}
