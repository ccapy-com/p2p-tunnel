﻿using System;
using System.Runtime.InteropServices;

namespace common.libs.extends
{
    public static class StructExtends
    {
        public static byte[] StructToBytes(this object structObj)
        {
            int size = Marshal.SizeOf(structObj);
            byte[] bytes = new byte[size];
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(structObj, structPtr, false);
            Marshal.Copy(structPtr, bytes, 0, size);
            Marshal.FreeHGlobal(structPtr);
            return bytes;
        }
    }
}
