using Microsoft.AspNetCore.SignalR;
using SignalRChatServer.Models;
using System;

namespace SignalRChatServer
{
    public class Chat : Hub
    {
        public static List<Message> Messages;
        public static List<UsersConnected> usersConnected;

        /// <summary>
        /// Constructor, initializes the Messages list
        /// </summary>
        public Chat()
        {
            if(Messages == null)
            {
                Messages = new List<Message>();
            }

            if(usersConnected == null)
            {
                usersConnected = new List<UsersConnected>();
            }
        }


        /// <summary>
        /// Send a new message to all clients
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="text"></param>
        public async Task NewMessage(string userName, string text)
        {
            await Clients.All.SendAsync("newMessage", userName, text);
            Messages.Add(new Message() 
            {
                UserName = userName, 
                Text = text 
            });
        }

        /// <summary>
        /// Initialize a new user with the previous messages
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="connectionId"></param>
        public async Task NewUser(string userName, string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("previousMessages", Messages);
            usersConnected.Add(new UsersConnected()
            {
                UserName = userName, 
                ConnectionId = connectionId 
            });
            await Clients.All.SendAsync("newUser", userName);
        }
    }
}
