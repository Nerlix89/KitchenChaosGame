using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отображает один ожидающий рецепт в интерфейсе заказов.
/// </summary>
public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    /// <summary>
    /// Скрывает шаблон иконки ингредиента.
    /// </summary>
    private void Start()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    /// <summary>
    /// Заполняет UI названием рецепта и иконками ингредиентов.
    /// </summary>
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;

        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTemplate.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
