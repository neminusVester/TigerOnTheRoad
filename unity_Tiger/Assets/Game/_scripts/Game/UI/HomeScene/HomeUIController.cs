using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject privacyPanel;
    [SerializeField] private GameObject shopPanel;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button closeSettingsButton;
    [SerializeField] private Button privacyButton;
    [SerializeField] private Button closePrivacyButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button closeShopButton;

    [Header("Texts")]
    [SerializeField] private Text coinsAmountText;

    private void Start()
    {
        SLS.Data.TotalCoinsAmount.OnValueChanged += SetCoinsAmountText;

        SetCoinsAmountText(SLS.Data.TotalCoinsAmount.Value);
        
        playButton.onClick.AddListener(StartPlay);
        settingsButton.onClick.AddListener(OpenSettings);
        closeSettingsButton.onClick.AddListener(CloseSettings);
        privacyButton.onClick.AddListener(OpenPrivacy);
        closePrivacyButton.onClick.AddListener(ClosePrivacy);
        shopButton.onClick.AddListener(OpenShop);
        closeShopButton.onClick.AddListener(CloseShop);
    }

    private void OnDestroy()
    {
        SLS.Data.TotalCoinsAmount.OnValueChanged -= SetCoinsAmountText;
    }

    private void StartPlay()
    {
        GameEvents.Instance.PlayerPressPlay();
    }

    private void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    private void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    private void OpenPrivacy()
    {
        privacyPanel.SetActive(true);
    }

    private void ClosePrivacy()
    {
        privacyPanel.SetActive(false);
    }

    private void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    private void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    private void SetCoinsAmountText(int newValue)
    {
        // coinsAmountText.text = SLS.Data.TotalCoinsAmount.Value.ToString();
        coinsAmountText.text = newValue.ToString();
    }

}
