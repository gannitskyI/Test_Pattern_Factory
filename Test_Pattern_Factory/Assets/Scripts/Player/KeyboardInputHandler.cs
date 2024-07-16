using UnityEngine;

public class KeyboardInputHandler : IInputHandler
{
    public bool IsLeftKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.A);
    }

    public bool IsRightKeyPressed()
    {
        return Input.GetKeyDown(KeyCode.D);
    }
}
