using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IndoorPlaygroundSafetyCheck.Commands;
using System.Windows.Input;
using IndoorPlaygroundSafetyCheck.Models;
using IndoorPlaygroundSafetyCheck.Services;

namespace IndoorPlaygroundSafetyCheck.ViewModels
{
    public class MessagingViewModel : INotifyPropertyChanged
    {
        private readonly MessagingService _messagingService;
        private ObservableCollection<Message> _messages;
        private string _newMessageText;
        private readonly int _currentUserId; // Add this field

        public ICommand RefreshMessagesCommand { get; private set; }

        public MessagingViewModel(MessagingService messagingService, int currentUserId)
        {
            _messagingService = messagingService;
            _currentUserId = currentUserId; // Ensure this private field exists in your ViewModel
            Messages = new ObservableCollection<Message>();
            RefreshMessagesCommand = new Commands.RelayCommand(o => RefreshMessages());
            RefreshMessages();
        }


   

        public void RefreshMessages()
        {
            Messages.Clear();
            var messages = _messagingService.GetMessagesForUser(_currentUserId);
            foreach (var message in messages)
            {
                Messages.Add(message);
            }
        }

        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged();
            }
        }

        public string NewMessageText
        {
            get => _newMessageText;
            set
            {
                _newMessageText = value;
                OnPropertyChanged();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void SendMessage(int receiverId)
        {
            if (string.IsNullOrEmpty(NewMessageText))
            {
                return; // Handle empty message case
            }

            var message = new Message
            {
                SenderIdent = _currentUserId,
                ReceiverIdent = receiverId,
                Notes = NewMessageText,
                // Initialize other necessary properties here
            };

            // Corrected call to SendMessage with both required arguments
            _messagingService.SendMessage(message, receiverId);

            NewMessageText = ""; // Clear the input field
            RefreshMessages(); // Update the message list
        }

        private void LoadMessages()
            {
                // Assuming there's a mechanism to get the current user ID
                int currentUserId = 1; // Placeholder for the current user's ID
                var messages = _messagingService.GetMessagesForUser(currentUserId);
                Messages.Clear();
                foreach (var message in messages)
                {
                    Messages.Add(message);
                }
            }
      
    }
    }
