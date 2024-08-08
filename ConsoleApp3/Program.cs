using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.IO;
using File = System.IO.File;
using VideoLibrary;
using NAudio.Wave;
using NAudio.Lame;
using TGD;
using ConsoleApp3;
using System.Reflection.Metadata.Ecma335;


namespace TGD
{

    class Program
    {
        YouTubeDownloader YTD = new YouTubeDownloader();

        static void Main()
        {
            var client = new TelegramBotClient("7074588692:AAF2wWRNb2bjsek7xHPmGCLOXLw5SssqkAc");
            client.StartReceiving(Update, Error);
            Console.ReadLine();

        }


        private static async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {

            var chatId = update.Message.Chat.Id;
            string urlVideo;
            string Name;
            urlVideo = update.Message.Text;

            YouTubeDownloader youTubeDownloader = new YouTubeDownloader();
            TGBotControl tGBotControl = new TGBotControl();

            if (urlVideo != null & urlVideo.StartsWith("https:"))
            {
                await client.SendTextMessageAsync(chatId, "то");
                try
                {
                    youTubeDownloader.Downloader(urlVideo, out Name);
                    tGBotControl.SendVideo(client, update, token, Name);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                Console.WriteLine("Аудиофайл отправлен!");
            }
            else
                await client.SendTextMessageAsync(chatId, "Не то");
        }

        private static async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}



