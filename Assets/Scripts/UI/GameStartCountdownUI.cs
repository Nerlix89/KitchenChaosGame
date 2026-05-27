using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI startCountdownText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        HideUI();
    }

    private void GameManager_OnStateChanged()
    {
        if (GameManager.Instance.IsCountdownToStart())
        {
            ShowUI();
        }
        else
        {
            HideUI();
        }
    }

    private void Update()
    {
        startCountdownText.text = Mathf.Ceil(GameManager.Instance.GetCountdownToStartTimer()).ToString();
    }

    private void ShowUI()
    {
        gameObject.SetActive(true);
    }

    private void HideUI()
    {
        gameObject.SetActive(false);
    }
}
