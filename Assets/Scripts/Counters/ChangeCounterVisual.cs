using UnityEngine;

/// <summary>
/// Меняет визуальное состояние стойки, когда игрок выбирает ее для взаимодействия.
/// </summary>
public class ChangeCounterVisual : MonoBehaviour
{
    private enum ChangeMode
    {
        UseMaterial,
        UseOutline,
    }

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjects;
    [SerializeField] ChangeMode changeMode; 

    [Header("Materials")]
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material interactMaterial;

    /// <summary>
    /// Подписывается на событие смены выбранной стойки игроком.
    /// </summary>
    void Start()
    {
       PlayerCharacter.Instance.OnSelectedCounterChanged += PlayerCharacter_OnSelectedCounterChanged;
    }

    /// <summary>
    /// Обновляет материал или контур в зависимости от выбранной стойки.
    /// </summary>
    private void PlayerCharacter_OnSelectedCounterChanged(BaseCounter selectedCounter)
    {
        switch (changeMode)
        {
            case ChangeMode.UseMaterial:

                if (selectedCounter == baseCounter)
                {
                    ChangeMaterial(interactMaterial);
                }
                else
                {
                    ChangeMaterial(baseMaterial);
                }
                break;

            case ChangeMode.UseOutline:

                if (selectedCounter == baseCounter)
                {
                    ChangeOutlineVisibility(true);
                }
                else
                {
                    ChangeOutlineVisibility(false);
                }
                break;
        }
    }

    /// <summary>
    /// Применяет материал ко всем визуальным объектам стойки.
    /// </summary>
    private void ChangeMaterial(Material newMaterial)
    {
        foreach (GameObject visualGameObject in visualGameObjects)
        {
            if (visualGameObject == null) continue;

            MeshRenderer meshRenderer = visualGameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = newMaterial;
            }

        }
    }

    /// <summary>
    /// Включает или выключает объекты контура стойки.
    /// </summary>
    private void ChangeOutlineVisibility(bool newVisibility)
    {
        foreach (GameObject visualGameObject in visualGameObjects)
        {
            if (visualGameObject == null) continue;

            visualGameObject.SetActive(newVisibility);

        }
    }
}
