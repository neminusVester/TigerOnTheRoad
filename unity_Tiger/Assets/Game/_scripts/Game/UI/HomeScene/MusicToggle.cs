using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    [SerializeField] private Sprite onToggleSprite;
    [SerializeField] private Sprite offToggleSprite;
    [SerializeField] private Button button;
    private Image _toggleImage;

    private void Start()
    {
        _toggleImage = this.GetComponent<Image>();
        SetToggleSprite(SLS.Data.Settings.MusicMuted.Value);
        button.onClick.AddListener(ChangeMusicToggleState);
    }

    private void ChangeMusicToggleState()
    {
        SoundController.Instance.ToggleMusic();
        SetToggleSprite(SLS.Data.Settings.MusicMuted.Value);
    }

    private void SetToggleSprite(bool musicMuted)
    {
        if (musicMuted)
        {
            _toggleImage.sprite = offToggleSprite;
        }
        else
        {
            _toggleImage.sprite = onToggleSprite;
        }
    }
}
