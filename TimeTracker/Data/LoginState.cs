using System.Security.Cryptography;
using System.Text;
using TimeTracker.Models;
using TimeTracker.Service;

namespace TimeTracker.Data
{
    public class LoginState
    {
        public bool IsLoggedIn { get; set; }
        public UserModel User { get; set; }

        public Dictionary<int, CollectionDictionary> userCollections { get; set; }
        public Dictionary<int, string> collections { get; set; }
        public Dictionary<int, string> projects { get; set; }

        public event Action OnChange;

        private readonly UserService _userService;
        public LoginState(UserService userService) {
            _userService = userService;
        }


        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public void Login(UserModelInput input)
        {
            if (_userService.CheckPassword(input))
            {
                IsLoggedIn = true;
                User = _userService.UserInfo;
            }
        }
        public void LogOut()
        {
            IsLoggedIn= false;
            User = new UserModel();
        }


       
        
    }
}
