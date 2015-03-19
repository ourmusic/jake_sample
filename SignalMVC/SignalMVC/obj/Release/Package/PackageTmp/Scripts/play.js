var tag = document.createElement('script');

tag.src = "https://www.youtube.com/iframe_api";
var firstScriptTag = document.getElementsByTagName('script')[0];
firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);

var player;
var time;

function onYouTubeIframeAPIReady() {
    player = new YT.Player('player', {
        height: '390',
        width: '640',
        videoId: 'e17iXDf0NGE',
        events: {
            'onReady': onPlayerReady,
            'onStateChange': state
        }
    });
}

function onPlayerReady(event) {
    event.target.playVideo();
    timer = player.getDuration();
    //document.write( timer);
}

function state(event) {
    if (event.data == YT.PlayerState.ENDED) {
        player.cueVideoById('Xpe-JoGyPsY');
        //document.write("change");
        event.target.playVideo();
    }
}

//This is where the server-client code goes

$(function () {
    var prox = $.connection.timerHub;

    function init() {
        prox.server.startCountDown(5);
        player.cueVideoById('CvvgdXKrd7g');
        player.playVideo();
    }

    prox.client.change = function () {
        player.cueVideoById('Xpe-JoGyPsY');
    }
    

    $.connection.hub.start().done(init);
});