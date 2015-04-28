using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AudioDivider.WASAPI
{
    public enum ERole
    {
        eConsole = 0,
        eMultimedia = 1,
        eCommunications = 2,
        ERole_enum_count = 3
    }

    public enum EDataFlow
    {
        eRender = 0,
        eCapture = 1,
        eAll = 2,
        EDataFlow_enum_count = 3
    }

    public enum DeviceStatemask : int
    {
        DEVICE_STATE_ACTIVE = 0x00000001,
        DEVICE_STATE_DISABLED = 0x00000002,
        DEVICE_STATE_NOTPRESENT = 0x00000004,
        DEVICE_STATE_UNPLUGGED = 0x00000008,
        DEVICE_STATEMASK_ALL = 0x0000000f
    }

    public enum ProperyStoreMode
    {
        STGM_READ = 0x00000000,
        STGM_WRITE = 0x00000001,
        STGM_READWRITE = 0x00000002
    }

    public enum ClsCtx
    {
        CLSCTX_ALL = 16 | 4 | 2 | 1
    }

    public enum AudioSessionState
    {
        AudioSessionStateInactive = 0,
        AudioSessionStateActive = 1,
        AudioSessionStateExpired = 2
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct PropertyKey
    {
        public Guid fmtid;
        public int pid;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct PropVariant
    {
        [FieldOffset(0)]
        public short vt;
        [FieldOffset(2)]
        public short wReserved1;
        [FieldOffset(4)]
        public short wReserved2;
        [FieldOffset(6)]
        public short wReserved3;

        [FieldOffset(8)]
        public sbyte cVal;
        [FieldOffset(8)]
        public byte bVal;
        [FieldOffset(8)]
        public short iVal;
        [FieldOffset(8)]
        public ushort uiVal;
        [FieldOffset(8)]
        public int lVal;
        [FieldOffset(8)]
        public uint ulVal;
        [FieldOffset(8)]
        public int intVal;
        [FieldOffset(8)]
        public uint uintVal;
        [FieldOffset(8)]
        public long hVal;
        [FieldOffset(8)]
        public ulong uhVal;
        [FieldOffset(8)]
        public float fltVal;
        [FieldOffset(8)]
        public double dblVal;
        [FieldOffset(8)]
        public IntPtr pszVal;
        [FieldOffset(8)]
        public IntPtr pwszVal;
    }

    struct WaveFormatEx
    {

    }

    enum DeviceShareMode
    {
    }
}
