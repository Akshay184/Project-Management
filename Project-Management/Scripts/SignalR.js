(function () {

    $.connection.hub.logging = true;
    $("#AddMember").click(function () {
        $.connection.hub.start()
            .done(function () {
                console.log("It Worked");

                $.connection.chatHub.server.joinRoom($("#Room").val())

                $.connection.chatHub.server.sendMessage("message here  ", $("#Room").val())
                $("#Room").val("");
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
