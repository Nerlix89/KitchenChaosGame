using UnityEngine;
using System;

public class PlatesCounter : BaseCounter
{
    public event Action OnPlateSpawned;
    public event Action OnPlateRemoved;

    [SerializeField] KitchenObjectSO plateKitchenObjectSO;
    [SerializeField] private float spawnPlateTimer = 4f;
    [SerializeField] private int maxPlateSpawnedAmount = 4;

    private float currentSpawnPlateTimer;
    private int currentPlateSpawnedAmount;
    

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
