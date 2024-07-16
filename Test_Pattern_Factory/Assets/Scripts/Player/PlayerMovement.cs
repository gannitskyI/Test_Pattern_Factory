using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private readonly float[] positions = new float[] { -6.13f, -2.12f, 1.95f };
    private int currentPositionIndex = 1; // Start at -2.12

    public void MoveLeft()
    {
        if (currentPositionIndex > 0)
        {
            currentPositionIndex--;
            UpdatePlayerPosition();
        }
    }

    public void MoveRight()
    {
        if (currentPositionIndex < positions.Length - 1)
        {
            currentPositionIndex++;
            UpdatePlayerPosition();
        }
    }

    private void UpdatePlayerPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = positions[currentPositionIndex];
        transform.position = newPosition;
    }
}
