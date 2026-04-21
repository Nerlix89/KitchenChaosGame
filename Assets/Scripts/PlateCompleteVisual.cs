using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// Отображает ингредиенты, которые были добавлены на тарелку.
/// </summary>
public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        [SerializeField] private KitchenObjectSO kitchenObjectSO;
        [SerializeField] private GameObject gameObject;

        /// <summary>
        /// Возвращает ингредиент, связанный с визуальным объектом.
        /// </summary>
        public KitchenObjectSO GetKitchenObjectSO() {return kitchenObjectSO;}

        /// <summary>
        /// Возвращает визуальный объект ингредиента.
        /// </summary>
        public GameObject GetGameObject() {return gameObject;}
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    /// <summary>
    /// Подписывается на добавление ингредиентов и скрывает все визуалы.
    /// </summary>
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            kitchenObjectSOGameObject.GetGameObject().SetActive(false);
        }
    }

    /// <summary>
    /// Показывает визуальный объект добавленного ингредиента.
    /// </summary>
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
