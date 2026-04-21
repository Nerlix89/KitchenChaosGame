using UnityEngine;

/// <summary>
/// Представляет предмет кухни, который можно переносить между родительскими объектами.
/// </summary>
public class KitchenObject : MonoBehaviour
{
   [SerializeField] private KitchenObjectSO kitchenObjectSO;

   private IKitchenObjectParent kitchenObjectParent;

   /// <summary>
   /// Возвращает данные этого кухонного предмета.
   /// </summary>
   public KitchenObjectSO GetKitchenObjectSO() {return kitchenObjectSO;}

   /// <summary>
   /// Возвращает текущего владельца предмета.
   /// </summary>
   public IKitchenObjectParent GetKitchenObjectParent() {return kitchenObjectParent;}

   /// <summary>
   /// Перемещает предмет к новому владельцу и обновляет его позицию.
   /// </summary>
   public void SetKitchenObjectParent(IKitchenObjectParent newkitchenObjectParent) 
   {
      if (kitchenObjectParent != null)
      {
         kitchenObjectParent.ClearKitchenObject();
      }
      kitchenObjectParent = newkitchenObjectParent;

      if (kitchenObjectParent.HasKitchenObject())
      {
         Debug.LogError("Counter already has KitchenObject!");
      }
      
      kitchenObjectParent.SetKitchenObject(this);
      transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
      transform.localPosition = Vector3.zero;
   }

   /// <summary>
   /// Удаляет предмет из текущего владельца и уничтожает объект сцены.
   /// </summary>
   public void DestroySelf()
   {
      kitchenObjectParent.ClearKitchenObject();
      Destroy(gameObject);
   }

   /// <summary>
   /// Проверяет, является ли предмет тарелкой.
   /// </summary>
   public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
   {
      if (this is PlateKitchenObject)
      {
         plateKitchenObject = this as PlateKitchenObject;
         return true;
      }
      else
      {
         plateKitchenObject = null;
         return false;
      }
   }

   /// <summary>
   /// Создает кухонный предмет из префаба и назначает ему владельца.
   /// </summary>
   public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
   {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
            return kitchenObject;
   }
}
