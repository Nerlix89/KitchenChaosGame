using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отображает одну иконку ингредиента на UI тарелки.
/// </summary>
public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;

    /// <summary>
    /// Устанавливает спрайт ингредиента для иконки.
    /// </summary>
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.sprite;
    }
}
