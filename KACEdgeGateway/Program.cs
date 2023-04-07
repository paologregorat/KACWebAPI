


//string connection = "Server=127.0.0.1;Port=5432;Userid=postgres;Password=password;Database=KVM";
using EdgeGateway.Infrastructure.System;
using EdgeGateway.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using Newtonsoft.Json;
using KACGatewayContextLibrary.Domain;
using KACGatewayContextLibrary.Domain.Entity;


var statusSystem = StatusSystem.GetInstance();
var directory = AppDomain.CurrentDomain.BaseDirectory;
var file = Path.Combine(directory, "appsettings.json");

using (StreamReader r = new StreamReader(file))
{
    string json = r.ReadToEnd();
    SettingFile setting = JsonConvert.DeserializeObject<SettingFile>(json);
    statusSystem.SetSettings(setting.Settings);
}

using IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "KAC Edge Gateway";
    })
    .ConfigureServices(services =>
    {
        LoggerProviderOptions.RegisterProviderOptions<
            EventLogSettings, EventLogLoggerProvider>(services);

        services.AddSingleton<ListenQueueService>();
        services.AddHostedService<WindowsBackgroundService>();
        services.AddDbContext<KACGatewayContext>(options => options.UseNpgsql(statusSystem.Settings.ConnectionString,
                o => o.MigrationsAssembly(typeof(Setting).Assembly.GetName().Name)), ServiceLifetime.Transient);

    })
    .ConfigureLogging((context, logging) =>
    {
        // See: https://github.com/dotnet/runtime/issues/47303
        logging.AddConfiguration(
            context.Configuration.GetSection("Logging"));
    })
    .Build();


DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
optionsBuilder.UseNpgsql(statusSystem.Settings.ConnectionString);
using (var context = new KACGatewayContext(optionsBuilder.Options, statusSystem.Settings.ConnectionString)) { }


await host.RunAsync();