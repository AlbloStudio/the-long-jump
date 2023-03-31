using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static Enum;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] protected Vector2 _fadeTimes = new(3f, 1.5f);
    [SerializeField] private Collider2D _activationTriggerCollider;
    [SerializeField] private string _activationButton;
    [SerializeField] private CanvasGroup _canvasGroup;

    protected readonly StateMachine<StartState> _state = new(StartState.FadedOut);
    protected float _fadeTimeCount = 0;

    private DepthOfField _depthOfField;
    private ClampedFloatParameter _minFloatParameter;
    private Vector2 _dofRange = new(1f, 25f);

    protected virtual void Awake()
    {
        _ = MenuManager.Instance.BlurVolume.profile.TryGet(out _depthOfField);
        _minFloatParameter = _depthOfField.focalLength;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_activationTriggerCollider == null || !_state.IsInState(StartState.FadedOut))
        {
            return;
        }

        bool activatedByTrigger = other == _activationTriggerCollider;
        if (activatedByTrigger)
        {
            ActivateMenu();
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
            WhileFadedIn();
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

    protected virtual void WhileFadedIn()
    {
        //--
    }

    protected virtual void WhileFadedOut()
    {
        if (_activationButton != "" && Input.GetButtonDown(_activationButton))
        {
            ActivateMenu();
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
    }

    private float GetIncrement(Vector2 range, float time)
    {
        return Mathf.Lerp(range.x, range.y, _fadeTimeCount / time);
    }

    public void ActivateMenu()
    {
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.alpha = 0;

        _state.ChangeState(StartState.FadingIn);
        OnFadingIn();
    }

    public void DeactivateMenu()
    {
        _canvasGroup.alpha = 1;

        _state.ChangeState(StartState.FadingOut);
        OnFadingOut();
    }
}
