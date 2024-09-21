using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Введите URL для скачивания файла:");
        string? fileUrl = Console.ReadLine();

        Console.WriteLine("Введите путь для сохранения файла:");
        string? savePath = Console.ReadLine();

        Thread downloadThread = new Thread(() => DownloadFile(fileUrl, savePath));
        downloadThread.Start();

        Console.WriteLine("Скачивание файла началось. Вы можете продолжать работать с приложением.");

        while (true)
        {
            Console.WriteLine("Введите 'exit' для выхода из приложения.");
            string? input = Console.ReadLine();
            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
        }

        if (downloadThread.IsAlive)
        {
            downloadThread.Join();
        }

        Console.WriteLine("Приложение завершено.");
    }

    static async void DownloadFile(string url, string path)
    {
        using HttpClient client = new HttpClient();
        try
        {
            Console.WriteLine($"Скачивание файла с {url}...");
            byte[] fileBytes = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(path, fileBytes);
            Console.WriteLine($"Файл успешно скачан и сохранен по пути: {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при скачивании файла: {ex.Message}");
        }
    }
}
