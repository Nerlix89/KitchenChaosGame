using UnityEngine;

public class PlayerCharacterAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] PlayerCharacter playerCharacter;
    [SerializeField] AICharacter aiCharacter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerCharacter)
        {
            animator.SetBool(IS_WALKING, playerCharacter.GetIsWalking());
        }
        if (aiCharacter)
        {
            animator.SetBool(IS_WALKING, aiCharacter.GetIsWalking());
        }

    }
}
