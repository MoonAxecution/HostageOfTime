using System;
using HOT.LootLocker.Auth;

namespace HOT.Auth
{
    public class LoginManager
    {
        private readonly IAuth authSystem;
        
        public LoginManager()
        {
            authSystem = new PlayerAuth();
        }

        public void Auth(Action onSuccess)
        {
            authSystem.Auth(onSuccess);
        }
    }
}