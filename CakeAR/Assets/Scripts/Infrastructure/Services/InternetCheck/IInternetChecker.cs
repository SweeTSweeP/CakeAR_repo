using System;

namespace Infrastructure.Services.InternetCheck
{
    public interface IInternetChecker : IService
    {
        event Action ConnectionLost;
        event Action ConnectionRestoreAttempted;
        void StartCheckInternetConnection();
        void TryToReconnect();
    }
}