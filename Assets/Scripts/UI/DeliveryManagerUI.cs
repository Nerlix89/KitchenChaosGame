using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Управляет списком ожидающих заказов в интерфейсе.
/// </summary>
public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    /// <summary>
    /// Скрывает шаблон UI-элемента рецепта.
    /// </summary>
    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    /// <summary>
    /// Подписывается на изменения заказов и строит начальный список.
    /// </summary>
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeComplited;

        UpdateVisual();
    }

    /// <summary>
    /// Обновляет список после появления нового заказа.
    /// </summary>
    private void DeliveryManager_OnRecipeSpawned()
    {
        UpdateVisual();
    }

    /// <summary>
    /// Обновляет список после завершения заказа.
    /// </summary>
    private void DeliveryManager_OnRecipeComplited()
    {
        UpdateVisual();
    }

    /// <summary>
    /// Перестраивает UI-список ожидающих рецептов.
    /// </summary>
    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTransfrom = Instantiate(recipeTemplate, container);
            recipeTransfrom.gameObject.SetActive(true);
            recipeTransfrom.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }

}
