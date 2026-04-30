using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

class GlitchTagApp {
    [DllImport(GlitchConstants.DLL_PATH_SECURITY)]
    private static extern bool verify_system_integrity();
    
    [DllImport(GlitchConstants.DLL_PATH_SECURITY)]
    private static extern IntPtr validate_identifier(string input);

    private static string _username = "";
    private static bool _tosAccepted = false;
    private static GlitchRenderer _renderer = new GlitchRenderer();
    private static InputManager _input = new InputManager();

    static async Task Main(string[] args) {
        if (!verify_system_integrity()) return;

        _renderer.InitializeWindow();
        
        await RunSplashSequence();

        await RunLegalSequence();

        await RunIdentitySequence();

        RunMainMenu();
    }

    static async Task RunSplashSequence() {
        float timer = 0;
        while (timer < GlitchConstants.SPLASH_DURATION) {
            timer += 0.016f;
            _renderer.RenderSplash(MathF.Sin(timer * 2.0f));
            await Task.Delay(16);
        }
    }

    static async Task RunLegalSequence() {
        string protocols = File.ReadAllText(GlitchConstants.ASSET_PATH_TOS);
        float scrollY = 1.0f;

        while (!_tosAccepted) {
            var state = _input.GetState();
            scrollY -= state.RightThumbstickY * 0.01f;
            scrollY = Math.Clamp(scrollY, 0, 1);

            _renderer.RenderLegalText(protocols, scrollY);

            if (scrollY <= 0.05f && state.Buttons.HasFlag(GamepadButtons.A)) {
                _tosAccepted = true;
            }
            await Task.Yield();
        }
    }

    static async Task RunIdentitySequence() {
        while (string.IsNullOrEmpty(_username)) {
            _renderer.RenderUsernamePrompt();
            
            if (GetButtonStart()) {
                string input = GetActiveInput();
                IntPtr ptr = validate_identifier(input);
                string result = Marshal.PtrToStringAnsi(ptr);
                
                if (result != "REJECTED") {
                    _username = result;
                    _renderer.TriggerGlitchTransition();
                } else {
                    _renderer.ShowError("INVALID NEURAL ID");
                }
            }
            await Task.Yield();
        }
    }

    static void RunMainMenu() {
        while (true) {
            _renderer.RenderBackgroundLiveArena();
            _renderer.RenderGlitchButton("SYNC TO GLITCH", "Search for active nodes", 0);
            _renderer.RenderGlitchButton("SOLO ECHO", "Practice against ghost data", 1);
            _renderer.RenderGlitchButton("SYSTEM SETTINGS", "Modify feedback", 2);
            _renderer.RenderGlitchButton("TERMINATE", "Disconnect session", 3);
            
            _renderer.Present();
        }
    }
}
