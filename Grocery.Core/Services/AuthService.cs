using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services {
    public class AuthService : IAuthService {
        private readonly IClientService _clientService;
        public AuthService(IClientService clientService) {
            _clientService = clientService;
        }
        public Client? Login(string email, string password) {
            Client? logClient = _clientService.Get(email);
            if (logClient == null)
                return null;
            bool verified = PasswordHelper.VerifyPassword(password, logClient._password);
            return verified
                ? new Client(logClient.Id, logClient.name, logClient._emailAddress, "")
                : null;
        }
    }
}
