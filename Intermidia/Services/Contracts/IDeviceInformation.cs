namespace Intermidia.Services.Contracts
{
    public interface IDeviceInformation
    {
        string GetIPAddress();
        string GetDeviceName();
        Task<bool> HasAccessLocation();
    }
}