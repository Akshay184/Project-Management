using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Project_Management.Models;
using System.Threading.Tasks;

namespace Project_Management
{
    public class ChatHub : Hub
    {
        public void SendMessage(string Message, int GroupName, int UserId, string UserName)
        {

            Clients.Group(GroupName.ToString()).SendMessage(Message, UserId, UserName);
            Messages mssg = new Messages();
            mssg.AddMessage(UserId, Message, GroupName, UserName);


        }


        public async Task JoinRoom(string roomName)
        {

            await Groups.Add(Context.ConnectionId, roomName);
            //  Clients.Group(roomName).SendMessage("Akshay" + " joined.");
        }

        public void PrivateMessage(string ToUser, string Message)
        {
            Clients.User(ToUser).PrivateMessage(Message);
        }

    }
}