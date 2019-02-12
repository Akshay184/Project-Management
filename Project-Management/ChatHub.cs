using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Project_Management.Models;


namespace Project_Management
{
    public class ChatHub : Hub
    {
        public void SendMessage(string Message, string GroupName)
        {

            Clients.Group(GroupName).SendMessage(Message);
            

        }


        
        public async Task JoinRoom(string roomName)
        {
           
            await Groups.Add(Context.ConnectionId, roomName);
          
        }


        public async Task JoinRoomAll(int roomName)
        {
            ProjectMembers room  = new ProjectMembers();
            var rooms = room.Groups(roomName);
            foreach (var group in rooms)
            {
               // var a = group.ProjectId.ToString();
                await Groups.Add(Context.ConnectionId, group.ProjectId.ToString());
            }
           
            
        }


    }
}