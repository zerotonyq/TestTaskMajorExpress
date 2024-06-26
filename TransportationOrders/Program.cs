
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using TransportationOrders.Models.Entities;
using TransportationOrders.Models.Repository;
using TransportationOrders.ViewModels;

namespace TransportationOrders
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton<App>();
                
                services.AddSingleton<OrderViewModel>();
                services.AddSingleton<OrderEditViewModel>();

                services.AddSingleton<MainWindow>();
                services.AddTransient<OrderEditWindow>();

                services.AddSingleton<ApplicationDBContext>();

                services.AddSingleton<CouriersRepository>();
                services.AddSingleton<OrdersRepository>();
                
            }).Build();
            
            var app = host.Services.GetService<App>();

            app?.Run();
        }
    }
}
