using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.Services.InternetCheck
{
    public class InternetChecker : IInternetChecker
    {
        public event Action ConnectionLost;
        public event Action ConnectionRestoreAttempted;

        public void StartCheckInternetConnection() => 
            CheckInternetConnection();

        public void TryToReconnect()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable) return;
            
            ConnectionRestoreAttempted?.Invoke();
            ConnectionRestoreAttempted = null;
        }

        private async void CheckInternetConnection()
        {
            while (Application.internetReachability != NetworkReachability.NotReachable) 
                await UniTask.WaitForSeconds(5);
        
            ConnectionLost?.Invoke();
            ConnectionLost = null;
        }
    }
}
