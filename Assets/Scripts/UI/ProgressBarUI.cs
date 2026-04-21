using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

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

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
         gameObject.SetActive(false);
    }
}
