using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event Action OnPlayerGrabbedObject;

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
