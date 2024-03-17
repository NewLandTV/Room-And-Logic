using UnityEngine;

public class Controller : MonoBehaviour
{
    public Vector2 moveDirection
    {
        get;
        private set;
    }
    public Vector2 rotateDirection
    {
        get;
        private set;
    }
    public bool fireKeyDown
    {
        get;
        private set;
    }

    public void OnMoveDirectionButtonPointerDown(int direction)
    {
        switch (direction)
        {
            case 0:
                moveDirection = Vector2.up;

                break;
            case 1:
                moveDirection = Vector2.left;

                break;
            case 2:
                moveDirection = Vector2.down;

                break;
            case 3:
                moveDirection = Vector2.right;

                break;
        }
    }

    public void OnMoveDirectionButtonPointerUp() => moveDirection = Vector2.zero;

    public void OnRotateCameraOrPlayerButtonPointerDown(int direction)
    {
        switch (direction)
        {
            case 0:
                rotateDirection = Vector2.up;

                break;
            case 1:
                rotateDirection = Vector2.left;

                break;
            case 2:
                rotateDirection = Vector2.down;

                break;
            case 3:
                rotateDirection = Vector2.right;

                break;
        }
    }

    public void OnRotateCameraOrPlayerButtonPointerUp() => rotateDirection = Vector2.zero;

    public void OnFireKeyPointerDown() => fireKeyDown = true;

    public void OnFireKeyPointerUp() => fireKeyDown = false;
}
