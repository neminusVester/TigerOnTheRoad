using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DelayPanel : MonoBehaviour
{
    [SerializeField] private Text countdownDisplay;
    private int _countdownTime = 3;

    private void Start()
    {

    }

    private void OnEnable()
    {
        StartCoroutine(CountdownToStart());
    }

    private IEnumerator CountdownToStart()
    {
        while (_countdownTime > 0)
        {
            countdownDisplay.text = _countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            _countdownTime--;
        }

        countdownDisplay.text = "GO!";
        _countdownTime = 3;

        yield return new WaitForSeconds(1f);

        GameEvents.Instance.StartGame();

        this.gameObject.SetActive(false);
    }
}
