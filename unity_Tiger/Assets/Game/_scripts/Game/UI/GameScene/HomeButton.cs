using UnityEngine;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour
{
    private Button _homeButton;

    private void Start()
    {
        _homeButton = GetComponent<Button>();
        _homeButton.onClick.AddListener(GoHomeScene);
    }

    private void GoHomeScene()
    {
        GameEvents.Instance.PlayerPressHome();
    }
}
