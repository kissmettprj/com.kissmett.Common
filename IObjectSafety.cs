﻿using System;
using System.Runtime.InteropServices;
namespace com.kissmett.Common
{
    [Guid("CB5BDC81-93C1-11CF-8F20-00805F2CD064"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IObjectSafety
    {
        void GetInterfacceSafyOptions(System.Int32 riid, out System.Int32 pdwSupportedOptions, out System.Int32 pdwEnabledOptions);
        void SetInterfaceSafetyOptions(System.Int32 riid, System.Int32 dwOptionsSetMask, System.Int32 dwEnabledOptions);
    }
}