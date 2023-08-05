using System;

namespace HOT.Auth
{
    public interface IAuth
    {
        void Auth(Action onSuccess);
    }
}