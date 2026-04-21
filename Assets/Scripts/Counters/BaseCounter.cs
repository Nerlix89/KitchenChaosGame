using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public virtual void Interact(PlayerCharacter player)
    {
    }

    public virtual void InteractAlternative(PlayerCharacter player)
    {
    }

    public Transform GetKitchenObjectFollowTransform() {return counterTopPoint;}
    public KitchenObject GetKitchenObject() {return kitchenObject;}
    public void SetKitchenObject(KitchenObject newKitchenObject) {kitchenObject = newKitchenObject;}
    public void ClearKitchenObject() {kitchenObject = null;}
    public bool HasKitchenObject() {return kitchenObject != null;}
}
