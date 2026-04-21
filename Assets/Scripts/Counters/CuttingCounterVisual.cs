using UnityEngine;

/// <summary>
/// Управляет анимацией стойки нарезки.
/// </summary>
public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    [SerializeField] CuttingCounter cuttingCounter;
    private Animator animator;

    /// <summary>
    /// Получает компонент Animator стойки нарезки.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Подписывается на событие нарезки.
    /// </summary>
    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    /// <summary>
    /// Запускает анимацию удара ножом.
    /// </summary>
    private void CuttingCounter_OnCut()
    {
        if (animator == null) return;

        animator.SetTrigger(CUT);
    }
}
