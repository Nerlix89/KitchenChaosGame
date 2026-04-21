using Unity.VisualScripting;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(PlayerCharacter player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
        }
    }
}
