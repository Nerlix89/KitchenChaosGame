using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Отображает прогресс объекта, реализующего IHasProgress.
/// </summary>
public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    /// <summary>
    /// Находит источник прогресса, подписывается на событие и скрывает шкалу.
    /// </summary>
    private void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError(hasProgressGameObject + "doesnt implement IHasProgress");
            return;
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    /// <summary>
    /// Обновляет заполнение шкалы и видимость по текущему прогрессу.
    /// </summary>
    private void HasProgress_OnProgressChanged(float currentProgress)
    {
        barImage.fillAmount = currentProgress;

        if (currentProgress == 0f || currentProgress == 1f)
        {
            Hide();
        }
        else
        {
           Show();
        }
    }

    /// <summary>
    /// Показывает шкалу прогресса.
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Скрывает шкалу прогресса.
    /// </summary>
    private void Hide()
    {
         gameObject.SetActive(false);
    }
}
