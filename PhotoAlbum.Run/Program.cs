using Microsoft.Extensions.DependencyInjection;
using PhotoAlbum.Business;
using PhotoAlbum.Business.Commands;
using PhotoAlbum.Run;

internal partial class Program
{
    private static void Main(string[] args)
    {
        var host = Setup.CreateHostBuilder(args).Build();

        bool flag = false;
        while (flag == false)
        {
            Console.WriteLine("Please enter an album id:");

            var enteredVal = Console.ReadLine();
            if (string.Compare(enteredVal, "Exit", true) == 0) break;

            var albumOrch = host.Services.GetService<AlbumOrchestrator>();

            if (albumOrch != null && enteredVal != null)
            {
                var album = albumOrch.Execute(enteredVal, Setup.AlbumUrl);

                if (!album.HasErrors)
                {
                    Console.WriteLine($"");
                    Console.WriteLine($"Album Id: {album.Id}");

                    album.Images.ForEach(img =>
                    {
                        Console.WriteLine($"");
                        Console.WriteLine($"Image # {img.Id}");
                        Console.WriteLine($"  Title: {img.Title}");
                    });
                    continue;
                }

                Console.WriteLine("");
                Console.WriteLine(album.Message);
            }

        }
    }
}
