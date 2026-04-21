using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event Action<State> OnStateChanged;
    public event Action<float> OnProgressChanged;
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] BurningRecipeSO[] burningRecipeSOArray;

    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private State currentState;

    private void Start()
    {
        SetState(State.Idle);
    }

    private void Update()
    {
        switch(currentState)
        {
            case State.Idle:
                break;

            case State.Frying:
                FryingLogic();
                break;

            case State.Fried:
                BurningLogic();
                break;

            case State.Burned:
                break;
        }
    }

    public override void Interact(PlayerCharacter player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasFryingRecupeSOWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    SetState(State.Frying);
                    fryingTimer = 0f;
                    if (OnProgressChanged != null) OnProgressChanged(fryingTimer / fryingRecipeSO.maxFryingTimer);
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                    SetState(State.Idle);
                    if (OnProgressChanged != null) OnProgressChanged(0f);
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                SetState(State.Idle);
                if (OnProgressChanged != null) OnProgressChanged(0f);
            }
        }
    }

    private bool HasFryingRecupeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO == null) return false;

        return true;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO == null) return null;

        return fryingRecipeSO.output;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

        private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }

    private void SetState(State newState)
    {
        currentState = newState;
        if (OnStateChanged != null) OnStateChanged(currentState);
    }

    private void FryingLogic()
    {
        if (HasKitchenObject())
        {
            fryingTimer += Time.deltaTime;
            if (OnProgressChanged != null) OnProgressChanged(fryingTimer / fryingRecipeSO.maxFryingTimer);
            if (fryingTimer > fryingRecipeSO.maxFryingTimer)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                SetState(State.Fried);
                burningTimer = 0f;
            }
        }
    }

    private void BurningLogic()
    {
        if (HasKitchenObject())
        {
            burningTimer += Time.deltaTime;
            if (OnProgressChanged != null) OnProgressChanged(burningTimer / burningRecipeSO.maxBurningTimer);
            if (burningTimer > burningRecipeSO.maxBurningTimer)
            {
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                SetState(State.Burned);
                if (OnProgressChanged != null) OnProgressChanged(0f);
            }
        }
    }
}
