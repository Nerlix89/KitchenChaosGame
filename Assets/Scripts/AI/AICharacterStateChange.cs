using UnityEngine;

public class AICharacterStateChange : MonoBehaviour
{
    [SerializeField] private AICharacter.CharacterState newState;
    private void OnTriggerEnter(Collider other)
    {
        AICharacter overlappedAICharacter;
        if (other.TryGetComponent<AICharacter>(out overlappedAICharacter))
        {
            overlappedAICharacter.SetCharacterState(newState);
        }
    }

}
