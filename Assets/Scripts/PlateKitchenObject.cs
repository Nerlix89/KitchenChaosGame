using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event Action<KitchenObjectSO> OnIngredientAdded;
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectsSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjectSOList.Contains(kitchenObjectSO) || !validKitchenObjectsSOList.Contains(kitchenObjectSO)) return false;

        kitchenObjectSOList.Add(kitchenObjectSO);
        if (OnIngredientAdded != null) OnIngredientAdded(kitchenObjectSO);
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
