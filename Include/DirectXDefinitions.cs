using System;
using System.Runtime.InteropServices;

public enum D3D12_COMMAND_LIST_TYPE {
    DIRECT = 0,
    BUNDLE = 1,
    COMPUTE = 2,
    COPY = 3,
    VIDEO_DECODE = 4,
    VIDEO_PROCESS = 5,
    VIDEO_ENCODE = 6
}

[StructLayout(LayoutKind.Sequential)]
public struct D3D12_COMMAND_QUEUE_DESC {
    public D3D12_COMMAND_LIST_TYPE Type;
    public int Priority;
    public int Flags;
    public uint NodeMask;
}

[StructLayout(LayoutKind.Sequential)]
public struct DXGI_SWAP_CHAIN_DESC1 {
    public uint Width;
    public uint Height;
    public int Format;
    public bool Stereo;
    public DXGI_SAMPLE_DESC SampleDesc;
    public uint BufferUsage;
    public uint BufferCount;
    public int Scaling;
    public int SwapEffect;
    public int AlphaMode;
    public uint Flags;
}

[StructLayout(LayoutKind.Sequential)]
public struct DXGI_SAMPLE_DESC {
    public uint Count;
    public uint Quality;
}
