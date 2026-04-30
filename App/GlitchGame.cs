using System;
using System.Runtime.InteropServices;

public class GlitchGame {
    [DllImport("glitch_security.dll")]
    private static extern IntPtr validate_identifier(string input);
    
    private enum GameState { Splash, Legal, Identity, MainMenu }
    private GameState _state = GameState.Splash;
    private GlitchUI _ui = new GlitchUI();

    public void Update() {
        switch (_state) {
            case GameState.Splash:
                if (Timer > 3.0f) _state = GameState.Legal;
                break;
            case GameState.Legal:
                _ui.Update(DeltaTime, ControllerInput);
                if (_ui.Accepted) _state = GameState.Identity;
                break;
            case GameState.Identity:
                if (InputSubmitted) {
                    IntPtr ptr = validate_identifier(InputText);
                    if (Marshal.PtrToStringAnsi(ptr) != "REJECTED") _state = GameState.MainMenu;
                }
                break;
        }
    }
}
