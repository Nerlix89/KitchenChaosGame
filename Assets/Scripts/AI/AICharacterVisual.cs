using System.Collections.Generic;
using UnityEngine;

public class AICharacterVisual : MonoBehaviour
{
    [SerializeField] private List<GameObject> bodyParts;
    [SerializeField] private List<Material> characterMaterials;

    private void Start()
    {
        UpdateVisual();
    }
    
    private void UpdateVisual()
    {
        Material randomCharacterMaterial = characterMaterials[UnityEngine.Random.Range(0, characterMaterials.Count)];

        foreach (GameObject bodyPart in bodyParts)
        {
            bodyPart.GetComponent<MeshRenderer>().material = randomCharacterMaterial;
        }
    }
    
}
