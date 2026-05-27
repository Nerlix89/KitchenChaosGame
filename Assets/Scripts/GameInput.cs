using UnityEngine;
using System;

/// <summary>
/// Обрабатывает пользовательский ввод и сообщает игровым объектам о действиях игрока.
/// </summary>
public class GameInput : MonoBehaviour
{

    public static GameInput Instance { get; private set;}

    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternativeAction;
    public event EventHandler OnPauseAction;
    private PlayerInputActions playerInputActions;

    /// <summary>
    /// Создает input actions и подписывает обработчики игровых действий.
    /// </summary>
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternative.performed += InteracAlternative_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternative.performed -= InteracAlternative_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
    }

    /// <summary>
    /// Вызывает событие основного взаимодействия.
    /// </summary>
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractAction != null)
        {
            OnInteractAction(this, EventArgs.Empty);
        } 
    }

    /// <summary>
    /// Вызывает событие альтернативного взаимодействия.
    /// </summary>
    private void InteracAlternative_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnInteractAlternativeAction != null)
        {
            OnInteractAlternativeAction(this, EventArgs.Empty);
        } 
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (OnPauseAction != null) OnPauseAction(this, EventArgs.Empty);
    }

    /// <summary>
    /// Возвращает нормализованное направление движения игрока.
    /// </summary>
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 InputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        InputVector = InputVector.normalized;

        return InputVector;
    }
}
