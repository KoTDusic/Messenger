using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Client.Annotations;

namespace Client
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string _serverAddres;
        private const string DefaultAddres = "127.0.0.1";
        private const string DefaultUsername = "Admin";
        private ClientImplementation _client;
        private bool _isConnected;
        private string _message;
        private string _username;

        public string ServerAddres
        {
            get { return _serverAddres; }
            set
            {
                _serverAddres = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value; 
                OnPropertyChanged();
            }
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value; 
                OnPropertyChanged();
            }
        }

        public ICommand ConnectToServerCommand { get; private set; }
        public ICommand SendMessageCommand { get; private set; }
        public ViewModel()
        {
            ServerAddres = DefaultAddres;
            Username = DefaultUsername;
            ConnectToServerCommand = new RelayCommand(ConnectToServerExecute);
            SendMessageCommand = new RelayCommand(SendMessageExecute);
        }

        private void SendMessageExecute(object o)
        {
            _client.SendMessage(Username, Message);
        }

        private void ConnectToServerExecute(object o)
        {
            _client?.Dispose();
            _client = new ClientImplementation(ServerAddres);
            IsConnected = _client.Connect();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
