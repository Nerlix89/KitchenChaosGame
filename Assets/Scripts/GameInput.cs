using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternativeAction;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternative.performed += InteracAlternative_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractAction != null)
        {
            OnInteractAction(this, EventArgs.Empty);
        } 
    }

    private void InteracAlternative_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractAlternativeAction != null)
        {
            OnInteractAlternativeAction(this, EventArgs.Empty);
        } 
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 InputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        InputVector = InputVector.normalized;

        return InputVector;
    }
}
