using Microsoft.Extensions.DependencyInjection;
using INV.Services.authentication;
using INV.Services.Infrastructure.authentication;
using INV.Services.Infrastructure.Masters;
using INV.Services.Masters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace INV.Core
{

    /// <summary>
    /// 
    /// </summary>
    public class Dependency
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public Dependency(IServiceCollection serviceCollection)
        {
            ResolveDependency(serviceCollection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ResolveDependency(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IItemGroupServices, ItemGroupServices>();
            services.AddTransient<IUnitTypeServices, UnitTypeService>();
            services.AddTransient<IUnitServices, UnitService>();
            services.AddTransient<ISupplierServices, SupplierServices>();
            services.AddTransient<IDepartmentServices, DepartmentServices>();
            services.AddTransient<IItemServices, ItemServices>();
            services.AddTransient<IRackServices, RackService>();
            services.AddTransient<IShelfServices, ShelfServices>();
            services.AddTransient<ITaxServices, TaxServices>();
        }
    }
}