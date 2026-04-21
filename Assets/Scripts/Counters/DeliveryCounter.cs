using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(PlayerCharacter player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                if (DeliveryManager.Instance.DeliverRecipe(plateKitchenObject))
                {
                    DeliveryManager.Instance.GiveKitchenObjectToAICharacter(player);
                    DeliveryManager.Instance.UpdateAICharactersQueue();
                }
                else
                {
                    
                }
            }  
        }
    }
}
