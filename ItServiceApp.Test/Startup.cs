using ItServiceApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.DependencyInjection;

namespace ItServiceApp.Test
{
    class Startup
    {
        public void ConfigureServices(IServiceCollection service)
        {
            service.AddTransient<IPaymentService, IyzicoPaymentService>();
        }
    }


}
