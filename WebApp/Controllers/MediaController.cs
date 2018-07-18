using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    public class MediaController : ApiController
    {
        public HttpResponseMessage GetAudio(string audio)
        {
            return ReturnApiResponse((stream, content, transportContext) =>
            {
                // Получаем ссылку на аудиофайл из сессии
                Uri wavUrl = MakeAudioFullWebUri(audio);
                StreamWriteHelper.WriteAudioContentToStream(wavUrl, stream);
            });
        }
        
        public HttpResponseMessage GetVideo(string video)
        {
            return ReturnApiResponse((stream, content, transportContext) =>
            {
                var filePath = WebListenerSiteHelpers.DecodeBase64(video);
                StreamWriteHelper.WriteVideoContentToStream(filePath, stream);
            });
        }

        #region Вспомогательные методы

        private Uri MakeAudioFullWebUri(string base64EncodedFilePath)
        {
            // ссылки fileUrl приходят с расширением slavic, файл может содержать видео и звуковую дорожку,
            // необходим только звуковой файл
            var fullUrl = WebListenerSiteHelpers.GetFileFullWebUrl(base64EncodedFilePath);
            fullUrl = fullUrl.Replace(".slavic", ".wav");
            return new Uri(fullUrl);
        }

        private HttpResponseMessage ReturnApiResponse(Action<Stream, HttpContent, TransportContext> action)
        {
            //https://forums.asp.net/t/2119288.aspx?Asynchronous+Video+Live+Streaming+with+ASP+NET+Web+APIs+2+0
            var httpResponce = Request.CreateResponse();
            httpResponce.Content = new PushStreamContent(action);
            return httpResponce;
        }

        #endregion
    }
}