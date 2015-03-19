var videos = {
   signalRConnect: $.connection.timerHub,
   init: function () {
       //Loads all customers
       videos.signalRConnect.client.allVideos = function (videoQueue) {
            /*for (i = 0; i < videoQueue.length; i++) {
                $(".list-group").append("<li class='list-group-item'>" 
                     + "<div class='row'>"
                    + "<div class='col-lg-4'>"
                    + customerList[i].FirstName + " "
                    + customerList[i].LastName
                    + "</div>"
                    + "<div class='col-lg-4'>"
                    + customerList[i].Address + "</i>"
                    + "</div>"
                    + "<div class='col-lg-4'>"
                    + customerList[i].Region 
                    + "</div>"
                    + "</div>" 
                    + "</li>");
            }*/
           document.getElementById("queue").innerHTML = videoQueue;
        };
        $.connection.hub.start();
    },
    /*addVideo: function () {
        // Adds new customer
        var newVideo = {
            title: $("#Title").val(),
            url: $("#url").val(),
            //Address: $("#Address").val(),
            //Region: $("#Region").val()
        };
        //customers.signalRConnect.server.addCustomer(newCustomer);
        //$.connection.hub.start();
    },*/
    updateQueue: function () {
        videos.signalRConnect.server.updateQueue();
        $.connection.hub.start();
    }
};
videos.init();
