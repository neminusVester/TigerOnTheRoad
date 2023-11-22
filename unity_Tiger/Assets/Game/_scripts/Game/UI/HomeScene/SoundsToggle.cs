using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsToggle : MonoBehaviour
{
    [SerializeField] private Sprite onToggleSprite;
    [SerializeField] private Sprite offToggleSprite;
    [SerializeField] private Button button;
    private Image _toggleImage;

    private void Start()
    {
        _toggleImage = this.GetComponent<Image>();
        SetToggleSprite(SLS.Data.Settings.SoundsMuted.Value);
        button.onClick.AddListener(ChangeSoundsToggleState);

    }

    private void ChangeSoundsToggleState()
    {
        SoundController.Instance.ToggleSounds();
        SetToggleSprite(SLS.Data.Settings.SoundsMuted.Value);
    }

    private void SetToggleSprite(bool soundsMuted)
    {
        if (soundsMuted)
        {
            _toggleImage.sprite = offToggleSprite;
        }
        else
        {
            _toggleImage.sprite = onToggleSprite;
        }
    }
}
