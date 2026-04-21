using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] GameObject stoveOnGameObject;
    [SerializeField] GameObject particlesOnGameObject;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(StoveCounter.State newState)
    {
        bool showActive = newState == StoveCounter.State.Frying || newState == StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(showActive);
        particlesOnGameObject.SetActive(showActive);
    }
}
