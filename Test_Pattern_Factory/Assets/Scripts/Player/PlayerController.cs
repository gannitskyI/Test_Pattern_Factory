using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IInputHandler inputHandler;
    private PlayerMovement playerMovement;

    void Start()
    {
        inputHandler = new KeyboardInputHandler();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (inputHandler.IsLeftKeyPressed())
        {
            playerMovement.MoveLeft();
        }
        else if (inputHandler.IsRightKeyPressed())
        {
            playerMovement.MoveRight();
        }
    }
}
