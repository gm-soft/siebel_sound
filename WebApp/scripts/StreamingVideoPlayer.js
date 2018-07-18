;
(function() {

    window.StreamingVideoPlayer = StreamingVideoPlayer;

    function StreamingVideoPlayer(config) {

        var that = this;

        that._videoPlayer = document.getElementById(config.videoPlayerDocumentId);
        that._videoPlayerJqueryObject = $("#" + config.videoPlayerDocumentId);

        // указывать не обязательно
        that._onPlayCallback = config.onPlayCallback || null;


        that._errorPageUrl = config.errorPageUrl || null;

        that._videoDidNotStartedToPlay = true;

        if (!config.audioPlayer)
            throw "Передайте ссылку на аудиоплеер";

        that._audioPlayer = config.audioPlayer;

        that._currentTime = 0;

        that._duration = 0;

        that._init();
    }

    StreamingVideoPlayer.prototype = {

        FullscreenSize: function () {

            var that = this;

            // Блок ниже разворачивает видео на весь экран.Блоки if-else нужны, т.к.реализация разная 
            // https://stackoverflow.com/a/6039930 
            if (that._videoPlayer.requestFullscreen) {

                that._videoPlayer.requestFullscreen();
            } else if (that._videoPlayer.mozRequestFullScreen) {

                that._videoPlayer.mozRequestFullScreen();
            } else if (that._videoPlayer.webkitRequestFullscreen) {

                that._videoPlayer.webkitRequestFullscreen();
            } else if (that._videoPlayer.msRequestFullscreen) {

                that._videoPlayer.msRequestFullscreen();
            }
        },

        DoesStartedToPlay: function() {

            var that = this;
            
            return that._currentTime != 0;
        },

        Play: function() {

            var that = this;

            that._videoPlayer.play();
        },

        Pause: function () {

            var that = this;

            that._videoPlayer.pause();
        },

        onVideoPlayingCallback: function (event) {

            var that = this;

            // вызываем кастомный коллбек и передаем метку о том, 
            // что видео только начало воспроизводиться, а не снято с паузы
            if (that._onPlayCallback)
                that._onPlayCallback();

            that._audioPlayer.Play();
        },

        onPauseCallback: function () {

            var that = this;

            if (that._currentTime !== that._duration)
                that._audioPlayer.Pause();
        },

        onVideoFinishedCallback: function () {

            var that = this;
        },

        onErrorCallback: function () {

            var that = this;

            window.location = that._errorPageUrl +
                "?message=Произошла ошибка при воспроизведении видео. Возможно, файл отсутствует на сервере";
        },

        onTimeUpdateCallback: function(currentTime, duration) {

            var that = this;

            that._currentTime = parseInt(currentTime);
            that._duration = parseInt(duration);
        },

        _init: function () {

            var that = this;

            that._videoPlayerJqueryObject.bind("playing", function(event) {
                that.onVideoPlayingCallback();
            });

            that._videoPlayerJqueryObject.bind("pause", function (event) {
                that.onPauseCallback();
            });

            that._videoPlayerJqueryObject.bind("ended", function (event) {
                that.onVideoFinishedCallback();
            });

            that._videoPlayerJqueryObject.find("source").bind("error", function (event) {
                that.onErrorCallback();
            });

            that._videoPlayerJqueryObject.on("timeupdate", function (event) {
                that.onTimeUpdateCallback(this.currentTime, this.duration);
            });
        }
    };


}());