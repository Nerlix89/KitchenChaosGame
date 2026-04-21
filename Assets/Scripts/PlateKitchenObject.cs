using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Описывает тарелку, на которую можно добавлять допустимые ингредиенты.
/// </summary>
public class PlateKitchenObject : KitchenObject
{
    public event Action<KitchenObjectSO> OnIngredientAdded;
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectsSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    /// <summary>
    /// Инициализирует список добавленных ингредиентов.
    /// </summary>
    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    /// <summary>
    /// Пытается добавить ингредиент на тарелку.
    /// </summary>
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjectSOList.Contains(kitchenObjectSO) || !validKitchenObjectsSOList.Contains(kitchenObjectSO)) return false;

        kitchenObjectSOList.Add(kitchenObjectSO);
        if (OnIngredientAdded != null) OnIngredientAdded(kitchenObjectSO);
        return true;
    }

    /// <summary>
    /// Возвращает список ингредиентов на тарелке.
    /// </summary>
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
