using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip passObstacleSound;
    [SerializeField] private AudioClip collisionObstacleSound;
    [SerializeField] private ParticleSystem scalingEffect;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float scalingSpeed;
    private Vector3 _maximumScale;
    private Vector3 _minimumScale = new Vector3(0.1f, 0.1f, 0.1f);
    private bool _isHolding = false;
    private bool _isManaOver = false;
    private Rigidbody2D _playerRigidbody;
    private Renderer _playerRenderer;
    public static bool IsInGame = false;

    private void Start()
    {
        playerAnimator.speed = 0f;
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerRenderer = GetComponent<Renderer>();
        _maximumScale = transform.localScale;
        scalingEffect.gameObject.SetActive(false);
        GameEvents.Instance.OnHolding += CheckHoldingFinger;
        GameEvents.Instance.OnInvincibility += InvinciblePlayer;
        GameEvents.Instance.OnManaEmpty += CheckManaIsOver;
        GameEvents.Instance.OnGameStarted += SetInGame;
        GameEvents.Instance.OnLostGameContinues += StartReviveRoutine;

        GameEvents.Instance.OnSaveZonePassed += PlayObstaclePassSound;
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnHolding -= CheckHoldingFinger;
        GameEvents.Instance.OnInvincibility -= InvinciblePlayer;
        GameEvents.Instance.OnManaEmpty -= CheckManaIsOver;
        GameEvents.Instance.OnGameStarted -= SetInGame;
        GameEvents.Instance.OnLostGameContinues -= StartReviveRoutine;

        GameEvents.Instance.OnSaveZonePassed += PlayObstaclePassSound;
    }

    private void FixedUpdate()
    {
        if (IsInGame)
        {
            playerAnimator.speed = 1f;
            if (_isHolding && !_isManaOver)
            {
                ScalePlayer(_minimumScale);
                scalingEffect.gameObject.SetActive(true);
            }
            else
            {
                ScalePlayer(_maximumScale);
                scalingEffect.gameObject.SetActive(false);
            }
            if (!_isHolding)
            {
                _isManaOver = false;
            }
        }
        else
        {
            scalingEffect.gameObject.SetActive(false);
            playerAnimator.speed = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SoundController.Instance.PlayEffectSound(collisionObstacleSound);
        GameEvents.Instance.PlayerLoseGame();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "SaveZone")
        {
            GameEvents.Instance.PlayerPasedObstacle();
            Destroy(collider.gameObject);
        }
    }

    private void PlayObstaclePassSound()
    {
        SoundController.Instance.PlayEffectSound(passObstacleSound);
    }

    private void CheckHoldingFinger(bool holding)
    {
        _isHolding = holding;
    }

    private void CheckManaIsOver()
    {
        _isManaOver = true;
    }

    private void ScalePlayer(Vector3 targetScale)
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.fixedDeltaTime * 1.5f);
    }

    private void StartReviveRoutine()
    {
        StartCoroutine(PlayerStateAfterRevive());
    }

    private IEnumerator PlayerStateAfterRevive()
    {
        MakePlayerInvincible();
        yield return new WaitForSeconds(10f);
        MakePlayerVincible();
    }

    private void InvinciblePlayer(bool isInvincible)
    {
        if (isInvincible)
        {
            MakePlayerInvincible();
        }
        else
        {
            MakePlayerVincible();
        }
    }

    private void MakePlayerInvincible()
    {
        _playerRigidbody.isKinematic = true;
        StartCoroutine(BlinkPlayer());
    }

    private void MakePlayerVincible()
    {
        _playerRigidbody.isKinematic = false;
    }

    private IEnumerator BlinkPlayer()
    {
        var blinkDuration = 5f;

        while (true)
        {
            _playerRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            _playerRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);

            blinkDuration -= 0.2f;

            if (blinkDuration <= 0)
            {
                break;
            }
        }
    }

    private void SetInGame()
    {
        IsInGame = true;
    }
}
