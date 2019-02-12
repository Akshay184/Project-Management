(function () {

    //$.connection.hub.logging = true;
    $("#AddMember").click(function () {
        $.connection.hub.start()
            .done(function () {
                console.log("It Worked");

                console.log(array[0].userId);
               
               
                $.connection.chatHub.server.joinRoom(array[0].userId)
                
                //  $.connection.chatHub.server.joinRoom($("#Room").val())

                $.connection.chatHub.server.sendMessage($("#Room").val(), array[0].userId)
                $("#Room").val(" ");
                console.log("It Worked");
            })
                .fail(function () {

                });
    })


    $.connection.chatHub.client.sendMessage = function (message, groupName) {
        // $("#Room").val("")
        $("#messages").append(message + "<br />");
    }

})()
