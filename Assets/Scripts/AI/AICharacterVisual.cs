using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Настраивает внешний вид AI-посетителя.
/// </summary>
public class AICharacterVisual : MonoBehaviour
{
    [SerializeField] private List<GameObject> bodyParts;
    [SerializeField] private List<Material> characterMaterials;

    /// <summary>
    /// Применяет случайный материал при создании посетителя.
    /// </summary>
    private void Start()
    {
        UpdateVisual();
    }
    
    /// <summary>
    /// Выбирает материал и применяет его ко всем частям тела.
    /// </summary>
    private void UpdateVisual()
    {
        Material randomCharacterMaterial = characterMaterials[UnityEngine.Random.Range(0, characterMaterials.Count)];

        foreach (GameObject bodyPart in bodyParts)
        {
            bodyPart.GetComponent<MeshRenderer>().material = randomCharacterMaterial;
        }
    }
    
}
