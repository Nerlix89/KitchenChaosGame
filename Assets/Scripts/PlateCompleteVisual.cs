using System.Collections.Generic;
using System;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;
        [SerializeField] private GameObject gameObject;

        public KitchenObjectSO GetKitchenObjectSO() {return kitchenObjectSO;}
        public GameObject GetGameObject() {return gameObject;}
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            kitchenObjectSOGameObject.GetGameObject().SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(KitchenObjectSO kitchenObjectSO)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSOGameObject.GetKitchenObjectSO() == kitchenObjectSO)
            {
                kitchenObjectSOGameObject.GetGameObject().SetActive(true);
            }
        }
    }
}
