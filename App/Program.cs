using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

class GlitchTagApp {
    [DllImport("glitch_security.dll")]
    private static extern bool verify_system_integrity();
    
    [DllImport("glitch_security.dll")]
    private static extern IntPtr validate_identifier(string input);

    private static string _username = "";
    private static bool _tosAccepted = false;

    static async Task Main(string[] args) {
        if (!verify_system_integrity()) return;

        InitializeDirectX();
        
        await RunSplashSequence();

        await RunLegalSequence();

        await RunIdentitySequence();

        RunMainMenu();
    }

    static async Task RunSplashSequence() {
        DrawTexture("Assets/Logo/Logo.png", ShimmerEffect: true);
        await Task.Delay(3000);
    }

    static async Task RunLegalSequence() {
        string protocols = File.ReadAllText("Assets/Legal/Protocols.txt");
        while (!_tosAccepted) {
            RenderScrollBox(protocols, "NEURAL LINK PROTOCOLS");
            if (GetScrollY() <= 0.05f && GetButtonA()) _tosAccepted = true;
            await Task.Yield();
        }
    }

    static async Task RunIdentitySequence() {
        while (string.IsNullOrEmpty(_username)) {
            RenderInputBox("ENTER NEURAL IDENTIFIER");
            string input = GetActiveInput();
            if (GetButtonStart()) {
                IntPtr ptr = validate_identifier(input);
                string result = Marshal.PtrToStringAnsi(ptr);
                if (result != "REJECTED") _username = result;
            }
            await Task.Yield();
        }
    }

    static void RunMainMenu() {
        while (true) {
            RenderBackgroundLiveArena();
            RenderGlitchButton("SYNC TO GLITCH", "Search for active nodes");
            RenderGlitchButton("SOLO ECHO", "Practice against ghost data");
            RenderGlitchButton("SYSTEM SETTINGS", "Modify feedback");
            RenderGlitchButton("TERMINATE", "Disconnect session");
        }
    }
}
