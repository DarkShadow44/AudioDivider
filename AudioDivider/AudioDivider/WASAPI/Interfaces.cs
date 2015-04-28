using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AudioDivider.WASAPI
{

    // #################### COM classes ####################

    [ComImport, Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
    class DeviceEnumerator
    {

    }

    [ComImport, Guid("870af99c-171d-4f9e-af0d-e63df40c2bc9")]
    class IPolicyConfigClass
    {

    }

    // #################### COM Interfaces ####################

    [ComImport, Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceEnumerator
    {
        IMMDeviceCollection EnumAudioEndpoints(EDataFlow dataFlow, DeviceStatemask dwStateMask);

        IMMDevice GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role);

        IMMDevice GetDevice([MarshalAs(UnmanagedType.LPWStr)] string pwstrId);

        void RegisterEndpointNotificationCallback(IMMNotificationClient pClient);

        void UnregisterEndpointNotificationCallback(IMMNotificationClient pClient);
    }

    [ComImport, Guid("0BD7A1BE-7A1A-44DB-8397-CC5392387B5E"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceCollection
    {
        uint GetCount();

        IMMDevice Item(uint nDevice);

    }

    [ComImport, Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDevice
    {
        [return: MarshalAs(UnmanagedType.IUnknown)]
        object Activate(ref Guid iid, int dwClsCtx, IntPtr pActivationParams);

        IMMPropertyStore OpenPropertyStore(ProperyStoreMode stgmAccess);

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetId();

        int GetState();

    }

    [ComImport, Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMPropertyStore
    {
        int GetCount();

        PropertyKey GetAt(int iProp);

        PropVariant GetValue(ref PropertyKey key);

        void SetValue(ref PropertyKey key, ref PropVariant propvar);

        void Commit();

    }

    [ComImport, Guid("BFA971F1-4D5E-40BB-935E-967039BFBEE4"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioSessionManager
    {
        IAudioSessionControl GetAudioSessionControl(ref Guid AudioSessionGuid, int StreamFlags);

        ISimpleAudioVolume GetSimpleAudioVolume(ref Guid AudioSessionGuid, int StreamFlags);
    }

    [ComImport, Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioSessionManager2
    {
        IAudioSessionControl GetAudioSessionControl(ref Guid AudioSessionGuid, int StreamFlags);

        ISimpleAudioVolume GetSimpleAudioVolume(ref Guid AudioSessionGuid, int StreamFlags);



        IAudioSessionEnumerator GetSessionEnumerator();

        void RegisterSessionNotification(IAudioSessionNotification SessionNotification);

        void UnregisterSessionNotification(IAudioSessionNotification SessionNotification);

        void RegisterDuckNotification([MarshalAs(UnmanagedType.LPWStr)] string sessionID, IAudioVolumeDuckNotification duckNotification);

        void UnregisterDuckNotification(IAudioVolumeDuckNotification duckNotification);
    }

    [ComImport, Guid("bfb7ff88-7239-4fc9-8fa2-07c950be9c6d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioSessionControl2
    {
        AudioSessionState GetState();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetDisplayName();

        void SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] string Value, ref Guid EventContext);

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetIconPath();

        void SetIconPath([MarshalAs(UnmanagedType.LPWStr)] string Value, ref Guid EventContext);

        void GetGroupingParam(ref Guid pRetVal);

        void SetGroupingParam(ref Guid Override, ref Guid EventContext);

        void RegisterAudioSessionNotification(IAudioSessionEvents NewNotifications);

        void UnregisterAudioSessionNotification(IAudioSessionEvents NewNotifications);



        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetSessionIdentifier();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetSessionInstanceIdentifier();

        int GetProcessId();

        void IsSystemSoundsSession();

        void SetDuckingPreference(int optOut);
    }

    [ComImport, Guid("F4B1A599-7266-4319-A8CA-E70ACB11E8CD"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioSessionControl
    {
        AudioSessionState GetState();

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetDisplayName();

        void SetDisplayName([MarshalAs(UnmanagedType.LPWStr)] string Value, ref Guid EventContext);

        [return: MarshalAs(UnmanagedType.LPWStr)]
        string GetIconPath();

        void SetIconPath([MarshalAs(UnmanagedType.LPWStr)] string Value, ref Guid EventContext);

        void GetGroupingParam(ref Guid pRetVal);

        void SetGroupingParam(ref Guid Override, ref Guid EventContext);

        void RegisterAudioSessionNotification(IAudioSessionEvents NewNotifications);

        void UnregisterAudioSessionNotification(IAudioSessionEvents NewNotifications);
    }

    [ComImport, Guid("E2F5BB11-0570-40CA-ACDD-3AA01277DEE8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IAudioSessionEnumerator
    {
        int GetCount();

        IAudioSessionControl GetSession(int SessionCount);
    }

    [ComImport, Guid("f8679f50-850a-41cf-9c72-430f290290c8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface IPolicyConfig
    {

        void GetMixFormat([MarshalAs(UnmanagedType.LPWStr)] string param1, /* WAVEFORMATEX ***/ out object param2);

        void GetDeviceFormat([MarshalAs(UnmanagedType.LPWStr)] string param1, int param2, /* WAVEFORMATEX ***/ out object param3);

        void ResetDeviceFormat([MarshalAs(UnmanagedType.LPWStr)] string param1);

        void SetDeviceFormat([MarshalAs(UnmanagedType.LPWStr)] string param1, ref WaveFormatEx param2, ref WaveFormatEx param3);

        void GetProcessingPeriod([MarshalAs(UnmanagedType.LPWStr)] string param1, int param2, IntPtr param4 /*pint64 */, IntPtr param5 /*pint64 */);

        void SetProcessingPeriod([MarshalAs(UnmanagedType.LPWStr)] string param1, IntPtr param2 /*pint64 */);

        void GetShareMode([MarshalAs(UnmanagedType.LPWStr)] string param1, ref DeviceShareMode param2);

        void SetShareMode([MarshalAs(UnmanagedType.LPWStr)] string param1, ref DeviceShareMode param2);

        void GetPropertyValue([MarshalAs(UnmanagedType.LPWStr)] string param1, ref PropertyKey param2, ref PropVariant param3);

        void SetPropertyValue([MarshalAs(UnmanagedType.LPWStr)] string param1, ref PropertyKey param2, ref PropVariant param3);

        void SetDefaultEndpoint([MarshalAs(UnmanagedType.LPWStr)] string wszDeviceId, ERole eRole);

        void SetEndpointVisibility([MarshalAs(UnmanagedType.LPWStr)] string param1, int param2);
    }

    // #################### COM Interfaces (unimplemented) ####################

    public interface IMMNotificationClient
    {

    }

    public interface ISimpleAudioVolume
    {

    }

    public interface IAudioSessionNotification
    {

    }

    public interface IAudioVolumeDuckNotification
    {

    }

    public interface IAudioSessionEvents
    {
    }
}
