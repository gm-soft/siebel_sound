;
(function() {

    window.MediaWaitPreloader = MediaWaitPreloader;

    function MediaWaitPreloader(config) {

        var that = this;

        that._playerWrapper = config.playerWrapper;

        that._preloaderInitialTextBlock = config.preloaderInitialTextBlock;
        that._preloaderSpinnerBlock = config.preloaderSpinnerBlock;

        that._startMediaLoadButton = config.startMediaLoadButton;

        that._onStartMediaLoadingButtonClickedCallback = config.onStartMediaLoadingButtonClickedCallback;

        that._init();
    }

    MediaWaitPreloader.prototype = {

        ShowMediaPlayerAfterLoading: function() {

            var that = this;

            if (that._playerWrapper.hasClass("hidden")) {

                that._preloaderSpinnerBlock.hide();
                that._playerWrapper.removeClass("hidden");
            }
        },

        _init: function () {

            var that = this;

            that._startMediaLoadButton.click(function () {

                that._onStartMediaLoadingButtonClickedCallback();

                that._preloaderInitialTextBlock.hide();

                that._preloaderSpinnerBlock
                    .removeClass("hidden")
                    .show();
            });
        },
    };

}());