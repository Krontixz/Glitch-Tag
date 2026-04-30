using System;
using System.Runtime.InteropServices;

public static class XTaskQueue {
    [DllImport("XGameRuntime.dll")]
    public static extern int XTaskQueueCreate(
        int dispatchMode,
        int completionMode,
        out IntPtr queue
    );
}

public static class XUser {
    [DllImport("XGameRuntime.dll")]
    public static extern int XUserAddAsync(
        int options,
        IntPtr queue,
        IntPtr context,
        IntPtr callback
    );

    [DllImport("XGameRuntime.dll")]
    public static extern int XUserGetId(
        IntPtr user,
        out ulong userId
    );
}
