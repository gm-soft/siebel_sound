;
(function() {

    window.StreamingAudioPlayer = StreamingAudioPlayer;

    function StreamingAudioPlayer(config) {

        var that = this;

        that._audioPlayer = document.getElementById(config.audioPlayerDocumentId);
        that._audioPlayerJqueryObject = $("#" + config.audioPlayerDocumentId);

        if (config.displayCurrentPosAndDurationBlockCssTag) {
            that._displayCurrentPosAndDurationBlock = $("." + config.displayCurrentPosAndDurationBlockCssTag);
        } else {

            that._displayCurrentPosAndDurationBlock = null;
        }

        that._audioDidNotStartedToPlay = true;

        // указывать не обязательно
        that._onPlayCallback = config.onPlayCallback || null;

        that._errorPageUrl = config.errorPageUrl || null;

        that._init();
    }

    StreamingAudioPlayer.prototype = {

        Play: function() {

            var that = this;

            that._audioPlayer.play();
        },

        Pause: function() {
            var that = this;

            that._audioPlayer.pause();
        },

        onPlayingCallback: function () {

            var that = this;

            // вызываем кастомный коллбек и передаем метку о том, 
            // что видео только начало воспроизводиться, а не снято с паузы
            if (that._onPlayCallback)
                that._onPlayCallback();
        },

        onFinishedCallback: function () {

            var that = this;
        },

        onErrorCallback: function (event) {

            var that = this;

            window.location = that._errorPageUrl +
                "?message=Произошла ошибка при воспроизведении аудио. Возможно, файл отсутствует на сервере";
        },

        onTimeUpdateCallback: function (currentTime, duration) {

            var that = this;

            if (!that._displayCurrentPosAndDurationBlock)
                return;

            var currentTimeSeconds = parseInt(currentTime);
            var durationSeconds = parseInt(duration);

            var text = "Аудио: " + currentTimeSeconds + " сек. /" + durationSeconds + " сек.";
            that._displayCurrentPosAndDurationBlock.html(text);
        },

        _init: function() {

            var that = this;

            that._audioPlayerJqueryObject.bind("playing", function () {
                that.onPlayingCallback();
            });

            that._audioPlayerJqueryObject.find("source").bind("error", function () {
                that.onErrorCallback();
            });

            that._audioPlayerJqueryObject.bind("ended", function () {
                that.onFinishedCallback();
            });

            that._audioPlayerJqueryObject.on("timeupdate", function (event) {
                that.onTimeUpdateCallback(this.currentTime, this.duration);
            });
        },
    };

}());