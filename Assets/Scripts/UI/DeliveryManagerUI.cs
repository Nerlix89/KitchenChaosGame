using Unity.VisualScripting;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeComplited;

        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned()
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeComplited()
    {
        UpdateVisual();
    }

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
