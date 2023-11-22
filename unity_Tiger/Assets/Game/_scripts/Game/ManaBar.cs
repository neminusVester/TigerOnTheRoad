using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private Image fillManaImage;
    private float _maxMana = 1;
    private float _currentMana;
    private float _minMana = 0;
    [SerializeField] private float restoringManaSpeed = 0.007f;
    [SerializeField] private float usageManaSpeed = 0.02f;
    private bool _isHolding;
    private bool _isFreeMana = false;

    private void Start()
    {
        _currentMana = _maxMana;
        GameEvents.Instance.OnHolding += CheckHoldingFinger;
        GameEvents.Instance.OnFreeMana +=FreeMana;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnHolding -= CheckHoldingFinger;
    }

    private void FixedUpdate()
    {
        CountManaAmount();
    }

    private void CheckHoldingFinger(bool holding)
    {
        _isHolding = holding;
    }

    private void CountManaAmount()
    {
        if (PlayerController.IsInGame)
        {
            if (!_isFreeMana)
            {
                if (_isHolding)
                {
                    DecreaseManaLevel();
                }
                else
                {
                    IncreaseManaLevel();
                }
            }
            else
            {
                IncreaseManaLevel();
            }

            SetManaFill(_currentMana);
        }
    }

    private void DecreaseManaLevel()
    {
        if (_currentMana >= _minMana)
            _currentMana -= usageManaSpeed;
    }

    private void IncreaseManaLevel()
    {
        if (_currentMana <= _maxMana)
        {
            _currentMana += restoringManaSpeed;
        }
    }

    private void FreeMana(bool isFree)
    {
        _isFreeMana = isFree;
    }

    private void SetManaFill(float fillAmount)
    {
        fillManaImage.fillAmount = Mathf.Clamp(fillAmount, 0f, 1f);
        if (fillManaImage.fillAmount <= 0)
        {
            GameEvents.Instance.ManaIsOver();
        }
    }
}

