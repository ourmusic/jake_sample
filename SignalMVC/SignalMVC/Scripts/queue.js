


$(function () {

    var tHub = $.connection.timerHub;

    //Now all arrays can call swapItems to swap objects at two indices
    Array.prototype.swapItems = function (a, b) {
        var temp = this[a];
        this[a] = this[b];
        this[b] = temp;
    };

    tHub.client.refreshList = function (jsonString) {
        
        //parse json representation of queue into JavaScript Array of Video objects
        var parsedList = JSON.parse(jsonString);
        var rowHTML = ""
        
        $('#tbd').empty();

        for (i = 0; i < parsedList.length; i++) {
            addRow(parsedList[i].title, parsedList[i].url, parsedList[i].votes);

        }
              
       

    };

    $(document.body).on('click', 'button.upvote', function () {
        var row = this.parentNode.parentNode;

        var votesCell = row.cells[2];
        var oldVotes = parseInt(votesCell.innerHTML);
        votesCell.innerHTML = oldVotes + 1;

        var rowIndex = row.rowIndex;
        var videoTitle = row.cells[0].innerHTML;
        var videoURL = row.cells[1].innerHTML;


        var glyphSpan = this.getElementsByTagName("SPAN")[0];
        glyphSpan.className += "red";
        

        alert("rowIndex: " + rowIndex + " videoTitle: " + videoTitle + " videoURL: " + videoURL + " oldVotes: " + oldVotes);



        //this works moving the row up
        //$(row).prev().before(row);


    });


    $(document.body).on('click', 'button.downvote', function () {


        var row = this.parentNode.parentNode;

        alert("button clicked in row : " + row.rowIndex + "");
    });

    tHub.client.addVideo = function (vidTitle, vidURL) {
        addRow(vidTitle, vidURL, 0);
    };


    // Start the connection.
    $.connection.hub.start().done(function () {
        tHub.server.refreshClientQueue();
        $('#addVideo').click(function () {
            // Call the Send method on the hub.
            tHub.server.addToQueue($('#vidTitle').val(), $('#vidUrl').val());
            // Clear text box and reset focus for next comment.
            $('#vidUrl').val('');
            $('#vidTitle').val('').focus();
        });

 

    });

});

function addRow(title, url, votes) {
    var table = document.getElementById('queueList');
    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);
    var cell1 = row.insertCell(0);
    cell1.innerHTML = title;
    var cell2 = row.insertCell(1);
    cell2.innerHTML = url;

    var cell3 = row.insertCell(2);
    cell3.className = "votesText";
    cell3.innerHTML = votes;

    var cell4 = row.insertCell(3);
    var upButton = document.createElement("button");
    upButton.id = url;
    upButton.type = "button";
    upButton.className = "btn btn-default btn-lg move upvote";

    var upBtnSpan = document.createElement("span");
    upBtnSpan.className = "glyphicon glyphicon-arrow-up";

    cell4.appendChild(upButton);
    upButton.appendChild(upBtnSpan);
    

    var cell5 = row.insertCell(4);
    var downButton = document.createElement("button");
    downButton.id = url + "down";
    downButton.type = "button";
    downButton.className = "btn btn-default btn-lg downvote";

    var downBtnSpan = document.createElement("span");
    downBtnSpan.className = "glyphicon glyphicon-arrow-down";

    downButton.appendChild(downBtnSpan);
    cell5.appendChild(downButton);

};
