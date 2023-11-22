using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PulsateText : MonoBehaviour
{
    private Text _textComponent;
    private Color _textColor;
    private Sequence _scaleSequence;

    private void OnEnable()
    {
        _textComponent = GetComponent<Text>();
        _textColor = _textComponent.color;
        _scaleSequence = DOTween.Sequence();
        TextPulsation();
    }

    private void TextPulsation()
    {
        _scaleSequence.AppendCallback(DisappearText)
        .AppendInterval(2f)
        .AppendCallback(ReappearText)
        .AppendInterval(2f)
        .SetLoops(-1);
    }


    private void DisappearText()
    {
        _textComponent.DOColor(new Color(_textColor.r, _textColor.g, _textColor.b, 0), 2f);
    }

    private void ReappearText()
    {
        _textComponent.DOColor(_textColor, 2f);
    }

    public void KillTween()
    {
        _scaleSequence.Kill();
        this.gameObject.SetActive(false);
    }
}
