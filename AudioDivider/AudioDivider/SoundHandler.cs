using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;
using AudioDivider.WASAPI;

namespace AudioDivider
{
    class SoundInfoDevice
    {
        public string name; // Name of the device as shown in device manager
        public string ID; // Unique sound device ID, for internal use
        public List<SoundInfoSession> sessions = new List<SoundInfoSession>(); // Sessions playing on this device
    }

    class SoundInfoSession
    {
        public int pid; // Process ID
        public string windowName; // Name of the audio session, currently only the window name
    }

    static class SoundHandler
    {

        // Switches default device to current default devices. Doesn't change the device, but sends an event so the program needs to aquire the device again, this time using our hook.
        public static void switchDefaultDevice()
        {
            List<SoundInfoDevice> devices = getSoundInfo();
            if (devices.Count >= 2)
            {
                IMMDeviceEnumerator enumerator = (IMMDeviceEnumerator)new DeviceEnumerator();
                string id = enumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia).GetId();

                IPolicyConfig policyConfig = (IPolicyConfig)new IPolicyConfigClass();
                policyConfig.SetDefaultEndpoint(id, ERole.eMultimedia);
            }
        }

        // Lists all devices, and for each device all processes that are currently playing sound using that device
        public static List<SoundInfoDevice> getSoundInfo()
        {
            List<SoundInfoDevice> soundInfoDevices = new List<SoundInfoDevice>();

            DeviceEnumerator enumerator = new DeviceEnumerator();
            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)enumerator;
            IMMDeviceCollection deviceCollection = deviceEnumerator.EnumAudioEndpoints(EDataFlow.eRender, DeviceStatemask.DEVICE_STATE_ACTIVE);
            uint deviceCount = deviceCollection.GetCount();

            for (uint i = 0; i < deviceCount; i++)
            {
                SoundInfoDevice soundInfoDevice = new SoundInfoDevice();
                soundInfoDevices.Add(soundInfoDevice);

                IMMDevice device = deviceCollection.Item(i);
                string deviceId = device.GetId();
                soundInfoDevice.ID = deviceId;
                IMMPropertyStore propertyStore = device.OpenPropertyStore(ProperyStoreMode.STGM_READ);
                PropertyKey propertyKeyDeviceDesc = new PropertyKey();
                propertyKeyDeviceDesc.fmtid = new Guid(0xa45c254e, 0xdf1c, 0x4efd, 0x80, 0x20, 0x67, 0xd1, 0x46, 0xa8, 0x50, 0xe0);
                propertyKeyDeviceDesc.pid = 2;
                PropVariant deviceNamePtr = propertyStore.GetValue(ref propertyKeyDeviceDesc);
                string deviceName = Marshal.PtrToStringUni(deviceNamePtr.pszVal);
                soundInfoDevice.name = deviceName;

                Guid guidAudioSessionManager2 = new Guid("77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F");
                IAudioSessionManager2 audioSessionManager = (IAudioSessionManager2)device.Activate(ref guidAudioSessionManager2, (int)ClsCtx.CLSCTX_ALL, IntPtr.Zero);


                IAudioSessionEnumerator sessionEnumerator = audioSessionManager.GetSessionEnumerator();

                int sessionCount = sessionEnumerator.GetCount();
                for (int j = 0; j < sessionCount; j++)
                {

                    IAudioSessionControl audioSessionControl = sessionEnumerator.GetSession(j);
                    IAudioSessionControl2 audioSessionControl2 = (IAudioSessionControl2)audioSessionControl;
                    AudioSessionState state = audioSessionControl.GetState();
                    if (state == AudioSessionState.AudioSessionStateActive)
                    {
                        SoundInfoSession soundInfoSession = new SoundInfoSession();
                        soundInfoDevice.sessions.Add(soundInfoSession);

                        string displayName = audioSessionControl.GetDisplayName();
                        string iconPath = audioSessionControl.GetIconPath();
                        int processId = audioSessionControl2.GetProcessId();
                        string processName = Process.GetProcessById(processId).MainWindowTitle;

                        soundInfoSession.pid = processId;
                        soundInfoSession.windowName = processName;
                    }
                }
            }

            return soundInfoDevices;
        }
    }
}
