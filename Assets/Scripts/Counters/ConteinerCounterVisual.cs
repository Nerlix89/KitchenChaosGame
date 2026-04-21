using UnityEngine;

public class ConteinerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject()
    {
        if (animator == null) return;

        animator.SetTrigger(OPEN_CLOSE);
    }


}
