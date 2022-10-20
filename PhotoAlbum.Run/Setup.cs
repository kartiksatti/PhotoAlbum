using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PhotoAlbum.Business.Commands;
using PhotoAlbum.Business;
using Microsoft.Extensions.Configuration;

using PhotoAlbum.Business.Service;

namespace PhotoAlbum.Run
{
    public static class Setup
    {
        private static string _albumUrl= string.Empty;
        public static string AlbumUrl { get {
                if (!string.IsNullOrWhiteSpace(_albumUrl))
                    return _albumUrl;

                var builder = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

                _albumUrl = builder.Build().GetValue<string>("Albums:Url");
                return _albumUrl;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var _hostBuilder = Host.CreateDefaultBuilder(args)
             .ConfigureServices((services) => 
             {
                 services.AddScoped<AlbumRequestValidationCommand>();
                 services.AddScoped<AlbumCommand>();
                 services.AddScoped<AlbumApiService>();
                 services.AddScoped<AlbumOrchestrator>();
             });
            return _hostBuilder;
        }
    }
}
