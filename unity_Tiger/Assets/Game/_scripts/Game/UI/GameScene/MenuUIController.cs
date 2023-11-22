using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MenuUIController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject delayPanel;

    [Header("Text")]
    [SerializeField] private Text gameScoreText;
    [SerializeField] private Text totalGameScoreText;
    [SerializeField] private Text coinsAmount;
    [SerializeField] private Text reviveCostText;

    [Header("Buttons")]
    [SerializeField] private Button reviveToGameButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeGameButton;
    [SerializeField] private Button restartButton;

    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private PulsateText cautionText;
    private int _formatedScore;
    private int _reviveCost;

    private void Start()
    {
        InitializeCoinsTexts();
        reviveToGameButton.onClick.AddListener(ReviveToGame);
        pauseButton.onClick.AddListener(PauseGame);
        resumeGameButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);

        GameEvents.Instance.OnGameLosed += LoseGame;
        GameEvents.Instance.OnDelayPanelActivated += OpenDelayPanel;
        GameEvents.Instance.OnDelayPanelActivated += CloseTutorialPanel;
        GameEvents.Instance.OnLostGameContinues += OpenDelayPanel;
        GameEvents.Instance.OnStartedDynamicObstacles += StartCautionRoutine;

        if (!SLS.Data.GameData.TutorialCompleted.Value)
        {
            tutorialPanel.SetActive(true);
        }
        else
        {
            OpenDelayPanel();
        }
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnGameLosed -= LoseGame;
        GameEvents.Instance.OnDelayPanelActivated -= OpenDelayPanel;
        GameEvents.Instance.OnDelayPanelActivated -= CloseTutorialPanel;
        GameEvents.Instance.OnLostGameContinues -= OpenDelayPanel;
        GameEvents.Instance.OnStartedDynamicObstacles -= StartCautionRoutine;
    }

    private void Update()
    {
        gameScoreText.text = scoreManager.totalScore.ToString();
    }

    private void OpenDelayPanel()
    {
        delayPanel.SetActive(true);
    }

    private void CloseTutorialPanel()
    {
        tutorialPanel.SetActive(false);
    }

    private void RestartGame()
    {
        GameEvents.Instance.PlayerPressRestart();
    }

    private void InitializeCoinsTexts()
    {
        _reviveCost = SLS.Data.GameData.PriceReviveToGame.Value;
        reviveCostText.text = _reviveCost.ToString();
        coinsAmount.text = SLS.Data.TotalCoinsAmount.Value.ToString();
    }

    private void LoseGame()
    {
        PlayerController.IsInGame = false;
        SetTotalGameSCore();
        SetTotalCoinsAmount();
        losePanel.SetActive(true);
    }

    private void SetTotalGameSCore()
    {
        _formatedScore = (int)scoreManager.totalScore;
        totalGameScoreText.text = _formatedScore.ToString();
    }

    private void SetTotalCoinsAmount()
    {
        if (coinsAmount.text == SLS.Data.TotalCoinsAmount.Value.ToString())
        {
            coinsAmount.text = (SLS.Data.TotalCoinsAmount.Value + _formatedScore).ToString();
        }
        else
        {
            coinsAmount.text = SLS.Data.TotalCoinsAmount.Value.ToString();
        }
    }

    private void ReviveToGame()
    {
        if (SLS.Data.TotalCoinsAmount.Value >= _reviveCost)
        {
            GameEvents.Instance.PlayerSelectReviveButton();
            losePanel.SetActive(false);
            SLS.Data.TotalCoinsAmount.Value -= _reviveCost;
            SetTotalCoinsAmount();
        }

    }

    private void PauseGame()
    {
        GameEvents.Instance.PlayerStopGame();
        PlayerController.IsInGame = false;
        pausePanel.SetActive(true);
    }

    private void ResumeGame()
    {
        GameEvents.Instance.ActivateDelayPanel();
        pausePanel.SetActive(false);
    }

    private void StartCautionRoutine()
    {
        StartCoroutine(CautionText());
    }

    private IEnumerator CautionText()
    {
        Debug.Log("in UI");
        cautionText.gameObject.SetActive(true);
        yield return new WaitForSeconds(6f);
        cautionText.KillTween();
    }
}
