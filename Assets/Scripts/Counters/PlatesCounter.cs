using UnityEngine;
using System;

/// <summary>
/// Стойка, которая накапливает чистые тарелки и выдает их игроку.
/// </summary>
public class PlatesCounter : BaseCounter
{
    public event Action OnPlateSpawned;
    public event Action OnPlateRemoved;

    [SerializeField] KitchenObjectSO plateKitchenObjectSO;
    [SerializeField] private float spawnPlateTimer = 4f;
    [SerializeField] private int maxPlateSpawnedAmount = 4;

    private float currentSpawnPlateTimer;
    private int currentPlateSpawnedAmount;
    

    /// <summary>
    /// Периодически добавляет новую тарелку до максимального количества.
    /// </summary>
    private void Update()
    {
        currentSpawnPlateTimer += Time.deltaTime;
        if (currentSpawnPlateTimer > spawnPlateTimer)
        {
            currentSpawnPlateTimer = 0f;
            if (currentPlateSpawnedAmount < maxPlateSpawnedAmount)
            {
                currentPlateSpawnedAmount++;
                if (OnPlateSpawned != null) OnPlateSpawned();
            }
        }
    }
    /// <summary>
    /// Выдает тарелку игроку, если у него свободны руки.
    /// </summary>
    public override void Interact(PlayerCharacter player)
    {
        if (!player.HasKitchenObject())
        {
            if (currentPlateSpawnedAmount > 0)
            {
                currentPlateSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                if (OnPlateRemoved != null) OnPlateRemoved();
            }
        }
    }
}
