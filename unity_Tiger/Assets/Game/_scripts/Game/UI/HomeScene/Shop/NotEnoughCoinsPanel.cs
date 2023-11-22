using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughCoinsPanel : MonoBehaviour
{
    [SerializeField] private Image tableImage;
    [SerializeField] private Text infoText;
    private float _minAlpha = 0f;
    private float _maxAlpha = 1f;
    private float _changeAlphaSpeed = 0.5f;

    private void OnEnable()
    {
        InitializeAlpha();
        ShowNotEnoughInformation();
    }

    private void InitializeAlpha()
    {
        tableImage.color = SetAlpha(tableImage.color, _minAlpha);
        infoText.color = SetAlpha(infoText.color, _minAlpha);
    }

    private Color SetAlpha(Color mainColor, float targetAlpha)
    {
        var startColor = mainColor;
        startColor.a = targetAlpha;
        return mainColor = startColor;
    }

    private void ShowNotEnoughInformation()
    {
        StopAllCoroutines();
        StartCoroutine(DecreaseAlpha());
    }

    private IEnumerator DecreaseAlpha()
    {
        tableImage.color = SetAlpha(tableImage.color, _maxAlpha);
        infoText.color = SetAlpha(infoText.color, _maxAlpha);

        yield return new WaitForSeconds(0.5f);

        float currentAlphaText = infoText.color.a; 
        float currentAlphaImage = tableImage.color.a;

        while (!Mathf.Approximately(currentAlphaText, _minAlpha) || !Mathf.Approximately(currentAlphaImage, _minAlpha))
        {
            currentAlphaText = Mathf.MoveTowards(currentAlphaText, _minAlpha, Time.deltaTime * _changeAlphaSpeed);
            currentAlphaImage = Mathf.MoveTowards(currentAlphaImage, _minAlpha, Time.deltaTime * _changeAlphaSpeed);

            infoText.color = SetAlpha(infoText.color, currentAlphaText);
            tableImage.color = SetAlpha(tableImage.color, currentAlphaImage);

            yield return null;
        }

        this.gameObject.SetActive(false);
    }
}
