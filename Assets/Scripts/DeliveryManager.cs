using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// Управляет очередью заказов и посетителями, которые ждут готовые блюда.
/// </summary>
public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance {get; private set;}

    public event Action OnRecipeSpawned;
    public event Action OnRecipeCompleted;

    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private float spawnRecipeTimerMax = 4f;
    [SerializeField] private int waitingRecipesMax = 4;
    [SerializeField] private GameObject aICharacterPrefab;

    [Header("AI Points")]
    [SerializeField] private Transform aICharacterSpawnPoint;
    [SerializeField] private Transform queueStandPoint;
    [SerializeField] private List<Transform> aICharacterLeavePoints;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private AICharacter aiCharacterInQueue;

    /// <summary>
    /// Инициализирует singleton и список ожидающих рецептов.
    /// </summary>
    private void Awake()
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }

    /// <summary>
    /// Создает первого посетителя в очереди.
    /// </summary>
    private void Start()
    {
        UpdateAICharactersQueue();
    }

    /// <summary>
    /// Периодически добавляет новые заказы, пока очередь рецептов не заполнена.
    /// </summary>
    private void Update()
    {
        if (waitingRecipeSOList.Count < waitingRecipesMax)
        {
            spawnRecipeTimer -= Time.deltaTime;
            if (spawnRecipeTimer <= 0f)
            {
                spawnRecipeTimer = spawnRecipeTimerMax;

                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                
                if (OnRecipeSpawned != null) OnRecipeSpawned();
            }
        }
    }

    /// <summary>
    /// Проверяет тарелку на совпадение с ожидающими рецептами и завершает заказ при успехе.
    /// </summary>
    public bool DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                int ingredientsCounter = 0;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientsCounter++;
                            break;
                        }
                    }
                }
                if (ingredientsCounter == waitingRecipeSO.kitchenObjectSOList.Count)
                {
                    waitingRecipeSOList.RemoveAt(i);
                    if (OnRecipeCompleted != null) OnRecipeCompleted();
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Возвращает список рецептов, которые сейчас ожидают доставки.
    /// </summary>
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    /// <summary>
    /// Обновляет посетителя в очереди и создает нового у точки появления.
    /// </summary>
    public void UpdateAICharactersQueue()
    {
        if (aiCharacterInQueue != null)
        {
            aiCharacterInQueue.SetCharacterState(AICharacter.CharacterState.LeavingQueue);
        }

        aiCharacterInQueue = Instantiate(aICharacterPrefab, aICharacterSpawnPoint.position, Quaternion.identity).GetComponent<AICharacter>();
    }

    /// <summary>
    /// Передает предмет из рук игрока текущему посетителю.
    /// </summary>
    public void GiveKitchenObjectToAICharacter(PlayerCharacter player)
    {
        if (aiCharacterInQueue == null) return;

        player.GetKitchenObject().SetKitchenObjectParent(aiCharacterInQueue);
        aiCharacterInQueue.SetCharacterState(AICharacter.CharacterState.LeavingQueue);
    }

    /// <summary>
    /// Возвращает точку, в которой посетитель должен стоять в очереди.
    /// </summary>
    public Transform GetQueueStandPoint()
    {
        return queueStandPoint;
    }

    /// <summary>
    /// Возвращает случайную точку ухода посетителя.
    /// </summary>
    public Transform GetRandomLeavePoint()
    {
        Transform leavePoint = aICharacterLeavePoints[UnityEngine.Random.Range(0, aICharacterLeavePoints.Count)];
        return leavePoint;
    }
    
}
