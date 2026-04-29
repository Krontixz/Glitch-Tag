using System;
using System.Runtime.InteropServices;
using Microsoft.XBox.Services;
using Silk.NET.Direct3D12;
using Silk.NET.DXGI;

class GlitchTagApp {
    [DllImport("glitch_security.dll")]
    private static extern bool verify_system_integrity();

    static void Main(string[] args) {
        if (!verify_system_integrity()) return;

        XblInitArgs initArgs = new XblInitArgs();
        SDK.XblInitialize(ref initArgs);

        using (var engine = new GlitchRenderer()) {
            engine.InitializeWindow();
            engine.LoadAssets();
            engine.RunLoop();
        }
    }
}
