using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Управляет AI-посетителем, его движением по NavMesh и переносом заказа.
/// </summary>
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

    /// <summary>
    /// Получает NavMeshAgent и сохраняет исходную скорость движения.
    /// </summary>
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        movementSpeed = navMeshAgent.speed;
    }

    /// <summary>
    /// Отправляет посетителя к месту в очереди.
    /// </summary>
    private void Start()
    {
        SetCharacterState(CharacterState.MovingInQueue);
    }

    /// <summary>
    /// Обновляет цель движения посетителя в зависимости от текущего состояния.
    /// </summary>
    void Update()
    {
        if (navMeshAgent == null) return;
        
        if (currentCharacterState == CharacterState.MovingInQueue || currentCharacterState == CharacterState.LeavingQueue)
        {
            if (movementTarget == null) return;
            navMeshAgent.SetDestination(movementTarget.position);
        }
    }

    /// <summary>
    /// Меняет состояние посетителя и настраивает скорость и цель движения.
    /// </summary>
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

    /// <summary>
    /// Возвращает, двигается ли посетитель.
    /// </summary>
    public bool GetIsWalking()
    {
        if (navMeshAgent == null) return false;

        return navMeshAgent.speed > 0.1f;
    }

    /// <summary>
    /// Уничтожает объект посетителя.
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Возвращает точку, в которой посетитель держит кухонный предмет.
    /// </summary>
    public Transform GetKitchenObjectFollowTransform()
    {
        return KitchenObjectHoldPoint;
    }

    /// <summary>
    /// Возвращает предмет, который находится у посетителя.
    /// </summary>
    public KitchenObject GetKitchenObject() {return kitchenObject;}

    /// <summary>
    /// Назначает посетителю кухонный предмет.
    /// </summary>
    public void SetKitchenObject(KitchenObject newKitchenObject) {kitchenObject = newKitchenObject;}

    /// <summary>
    /// Очищает ссылку на предмет у посетителя.
    /// </summary>
    public void ClearKitchenObject() {kitchenObject = null;}

    /// <summary>
    /// Проверяет, есть ли у посетителя кухонный предмет.
    /// </summary>
    public bool HasKitchenObject() {return kitchenObject != null;}
}
