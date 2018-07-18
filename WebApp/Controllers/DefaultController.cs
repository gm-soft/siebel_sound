using System.Net;
using System.Web.Mvc;
using WebApp.Helpers;
using WebApp.ViewModels.Default;

namespace WebApp.Controllers
{
    public class DefaultController : Controller
    {
        public const string ControllerName = "Default";

        public const string AudioPlayerDataSessionKey = "AudioPlayerData";

        public const string VideoPlayerDataSessionKey = "VideoPlayerData";

        private const string AudioOnlyParamName = "k";
        private const string VideoParamName = "video";
        private const string AudioParamName = "audio";

        private const string ErrorMessageParamName = "message";

        public const string IndexActionName = nameof(Index);
        [HttpGet, ActionName(IndexActionName)]
        public ActionResult Index(
            [Bind(Prefix = AudioOnlyParamName)] string base64EncodedFilePath,
            [Bind(Prefix = VideoParamName)] string videoFilePathEncoded,
            [Bind(Prefix = AudioParamName)] string audioFilePathEncoded
        )
        {
            if (base64EncodedFilePath != null)
            {
                SessionHelper.SaveObjectToSession(AudioPlayerDataSessionKey, base64EncodedFilePath);

                return RedirectToAction(PlayAudioActionName);
            }

            if (videoFilePathEncoded != null && audioFilePathEncoded != null)
            {
                var objectToSave = new VideoPlayerViewModel
                {
                    AudioFilePathEncoded = audioFilePathEncoded,
                    VideoFilePathEncoded = videoFilePathEncoded
                };
                SessionHelper.SaveObjectToSession(VideoPlayerDataSessionKey, objectToSave);

                return RedirectToAction(PlayVideoActionName);
            }

            return ServerError("Необходимо указать ссылки на файлы");
        }

        public const string PlayAudioActionName = "play_audio";
        [HttpGet, ActionName(PlayAudioActionName)]
        public ActionResult PlayAudio()
        {
            string audioFilePathEncoded = SessionHelper.GetObjectFromSession<string>(AudioPlayerDataSessionKey);

            if (audioFilePathEncoded == null)
                return ServerError("Необходимо указать ссылку на файл");

            return View("~/Views/Default/AudioPlayer.cshtml", model: audioFilePathEncoded);
        }

        public const string PlayVideoActionName = "play_video";

        [HttpGet, ActionName(PlayVideoActionName)]
        public ActionResult PlayVideo()
        {
            VideoPlayerViewModel viewModel = SessionHelper.GetObjectFromSession<VideoPlayerViewModel>(VideoPlayerDataSessionKey);

            if (viewModel == null)
                return ServerError("Необходимо указать ссылки на файлы");

            return View("~/Views/Default/VideoPlayer.cshtml", viewModel);
        }

        public const string ServerErrorActionName = "error";
        [HttpGet, ActionName(ServerErrorActionName)]
        public ViewResult ServerError([Bind(Prefix = ErrorMessageParamName)]string message = null)
        {
            // не ставим статус в Response.StatusCode = (int) HttpStatusCode.InternalServerError,
            // потому что тогда ИИС будет показывать свою ошибку, ибо в настройках нет кастомных страниц ошибок

            return View("~/Views/Default/Error.cshtml", model: message);
        }
    }
}