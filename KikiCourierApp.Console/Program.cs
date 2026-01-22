using KikiCourierApp.BLL.Interfaces;
using KikiCourierApp.BLL.Services;
using KikiCourierApp.Console;
using KikiCourierApp.Infrastructure.InputProviders.DiscountInputProviders;
using KikiCourierApp.Infrastructure.InputProviders.PackageInputProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel
    .Debug()
    .WriteTo
    .Console()
    .WriteTo
    .File("logs/kiki-courier-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var services = new ServiceCollection();

services.AddLogging(builder =>
{
    builder.ClearProviders();
    builder.AddSerilog();
});

//Application providers
services.AddSingleton<DeliveryApp>();
services.AddSingleton<IDiscountRules>(
    sp =>
        new JsonDiscountInputProvider(
            @"Input\DiscountRules.json",
            sp.GetRequiredService<ILogger<JsonDiscountInputProvider>>()
        )
);

//BLL services
services.AddTransient<PackageReader>();

//Infrastructure providers
services.AddSingleton<IPackageInputProvider, ConsolePackageInputProvider>();

// services.AddTransient<IPackageInputProvider>(
//     sp =>
//         new FilePackageInputProvider(
//             @"Input\Packages.txt",
//             sp.GetRequiredService<ILogger<FilePackageInputProvider>>()
//         )
// );

var serviceProvider = services.BuildServiceProvider();

var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Application started");

var app = serviceProvider.GetRequiredService<DeliveryApp>();
app.Run();
