using UnityEngine;

/// <summary>
/// Стойка доставки, принимающая готовые блюда от игрока.
/// </summary>
public class DeliveryCounter : BaseCounter
{
    /// <summary>
    /// Проверяет тарелку игрока и отправляет заказ посетителю при успешной доставке.
    /// </summary>
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
