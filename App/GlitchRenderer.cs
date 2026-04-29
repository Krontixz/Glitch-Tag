using System;
using Silk.NET.Direct3D12;
using Silk.NET.DXGI;

public unsafe class GlitchRenderer : IDisposable {
    private ComPtr<ID3D12Device> _device;
    private ComPtr<ID3D12CommandQueue> _commandQueue;
    private ComPtr<IDXGISwapChain4> _swapChain;

    public void InitializeWindow() {
        DXGIFactory.Create(out ComPtr<IDXGIFactory4> factory);
        D3D12.CreateDevice(null, D3DFeatureLevel.Level11, out _device);
        
        CommandQueueDesc desc = new CommandQueueDesc { Type = CommandListType.Direct };
        _device.Get()->CreateCommandQueue(desc, out _commandQueue);
    }

    public void RenderSplash(float shimmerIntensity) {
        var commandList = GetAvailableCommandList();
        // Graphics logic to draw assets/logo/logo.png with shimmer shader
        ExecuteCommands(commandList);
    }

    public void RunLoop() {
        while (true) {
            RenderSplash(MathF.Sin((float)DateTime.Now.TimeOfDay.TotalSeconds));
            _swapChain.Get()->Present(1, 0);
        }
    }

    public void Dispose() {
        _device.Dispose();
        _swapChain.Dispose();
    }
}
