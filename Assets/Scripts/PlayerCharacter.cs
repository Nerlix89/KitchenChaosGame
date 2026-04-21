using System;
using UnityEngine;

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

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternativeAction += GameInput_OnInteractAlternativeAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void GameInput_OnInteractAlternativeAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternative(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool GetIsWalking() {return IsWalking;}

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

    private void SetSelectedCounter(BaseCounter newSelectedCounter)
    {
        selectedCounter = newSelectedCounter;
        if (OnSelectedCounterChanged != null) OnSelectedCounterChanged(selectedCounter);
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return KitchenObjectHoldPoint;
    }

    public KitchenObject GetKitchenObject() {return kitchenObject;}
    public void SetKitchenObject(KitchenObject newKitchenObject) {kitchenObject = newKitchenObject;}
    public void ClearKitchenObject() {kitchenObject = null;}
    public bool HasKitchenObject() {return kitchenObject != null;}
}
