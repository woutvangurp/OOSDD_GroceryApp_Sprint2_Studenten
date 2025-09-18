using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.App.Views;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Windows.Input;

namespace Grocery.App.ViewModels {
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly GlobalViewModel _global;

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                if (_email == value)
                    return;
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                if (_password == value)
                    return;
                _password = value;
                OnPropertyChanged();
            }
        }

        private string _loginMessage;

        public string LoginMessage
        {
            get => _loginMessage;
            set
            {
                if (_loginMessage == value)
                    return;
                _loginMessage = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel(IAuthService authService, GlobalViewModel global)
        {
            _authService = authService;
            _global = global;
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        [RelayCommand]
        private void ExecuteLogin()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                LoginMessage = "Vul alle velden in.";
                return;
            }

            Client? authenticatedClient = _authService.Login(Email, Password);

            if (authenticatedClient == null)
            {
                LoginMessage = "Ongeldige inloggegevens.";
                return;
            }

            LoginMessage = $"Welkom {authenticatedClient.name}!";
            _global.Client = authenticatedClient;
            if (Application.Current != null)
                Application.Current.MainPage = new AppShell();
            else
                throw new ApplicationException();
        }
    }
}
