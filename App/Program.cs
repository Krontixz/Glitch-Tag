using System;
using System.Runtime.InteropServices;
using Microsoft.XBox.Services;

class GlitchTagApp {
    [DllImport("glitch_security.dll")]
    private static extern bool verify_system_integrity();

    static void Main(string[] args) {
        // 1. Splash Screen Logic
        Console.WriteLine("BOOTING GLITCH_TAG_OS...");
        if (!verify_system_integrity()) {
            return; // Security fail
        }

        // 2. Initialize GDK
        var initArgs = new XblInitArgs();
        SDK.XblInitialize(ref initArgs);

        // 3. Launch Game Loop
        using (var game = new GlitchEngine()) {
            game.Run();
        }
    }
}
