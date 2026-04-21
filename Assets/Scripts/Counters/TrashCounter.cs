using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Мусорная стойка, удаляющая предмет из рук игрока.
/// </summary>
public class TrashCounter : BaseCounter
{
    /// <summary>
    /// Уничтожает кухонный предмет, который держит игрок.
    /// </summary>
    public override void Interact(PlayerCharacter player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
        }
    }
}
