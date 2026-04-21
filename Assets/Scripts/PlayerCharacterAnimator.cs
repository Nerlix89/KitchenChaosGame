using UnityEngine;

/// <summary>
/// Обновляет параметры Animator для персонажа игрока или AI.
/// </summary>
public class PlayerCharacterAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] PlayerCharacter playerCharacter;
    [SerializeField] AICharacter aiCharacter;
    private Animator animator;

    /// <summary>
    /// Получает компонент Animator на этом объекте.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Передает в Animator состояние ходьбы выбранного персонажа.
    /// </summary>
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
