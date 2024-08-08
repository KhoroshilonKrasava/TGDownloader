using NAudio.Lame;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;

namespace ConsoleApp3
{
    internal class YouTubeDownloader
    {
        public void Downloader(string videoUrl, out string NameOfAudio)
        {
            string[] Name = new string[1];
            try
            {

                var youTube = YouTube.Default;
                var video = youTube.GetVideo(videoUrl);
                var tempVideoPath = Path.Combine(Directory.GetCurrentDirectory(), "youtube", video.FullName);
                var mp3Path = Path.ChangeExtension(tempVideoPath, ".mp3");

                Directory.CreateDirectory(Path.GetDirectoryName(tempVideoPath));

                File.WriteAllBytes(tempVideoPath, video.GetBytes());


                ConvertToMp3(tempVideoPath, mp3Path);

                Name[0] = video.FullName;

                Console.WriteLine($"Аудио успешно сохранено: {mp3Path}");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при скачивании видео: {ex.Message}");
            }
            finally
            {
                NameOfAudio = Name[0].TrimEnd('.', 'm', 'p', '4');
            }


        }

        static void ConvertToMp3(string inputPath, string outputPath)
        {
            using (var reader = new MediaFoundationReader(inputPath))
            using (var writer = new LameMP3FileWriter(outputPath, reader.WaveFormat, LAMEPreset.VBR_90))
            {
                reader.CopyTo(writer);
            }
        }
    }
}

