using System;
using UnityEngine;

/// <summary>
/// Стойка-контейнер, которая выдает игроку заданный кухонный предмет.
/// </summary>
public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event Action OnPlayerGrabbedObject;

    /// <summary>
    /// Создает предмет в руках игрока, если руки свободны.
    /// </summary>
    public override void Interact(PlayerCharacter player)
    {
        if (GetKitchenObject() == null)
        {
            if (!player.HasKitchenObject())
            {
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
                if (OnPlayerGrabbedObject != null) OnPlayerGrabbedObject();
            }
        }
    }
}
