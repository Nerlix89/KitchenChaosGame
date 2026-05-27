using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        HideUI();
    }

    private void GameManager_OnStateChanged()
    {
        if (GameManager.Instance.IsGameOver())
        {
            ShowUI();
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfuulRecipesAmount().ToString();
        }
        else
        {
            HideUI();
        }
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
