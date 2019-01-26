(function () {
$.connection.hub.start()
   .done(function() {
       console.log("It Worked");
        $.connection.hub.logging = true;
            $.connection.chatHub.server.sendMessage("message here  ")
        })
        .fail(function() {

    });

$.connection.chatHub.client.sendMessage = function(message ) {

    $("#messages").append(message+ "<br />");
}

})()
