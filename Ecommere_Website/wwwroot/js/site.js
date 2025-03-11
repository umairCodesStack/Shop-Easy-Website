document.addEventListener('DOMContentLoaded', function () {
   
    var connection = new signalR.HubConnectionBuilder().withUrl("/notifierHub").build();
    connection.start();

    var sendButton = document.getElementById("sendButton");
    if (sendButton) {
        sendButton.addEventListener("click", function (event) {
            data = $('#txt').val();
            connection.invoke("sendNotification", data);
            event.preventDefault();
        });
    }

    connection.on("ReceiveNotification", function (data) {
        document.getElementById("dataDisplay").textContent = data;
        console.log(data);
    });
});