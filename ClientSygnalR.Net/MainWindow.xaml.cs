using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientSygnalR.Net
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        HubConnection connection;
        string roomName = "";
        int roomID = -1;
        bool userChosen = false;
        public MainWindow()
        {
            InitializeComponent();

           connection = new HubConnectionBuilder()
           .WithUrl("https://localhost:5002/chatHub")
           .Build();
         }

        private void handlers()
        {
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                //if(chat.Equals(chatName))
                //{
                    this.Dispatcher.Invoke(() =>
                    {
                       var newMessage = $"{user}: {message}";
                            messagesList.Items.Add(newMessage);
                    });
                //}
            });

            connection.On<string, string>("ReceivePrivateMessage", (user, message) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newMessage = $"{user}: {message}";
                    messagesList.Items.Add(newMessage);
                });
            });

            connection.On<string>("AddUserToListBox", (user) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newMessage = $"{user}";
                    usersList.Items.Add(newMessage);
                });
            });
            connection.On("ClearUserListBox", () =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    usersList.Items.Clear();
                });
            });
            connection.On<string>("AddToChannelListbox", (chatRoomName) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    channelRoomList.Items.Add(chatRoomName);
                });
                
            });
            connection.On<int>("ReceiveChannelNumber", (number) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    roomID = number;
                });
            }
            );
            connection.On<string>("RemoveFromUserListBox", (username) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    foreach(string usernameToDelete in usersList.Items)
                    {
                        if (usernameToDelete.Equals(username))
                        {
                            usersList.Items.Remove(usernameToDelete);
                            break;
                        }
                    }
                    
                });
            });

            connection.On<string>("SetUserName", (username) =>
            {
                nickname.Text = username;
            });

        }
        private async void connectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.StartAsync();
                
                messagesList.Items.Add("Connection started");
                connectButton.IsEnabled = false;
                sendButton.IsEnabled = true;
                handlers();
                await connection.InvokeAsync("LogInToServer");
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(checkBox.IsChecked == true && userChosen)
                {
                    await connection.InvokeAsync("SendPrivateMessage",
                           nickname.Text, messagebox.Text, (string)target.Content);
                    
                }
                else
                {
                    if(roomName != null)
                    await connection.InvokeAsync("SendMessage",
                        nickname.Text, messagebox.Text);
                }
                
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }

        private async void createNewRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(connection.State == HubConnectionState.Connected)
                {
                    await connection.InvokeAsync("CreateNewChannel", chatroomName.Text);
                }
                else
                {
                    await connection.StartAsync();
                    await connection.InvokeAsync("CreateNewChannel", chatroomName.Text);
                    await connection.StopAsync();
                }
                
            }
            finally
            {

            }
        }

        private async void channelRoomList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roomID > -1)
            {
                await connection.InvokeAsync("LogOutOfChannel", roomID, nickname.Text);
            }
            roomName = (string) channelRoomList.SelectedItem;
            messagesList.Items.Add("Zmieniono kanal na " + roomName);
            await connection.InvokeAsync("LogInToChannel", nickname.Text, roomName);
        }

        private void usersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = (string)usersList.SelectedItem;
            
            if (item != null)
            {
                target.Content = item;
                userChosen = true;
            }

        }

        private async void DataWindow_Closing(object sender, CancelEventArgs e)
        {
            string msg = "Czy napewno?";
            MessageBoxResult result =
              MessageBox.Show(
                msg,
                "Data App",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                // If user doesn't want to close, cancel closure
                e.Cancel = true;
            }else
            {
                if (connection.State == HubConnectionState.Connected)
                    await connection.InvokeAsync("LogOut", roomID, nickname.Text);
            }
            
        }
    }
}
