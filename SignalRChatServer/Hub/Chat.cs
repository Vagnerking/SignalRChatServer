using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace SignalRChatServer
{
    public class Chat : Hub
    {

        public static List<Message> Messages;


        public Chat()
        {
            if (Messages == null)
                Messages = new List<Message>();
        }

        public void NewMessage(string username, string message)
        {
            Clients.All.SendAsync("newMessage", username, message);
            Messages.Add(new Message()
            {
                Text = message,
                UserName = username
            });
        }

        public void NewUser(string userName, string connectionId)
        {
            Clients.Client(connectionId).SendAsync("previousMessages", Messages);
            Clients.All.SendAsync("newUser", userName);
        }




    }

    public class Message
    {
        public string UserName { get; set; }

        public string Text { get; set; }
    }

}
