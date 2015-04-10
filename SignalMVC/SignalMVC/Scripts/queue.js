


$(function () {


    function moveRow() {
        var row = $(this).closest('tr');
        if ($(this).hasClass('up'))
            row.prev().before(row);
        else
            row.next().after(row);
    };

    var tHub = $.connection.timerHub;

    //Now all arrays can call swapItems to swap objects at two indices
    Array.prototype.swapItems = function (a, b) {
        var temp = this[a];
        this[a] = this[b];
        this[b] = temp;
    };

    //var parsedList;

    tHub.client.refreshList = function (jsonString) {
        
        //parse json representation of queue into JavaScript Array of Video objects
        var parsedList = JSON.parse(jsonString);
        var rowHTML = ""
        
        $('#tbd').empty();

        for (i = 0; i < parsedList.length; i++) {
            addRow(parsedList[i].title, parsedList[i].url, parsedList[i].votes);

        }
        
        $('#tbd button.move').click(function() {
            var row = $(this).closest('tr');
            if ($(this).hasClass('up'))
                row.prev().before(row);
            else
                row.next().after(row);
        });
        
       

    };

    $(document.body).on('click', 'button.move', function () {
        alert('button ' + this.parentNode.parentNode.rowIndex + ' clicked');
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
    cell3.innerHTML = votes;
    var cell4 = row.insertCell(3);
    var upButton = document.createElement("button");
    upButton.id = url;
    upButton.type = "button";
    upButton.className = "btn btn-default btn-lg move up";
    //upButton.onclick = above();
    var upBtnSpan = document.createElement("span");
    upBtnSpan.className = "glyphicon glyphicon-arrow-up";
    //upBtnSpan.onclick = above();
    cell4.appendChild(upButton);
    upButton.appendChild(upBtnSpan);
    

    var cell5 = row.insertCell(4);
    var downButton = document.createElement("button");
    downButton.id = url + "down";
    downButton.type = "button";
    downButton.className = "btn btn-default btn-lg move down move up";
    /*downButton.onclick = function() {
        var row = $(this).closest('tr');
        if ($(this).hasClass('up'))
            row.prev().before(row);
        else
            row.next().after(row);
    };*/
    var downBtnSpan = document.createElement("span");
    downBtnSpan.className = "glyphicon glyphicon-arrow-down";
    //downBtnSpan.onclick = above2();

    downButton.appendChild(downBtnSpan);
    cell5.appendChild(downButton);



    /*
    var cell3 = row.insertCell(2);
    var element2 = document.createElement("input");
    element2.type = "text";
    element2.name = "txtbox[]";
    cell3.appendChild(element2);
    */
};
/*
$('#queueList button.move').click(function () {
    var row = $(this).closest('tr');
    if ($(this).hasClass('up'))
        row.prev().before(row);
    else
        row.next().after(row);
});
*/
/*
function moveRow() {
    var row = $(this).closest('tr');
    if ($(this).hasClass('up'))
        row.prev().before(row);
    else
        row.next().after(row);
};
*/
/*
function above() {
    var aboveElement = document.getElementById("higherTestP");
    aboveElement.innerHTML = "poopopppoopoooopopopopoooop";
};

function above2() {
    var aboveElement = document.getElementById("higherTestP");
    aboveElement.innerHTML = "peeepepepepeepepepepeeeeepee";
};
*/
function upVote(upArrowIMG) {

    window.alert("upvoted: " + upArrowIMG);
};

var downVote = function (downURL) {
    window.alert("downvoted: " + downURL);
};