using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web;
using NAudio.Lame;
using NAudio.Wave;
using NReco.VideoConverter;

namespace WebApp.Helpers
{
    public static class StreamWriteHelper
    {
        public static void WriteVideoContentToStream(string pathToVideoFile, Stream outputStream)
        {
            // pathToVideoFile - прямой путь до файла, не web-запрос, как в случае аудио
            try
            {
                var ffMpeg = new FFMpegConverter();
                ffMpeg.ConvertMedia(inputFile: pathToVideoFile,
                    inputFormat: Format.wmv,
                    outputStream: outputStream,
                    outputFormat: Format.mp4,
                    settings: null);
                
            }
            catch (Exception ex)
            {
                throw new HttpException(404, "Not Found", innerException: ex);
            }
            finally
            {
                outputStream.Close();
            }
        }

        public static async void WriteAudioContentToStream(Uri audioUri, Stream outputStream)
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(audioUri);

                WebListenerSiteHelpers.SetSertificateCallback(request);

                using (Stream sourceStream = request.GetResponse().GetResponseStream())
                using (MemoryStream ms = new MemoryStream())
                {
                    sourceStream.CopyTo(ms);
                    var buffer = ms.ToArray();

                    var mp3 = ConvertWavToMp3(buffer);

                    await outputStream.WriteAsync(mp3, 0, mp3.Length);
                }
            }
            catch (Exception ex)
            {
                throw new HttpException(404, "Not Found", innerException: ex);
            }
            finally
            {
                outputStream.Close();
            }
        }

        private static byte[] ConvertWavToMp3(byte[] wavFile)
        {
            using (var retMs = new MemoryStream())
            {
                using (var ms = new MemoryStream(wavFile))
                using (var rdr = new WaveFileReader(ms))
                using (var wtr = new LameMP3FileWriter(retMs, rdr.WaveFormat, 128))
                {
                    rdr.CopyTo(wtr);
                }

                //получение файла, только по завершению работы LameMP3FileWriter, при Dispose в поток дописываются остаточные данные
                return retMs.ToArray();
            }
        }
    }
}