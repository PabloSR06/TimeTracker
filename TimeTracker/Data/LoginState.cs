﻿using TimeTracker.Models;


namespace TimeTracker.Data
{
    public class LoginState
    {
        public bool IsLoggedIn { get; set; }
        public string username { get; set; }
        public int userId { get; set; }
        public Dictionary<int, CollectionDictionary> userCollections { get; set; }
        public Dictionary<int, string>? collections { get; set; }
        public Dictionary<int, string>? projects { get; set; }

        public event Action OnChange;


        public void SetLogin(bool login, string user)
        {
            IsLoggedIn = login;
            username = user;

            NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
