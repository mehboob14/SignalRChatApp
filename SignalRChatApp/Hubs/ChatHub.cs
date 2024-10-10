using Microsoft.AspNetCore.SignalR;

namespace SignalRChatApp.Hubs
{

    public class ChatHub : Hub
    {
        private static Dictionary<string, List<string>> Rooms = new Dictionary<string, List<string>>();

        
        public async Task JoinRoom(string roomName, string displayName)
        {
           
            if (!Rooms.ContainsKey(roomName))
            {
                Rooms[roomName] = new List<string>();
            }

            
            if (!Rooms[roomName].Contains(displayName))
            {
                Rooms[roomName].Add(displayName);
            }

            
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);

           
            await Clients.Group(roomName).SendAsync("UserJoined", displayName, Rooms[roomName]);

         
            await Clients.Caller.SendAsync("UpdateUsersList", Rooms[roomName]);
        }

     
        public async Task SendMessage(string roomName, string displayName, string message)
        {
          
            await Clients.Group(roomName).SendAsync("ReceiveMessage", displayName, message);
        }

       
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            foreach (var room in Rooms)
            {
               
                var userToRemove = room.Value.FirstOrDefault(u => u == Context.ConnectionId);
                if (userToRemove != null)
                {
                    room.Value.Remove(userToRemove);

                    
                    await Clients.Group(room.Key).SendAsync("UserLeft", userToRemove);

                    
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, room.Key);
                    break;
                }
            }
            await base.OnDisconnectedAsync(exception);
        }
    }


}
