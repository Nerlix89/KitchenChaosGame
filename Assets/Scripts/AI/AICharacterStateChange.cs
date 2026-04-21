using UnityEngine;

/// <summary>
/// Меняет состояние AI-посетителя при входе в триггер.
/// </summary>
public class AICharacterStateChange : MonoBehaviour
{
    [SerializeField] private AICharacter.CharacterState newState;

    /// <summary>
    /// Назначает новое состояние посетителю, который вошел в триггер.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        AICharacter overlappedAICharacter;
        if (other.TryGetComponent<AICharacter>(out overlappedAICharacter))
        {
            overlappedAICharacter.SetCharacterState(newState);
        }
    }

}
