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
          //  Clients.Group(roomName).SendMessage("Akshay" + " joined.");
        }

        public async Task JoinRoomAll(int id)
        {
            Projects List = new Projects();
            var room = List.ListProject(id);
            foreach (var projects in room)
            {
                await Groups.Add(Context.ConnectionId, projects.ProjectId.ToString());
            }
           
            //  Clients.Group(roomName).SendMessage("Akshay" + " joined.");
        }


    }
}