using UnityEngine;

public class AICharacterDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AICharacter overlappedAICharacter;
        if (other.TryGetComponent<AICharacter>(out overlappedAICharacter))
        {
            overlappedAICharacter.DestroySelf();
        }
    }

}
