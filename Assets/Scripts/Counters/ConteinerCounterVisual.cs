using UnityEngine;

/// <summary>
/// Управляет анимацией контейнера при выдаче предмета игроку.
/// </summary>
public class ConteinerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] ContainerCounter containerCounter;
    private Animator animator;

    /// <summary>
    /// Получает компонент Animator контейнера.
    /// </summary>
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Подписывается на событие получения предмета игроком.
    /// </summary>
    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    /// <summary>
    /// Запускает анимацию открытия и закрытия контейнера.
    /// </summary>
    private void ContainerCounter_OnPlayerGrabbedObject()
    {
        if (animator == null) return;

        animator.SetTrigger(OPEN_CLOSE);
    }


}
