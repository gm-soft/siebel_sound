using System;

namespace WebApp.ViewModels.Default
{
    // для сохранения в сессии
    [Serializable] 
    public class VideoPlayerViewModel
    {
        public string VideoFilePathEncoded { get; set; }

        public string AudioFilePathEncoded { get; set; }
    }
}