using System;
using HOT.Auth;
using LootLocker.Requests;
using UnityEngine;

namespace HOT.LootLocker.Auth
{
    public class PlayerAuth : IAuth
    {
        public void Auth(Action onSuccess)
        {
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (!response.success)
                {
                    Debug.Log("error starting LootLocker session");
                    onSuccess.Fire();
                    return;
                }

                Debug.Log("successfully started LootLocker session");
                onSuccess.Fire();
            });
        }
    }
}