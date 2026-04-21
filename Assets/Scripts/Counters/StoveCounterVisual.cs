using UnityEngine;

/// <summary>
/// Управляет визуальными эффектами включенной плиты.
/// </summary>
public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] GameObject stoveOnGameObject;
    [SerializeField] GameObject particlesOnGameObject;

    /// <summary>
    /// Подписывается на изменение состояния плиты.
    /// </summary>
    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    /// <summary>
    /// Показывает огонь и частицы, когда плита жарит или держит готовое блюдо.
    /// </summary>
    private void StoveCounter_OnStateChanged(StoveCounter.State newState)
    {
        bool showActive = newState == StoveCounter.State.Frying || newState == StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(showActive);
        particlesOnGameObject.SetActive(showActive);
    }
}
