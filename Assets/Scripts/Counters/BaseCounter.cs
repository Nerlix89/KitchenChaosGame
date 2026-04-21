using UnityEngine;

/// <summary>
/// Базовый класс кухонной стойки, которая может хранить один кухонный предмет.
/// </summary>
public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    /// <summary>
    /// Выполняет основное взаимодействие игрока со стойкой.
    /// </summary>
    public virtual void Interact(PlayerCharacter player)
    {
    }

    /// <summary>
    /// Выполняет альтернативное взаимодействие игрока со стойкой.
    /// </summary>
    public virtual void InteractAlternative(PlayerCharacter player)
    {
    }

    /// <summary>
    /// Возвращает точку размещения предмета на стойке.
    /// </summary>
    public Transform GetKitchenObjectFollowTransform() {return counterTopPoint;}

    /// <summary>
    /// Возвращает предмет, который лежит на стойке.
    /// </summary>
    public KitchenObject GetKitchenObject() {return kitchenObject;}

    /// <summary>
    /// Назначает стойке новый кухонный предмет.
    /// </summary>
    public void SetKitchenObject(KitchenObject newKitchenObject) {kitchenObject = newKitchenObject;}

    /// <summary>
    /// Очищает ссылку на предмет на стойке.
    /// </summary>
    public void ClearKitchenObject() {kitchenObject = null;}

    /// <summary>
    /// Проверяет, есть ли на стойке кухонный предмет.
    /// </summary>
    public bool HasKitchenObject() {return kitchenObject != null;}
}
