using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections;
namespace SignalR
{
    public class ClientChat
    {
        public string nickName;
        public IClientProxy clientProxy;
        public ClientChat(IClientProxy clientProxy, string nickName)
        {
            this.clientProxy = clientProxy;
            this.nickName = nickName;
        }
    }
    public class Chat
    {
        public string name;
        public ArrayList messageList;
        public ArrayList clientList;
        public Chat(string name)
        {
            this.name = name;
            messageList = new ArrayList();
            clientList = new ArrayList();
        }
    }
    public class ChatHub : Hub
    {
        static ArrayList chatRoomList = new ArrayList();
        private Chat FindChatRoomByChannelName(string channelName)
        {
            foreach(Chat chat in chatRoomList)
            {
                if (chat.name.Equals(channelName))
                    return chat;
            }
            return null;
        }

        private Chat FindChatRoomByUserName(string username, ref ClientChat clientSendingMessage)
        {
            foreach (Chat chat in chatRoomList)
            {
                foreach(ClientChat clientInChat in chat.clientList)
                {
                    if (clientInChat.nickName.Equals(username))
                    {
                        clientSendingMessage = clientInChat;
                        return chat;
                    }   
                }
            }
            return null;
        }

        private ClientChat FindClientChatByUsername(string username)
        {
            ClientChat clientToReturn = null;
            Chat chat = FindChatRoomByUserName(username, ref clientToReturn);
            return clientToReturn;
        }

        public async Task SendMessage(string userName, string message)
        {
            //var client = Clients.Caller;

            ClientChat clientSendingMessage = null;
            Chat chatroom = FindChatRoomByUserName(userName, ref clientSendingMessage);
            foreach(ClientChat client  in chatroom.clientList)
            {
                await client.clientProxy.SendAsync("ReceiveMessage", userName, message);
            }
        }

        public async Task SendPrivateMessage(string sender, string message, string target)
        {
            await FindClientChatByUsername(target).clientProxy.SendAsync("ReceiveMessage", "Priv od " + sender + " do " + target, message);
            await Clients.Caller.SendAsync("ReceiveMessage", "Priv od " + sender + " do " + target, message);
        }

        public async Task LogInToServer()
        {
            foreach(Chat chat in chatRoomList)
            {
                await Clients.Caller.SendAsync("AddToChannelListbox", chat.name);
            }
        }
        public async Task LogInToChannel(string userName, string channelName)
        {
            await Clients.Caller.SendAsync("ClearUserListBox");
            Chat chat = FindChatRoomByChannelName(channelName);
            foreach (ClientChat client in chat.clientList)
            {
                await client.clientProxy.SendAsync("AddUserToListBox", userName);
                await Clients.Caller.SendAsync("AddUserToListBox", client.nickName);
            };
            chat.clientList.Add(new ClientChat(Clients.Caller, userName));
            await Clients.Caller.SendAsync("AddUserToListBox", userName);
            await Clients.Caller.SendAsync("ReceiveChannelNumber", chatRoomList.IndexOf(chat));
        }

        public async void LogOutOfChannelAction(int channelID, string nickname)
        {
            Chat chat = (Chat)chatRoomList[channelID];
            foreach (ClientChat client in chat.clientList)
            {
                await client.clientProxy.SendAsync("RemoveFromUserListBox", nickname);
            };
            ArrayList clientsToDelete = new ArrayList();
            foreach (ClientChat client in chat.clientList)
            {
                if (client.nickName.Equals(nickname))
                {
                    clientsToDelete.Add(client);
                }
            };
            foreach (ClientChat client in clientsToDelete)
            {
                chat.clientList.Remove(client);
            }
        }

        public async Task LogOutOfChannel(int channelID, string nickname)
        {
            LogOutOfChannelAction(channelID, nickname);
        }

        public async Task LogOut(int channelID, string userName)
        {
            if(channelID >= 0)
                LogOutOfChannelAction(channelID, userName);
        }

        public async Task CreateNewChannel(string name)
        {
            chatRoomList.Add(new Chat(name));
            await Clients.All.SendAsync("AddToChannelListbox", name);
        }

    }
}