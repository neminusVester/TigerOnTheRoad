using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private EventTrigger eventTrigger;
    [SerializeField] private GameObject[] tutorialPanels;
    private int _currentPanelIndex = 0;

    private void Start()
    {
        ShowPanel(_currentPanelIndex);
        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((data) => { OnPointerUpDelegate((PointerEventData)data); });
        eventTrigger.triggers.Add(pointerUpEntry);
    }

    public void OnPointerUpDelegate(PointerEventData data)
    {
        ShowNextPanel();
    }

    private void ShowNextPanel()
    {
        HidePanel(_currentPanelIndex);
        _currentPanelIndex++;
        if (_currentPanelIndex >= tutorialPanels.Length)
        {
            GameEvents.Instance.ActivateDelayPanel();
            SLS.Data.GameData.TutorialCompleted.Value = true;
        }
        else
        {
            ShowPanel(_currentPanelIndex);
        }
    }

    private void ShowPanel(int index)
    {
        tutorialPanels[index].SetActive(true);
    }

    private void HidePanel(int index)
    {
        tutorialPanels[index].SetActive(false);
    }
}
