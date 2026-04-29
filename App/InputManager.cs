using Microsoft.GameInput;

public class InputManager {
    private IGameInput _gameInput;
    private IGameInputDevice _device;

    public InputManager() {
        GameInput.Create(out _gameInput);
    }

    public GamepadState GetState() {
        _gameInput.GetCurrentReading(GameInputKind.Gamepad, _device, out var reading);
        reading.GetGamepadState(out var state);
        return state;
    }
}
