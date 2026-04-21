using System;
using UnityEngine;

/// <summary>
/// Управляет движением, взаимодействиями и переносом предметов игроком.
/// </summary>
public class PlayerCharacter : MonoBehaviour, IKitchenObjectParent
{

    public static PlayerCharacter Instance { get; private set;}

    [SerializeField] private float movementSpeed = 7f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float interactionDistance = 2f;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform KitchenObjectHoldPoint;

    public event Action<BaseCounter> OnSelectedCounterChanged;
    private bool IsWalking;
    private Vector3 lastInteractDirection;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    /// <summary>
    /// Инициализирует singleton игрока.
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Подписывает игрока на события ввода.
    /// </summary>
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternativeAction += GameInput_OnInteractAlternativeAction;
    }

    /// <summary>
    /// Выполняет основное взаимодействие с выбранной стойкой.
    /// </summary>
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    /// <summary>
    /// Выполняет альтернативное взаимодействие с выбранной стойкой.
    /// </summary>
    private void GameInput_OnInteractAlternativeAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternative(this);
        }
    }

    /// <summary>
    /// Обновляет движение игрока и поиск доступного взаимодействия.
    /// </summary>
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    /// <summary>
    /// Возвращает, двигается ли игрок в текущем кадре.
    /// </summary>
    public bool GetIsWalking() {return IsWalking;}

    /// <summary>
    /// Обрабатывает движение, столкновения и поворот игрока.
    /// </summary>
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        movementDirection = movementDirection.normalized;

        float moveDistance = movementSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirection, moveDistance);

        if (!canMove)
        {
            Vector3 movementDirectionX = new Vector3(movementDirection.x, 0, 0).normalized;
            canMove = movementDirection.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirectionX, moveDistance);

            if (canMove)
            {
                movementDirection = movementDirectionX;
            }
            else
            {
                Vector3 movementDirectionZ = new Vector3(0, 0, movementDirection.z).normalized;
                canMove = movementDirection.z != 0 &&!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementDirectionZ, moveDistance);

                if (canMove)
                {
                    movementDirection = movementDirectionZ;
                }
                else
                {
                    
                }
            }
        }

        if (canMove)
        {
            transform.position += movementDirection * moveDistance;
        }
        
        IsWalking = movementDirection != Vector3.zero;
        
        transform.forward = Vector3.Slerp(transform.forward, movementDirection, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Определяет стойку перед игроком, с которой можно взаимодействовать.
    /// </summary>
    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        if (movementDirection != Vector3.zero)
        {
            lastInteractDirection = movementDirection;
        }
        
        RaycastHit interactRaycastHit;
        if (Physics.Raycast(transform.position, lastInteractDirection, out interactRaycastHit, interactionDistance, countersLayerMask))
        {
            BaseCounter baseCounter = interactRaycastHit.transform.GetComponent<BaseCounter>();
            if (baseCounter != null)
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
           SetSelectedCounter(null); 
        }
    }

    /// <summary>
    /// Назначает выбранную стойку и сообщает об этом визуальным системам.
    /// </summary>
    private void SetSelectedCounter(BaseCounter newSelectedCounter)
    {
        selectedCounter = newSelectedCounter;
        if (OnSelectedCounterChanged != null) OnSelectedCounterChanged(selectedCounter);
    }

    /// <summary>
    /// Возвращает точку, в которой игрок держит кухонный предмет.
    /// </summary>
    public Transform GetKitchenObjectFollowTransform()
    {
        return KitchenObjectHoldPoint;
    }

    /// <summary>
    /// Возвращает предмет, который находится у игрока.
    /// </summary>
    public KitchenObject GetKitchenObject() {return kitchenObject;}

    /// <summary>
    /// Назначает игроку новый кухонный предмет.
    /// </summary>
    public void SetKitchenObject(KitchenObject newKitchenObject) {kitchenObject = newKitchenObject;}

    /// <summary>
    /// Очищает ссылку на предмет у игрока.
    /// </summary>
    public void ClearKitchenObject() {kitchenObject = null;}

    /// <summary>
    /// Проверяет, держит ли игрок кухонный предмет.
    /// </summary>
    public bool HasKitchenObject() {return kitchenObject != null;}
}
