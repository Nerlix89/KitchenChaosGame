using UnityEngine;
using UnityEngine.AI;

public class AICharacter : MonoBehaviour, IKitchenObjectParent
{
    public enum CharacterState
    {
        None,
        MovingInQueue,
        StandingInQueue,
        LeavingQueue,
    }

    [SerializeField] private Transform KitchenObjectHoldPoint;
    
    private float movementSpeed;

    private CharacterState currentCharacterState;
    private NavMeshAgent navMeshAgent;
    private Transform movementTarget;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        movementSpeed = navMeshAgent.speed;
    }

    private void Start()
    {
        SetCharacterState(CharacterState.MovingInQueue);
    }

    void Update()
    {
        if (navMeshAgent == null) return;
        
        if (currentCharacterState == CharacterState.MovingInQueue || currentCharacterState == CharacterState.LeavingQueue)
        {
            if (movementTarget == null) return;
            navMeshAgent.SetDestination(movementTarget.position);
        }
    }

    public void SetCharacterState(CharacterState newCharacterState)
    {
        currentCharacterState = newCharacterState;

        if (newCharacterState == CharacterState.MovingInQueue)
        {
            navMeshAgent.speed = movementSpeed;
            movementTarget = DeliveryManager.Instance.GetQueueStandPoint();
        }
        if (newCharacterState == CharacterState.StandingInQueue)
        {
            navMeshAgent.speed = 0f;
        }
        else if (newCharacterState == CharacterState.LeavingQueue)
        {
            navMeshAgent.speed = movementSpeed;
            movementTarget = DeliveryManager.Instance.GetRandomLeavePoint();
        }
    }

    public bool GetIsWalking()
    {
        if (navMeshAgent == null) return false;

        return navMeshAgent.speed > 0.1f;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
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
