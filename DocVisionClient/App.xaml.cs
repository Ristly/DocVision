using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using DocVisionClient.Services;
using DocVisionClient.ViewModels;

namespace DocVisionClient
{
 
    public partial class App : Application
    {

        public static IHost? AppHost { get; private set; }

        // через систему внедрения зависимостей получаем объект главного окна
        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient();
                    services.AddSingleton<MainWindow>();      

                    services.AddScoped<ISendService, SendService>();

                    services.AddTransient<MessageViewModel>();
                })
                .Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
