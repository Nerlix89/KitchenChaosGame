using System;
using UnityEngine;

/// <summary>
/// Управляет набором иконок ингредиентов на тарелке.
/// </summary>
public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    /// <summary>
    /// Скрывает шаблон иконки ингредиента.
    /// </summary>
    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    /// <summary>
    /// Подписывается на добавление ингредиентов на тарелку.
    /// </summary>
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    /// <summary>
    /// Обновляет иконки после добавления ингредиента.
    /// </summary>
    private void PlateKitchenObject_OnIngredientAdded(KitchenObjectSO kitchenObjectSO)
    {
        UpdateVisual();
    }

    /// <summary>
    /// Перестраивает список иконок по текущим ингредиентам тарелки.
    /// </summary>
    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransfrom = Instantiate(iconTemplate, transform);
            iconTransfrom.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
            iconTransfrom.gameObject.SetActive(true);
        
        }
    }
}
