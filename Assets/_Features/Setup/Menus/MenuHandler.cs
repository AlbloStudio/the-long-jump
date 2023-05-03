using Assets.Scripts.managers;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static Enum;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] protected float _activateAfterTime = 0;
    [SerializeField] protected Vector2 _dofRange = new(1f, 25f);
    [SerializeField] protected Vector2 _fadeTimes = new(3f, 1.5f);
    [SerializeField] private string _activationButton;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private bool _activateOnlyOnce = false;

    protected readonly StateMachine<StartState> _state = new(StartState.FadedOut);
    protected float _fadeTimeCount = 0;

    private DepthOfField _depthOfField;
    private ClampedFloatParameter _minFloatParameter;
    private Collider2D _activationTriggerCollider;
    private bool _alreadyActivated = false;

    protected virtual void Awake()
    {
        _ = MenuManager.Instance.BlurVolume.profile.TryGet(out _depthOfField);
        _minFloatParameter = _depthOfField.focalLength;

        _activationTriggerCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_activationTriggerCollider == null || !_state.IsInState(StartState.FadedOut))
        {
            return;
        }

        bool activatedByTrigger = GeneralData.Instance.Player.GetComponent<Collider2D>() == other;
        if (activatedByTrigger)
        {
            _ = StartCoroutine(ActivateMenu());
        }
    }

    protected virtual void Update()
    {
        if (_state.IsInState(StartState.FadedOut))
        {
            WhileFadedOut();
        }
        else if (_state.IsInState(StartState.FadingIn))
        {
            WhileFadingIn();
        }
        else if (_state.IsInState(StartState.FadedIn))
        {
            _ = StartCoroutine(WhileFadedIn());
        }
        else if (_state.IsInState(StartState.FadingOut))
        {
            WhileFadingOut();
        }
    }

    protected virtual void WhileFadingIn()
    {
        _fadeTimeCount += Time.deltaTime;
        _minFloatParameter.value = GetIncrement(_dofRange, _fadeTimes.x);

        _canvasGroup.alpha = GetIncrement(new(0, 1), _fadeTimes.x);

        if (_fadeTimeCount >= _fadeTimes.x)
        {
            OnFadedIn();
        }
    }

    protected virtual void WhileFadingOut()
    {
        _fadeTimeCount -= Time.deltaTime;
        _minFloatParameter.value = GetIncrement(_dofRange, _fadeTimes.y);

        _canvasGroup.alpha = GetIncrement(new(0, 1), _fadeTimes.y);

        if (_fadeTimeCount <= 0)
        {
            OnFadedOut();
        }
    }

    protected virtual IEnumerator WhileFadedIn()
    {
        yield return null;
    }

    protected virtual void WhileFadedOut()
    {
        if (_activationButton != "" && Input.GetButtonDown(_activationButton))
        {
            _ = StartCoroutine(ActivateMenu());
        }
    }

    protected virtual void OnFadingIn()
    {
        _fadeTimeCount = 0;
    }

    protected virtual void OnFadingOut()
    {
        _fadeTimeCount = _fadeTimes.y;
    }

    protected virtual void OnFadedIn()
    {
        _fadeTimeCount = _fadeTimes.x;
        _state.ChangeState(StartState.FadedIn);

        _minFloatParameter.value = _dofRange.y;
    }

    protected virtual void OnFadedOut()
    {
        _fadeTimeCount = 0;
        _state.ChangeState(StartState.FadedOut);

        _minFloatParameter.value = _dofRange.x;

        _canvasGroup.gameObject.SetActive(false);

        MenuManager.Instance.CurrentMenuHandler = null;
    }

    private float GetIncrement(Vector2 range, float time)
    {
        return Mathf.Lerp(range.x, range.y, _fadeTimeCount / time);
    }

    public IEnumerator ActivateMenu()
    {
        bool canActivate = !(_activateOnlyOnce && _alreadyActivated);

        bool isMenuHandlerFreeOfUse = !(
            MenuManager.Instance.CurrentMenuHandler != null &&
            MenuManager.Instance.CurrentMenuHandler != this
           );

        if (canActivate && isMenuHandlerFreeOfUse)
        {

            yield return new WaitForSeconds(_activateAfterTime);

            MenuManager.Instance.CurrentMenuHandler = this;

            _canvasGroup.gameObject.SetActive(true);
            _canvasGroup.alpha = 0;

            _state.ChangeState(StartState.FadingIn);

            _alreadyActivated = true;

            OnFadingIn();
        }
    }

    public void DeactivateMenu()
    {
        _canvasGroup.alpha = 1;

        _state.ChangeState(StartState.FadingOut);
        OnFadingOut();
    }
}
