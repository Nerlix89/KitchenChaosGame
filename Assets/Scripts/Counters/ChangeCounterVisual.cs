using UnityEngine;

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

    void Start()
    {
       PlayerCharacter.Instance.OnSelectedCounterChanged += PlayerCharacter_OnSelectedCounterChanged;
    }

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

    private void ChangeOutlineVisibility(bool newVisibility)
    {
        foreach (GameObject visualGameObject in visualGameObjects)
        {
            if (visualGameObject == null) continue;

            visualGameObject.SetActive(newVisibility);

        }
    }
}
