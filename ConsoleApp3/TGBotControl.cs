using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using VideoLibrary;


namespace ConsoleApp3
{
    internal class TGBotControl
    {
        YouTubeDownloader youTubeDownloader = new YouTubeDownloader();
        public async Task SendVideo(ITelegramBotClient client, Update update, CancellationToken token, string Name)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            try
            {
                await using Stream stream1 = System.IO.File.OpenRead(@$"C:\Users\Nikita\source\repos\ConsoleApp3\ConsoleApp3\bin\Debug\net8.0\youtube\{Name}.mp4");
                await client.SendVideoAsync(chatId, stream1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public async Task SendAudio(ITelegramBotClient client, Update update, CancellationToken token, string Name)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            string audioFilePath = @$"C:\Users\Nikita\source\repos\ConsoleApp3\ConsoleApp3\bin\Debug\net8.0\youtube\{Name}.mp3";
            try
            {
                using Stream stream = System.IO.File.OpenRead(audioFilePath);
                var sendeMssage = await client.SendDocumentAsync(chatId, document: InputFile.FromStream(stream, Name + ".mp3"));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

