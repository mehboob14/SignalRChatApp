let connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

connection.on("ReceiveMessage", function (user, message) {
    const msg = document.createElement("div");
    msg.textContent = `${user}: ${message}`;
    document.getElementById("messagesList").appendChild(msg);
});

connection.on("UserJoined", function (newUser, allUsers) {
    const usersList = document.getElementById("usersList");
    usersList.innerHTML = ''; 

   
    allUsers.forEach(function (user) {
        const userItem = document.createElement("li");
        userItem.textContent = user;
        usersList.appendChild(userItem);
    });
});

connection.on("UserLeft", function (user) {
    const usersList = document.getElementById("usersList");
    Array.from(usersList.children).forEach((li) => {
        if (li.textContent === user) {
            li.remove();
        }
    });
});

document.getElementById("sendMessage").addEventListener("click", function () {
    const message = document.getElementById("messageInput").value;
    if (message) {
        connection.invoke("SendMessage", roomName, displayName, message);
        document.getElementById("messageInput").value = '';
    }
});





