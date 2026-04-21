using UnityEngine;

/// <summary>
/// Уничтожает AI-посетителя при входе в триггер.
/// </summary>
public class AICharacterDestroyer : MonoBehaviour
{
    /// <summary>
    /// Проверяет вошедший объект и удаляет посетителя.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        AICharacter overlappedAICharacter;
        if (other.TryGetComponent<AICharacter>(out overlappedAICharacter))
        {
            overlappedAICharacter.DestroySelf();
        }
    }

}
