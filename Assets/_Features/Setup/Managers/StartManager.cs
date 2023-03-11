using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static Enum;
using Assets.Scripts.being;
using Cinemachine;
using Assets.Scripts.managers;

public class StartManager : MonoBehaviour
{
    [SerializeField] private bool _activateStartSequence = false;
    [SerializeField] private float _fadeInTime = 3f;
    [SerializeField] private float _fadeOutTime = 2f;
    [SerializeField] private Vector3 _initialCharPosition = new(2, 20, 0);
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _startButton;
    [SerializeField] private Volume _blurVolume;
    [SerializeField] private CinemachineVirtualCamera _startVirtualCamera;

    private DepthOfField _depthOfField;
    private ClampedFloatParameter _minFloatParameter;
    private TextMeshProUGUI _text;
    private CharacterMover _char;
    private Rigidbody2D _charBody;

    private readonly StateMachine<StartState> _state = new(StartState.Awaking);
    private Vector2 _dofRange = new(25, 1);
    private Vector2 _textAlphaRangeStart = new(1, 0);
    private Vector2 _textAlphaRangeAwake = new(-0.5f, 1);
    private float _timeAccounted = 0;
    private float _fallDeath;

    private void Awake()
    {
        if (!_activateStartSequence)
        {
            return;
        }

        _canvas.gameObject.SetActive(true);
        _startVirtualCamera.enabled = true;
    }

    private void Start()
    {
        if (!_activateStartSequence)
        {
            return;
        }

        StartButtonProps();
        StartVolumeProps();
        StartCharProps();
    }

    private void Update()
    {
        if (!_activateStartSequence)
        {
            return;
        }

        if (_state.IsInState(StartState.Awaking))
        {
            _timeAccounted += Time.deltaTime;

            _text.color = new Color(1, 1, 1, GetIncrement(_textAlphaRangeAwake, _fadeInTime / 1.1f));

            if (_timeAccounted >= _fadeInTime)
            {
                OnAwake();
            }
        }

        if (_state.IsInState(StartState.Starting))
        {
            _timeAccounted += Time.deltaTime;

            UpdateButtonProps();
            UpdateVolumeProps();

            if (_timeAccounted >= _fadeOutTime / 2)
            {
                _startVirtualCamera.enabled = false;
            }

            if (_timeAccounted >= _fadeOutTime)
            {
                OnDone();
            }
        }
    }

    public void OnPressStart()
    {
        if (!_activateStartSequence || !_state.IsInState(StartState.Ready))
        {
            return;
        }

        _charBody.simulated = true;

        _state.ChangeState(StartState.Starting);
    }

    private void StartButtonProps()
    {
        _text = _startButton.GetComponentInChildren<TextMeshProUGUI>();
        _text.color = new Color(1, 1, 1, 0);

        _startButton.onClick.AddListener(OnPressStart);
    }

    private void StartVolumeProps()
    {
        _ = _blurVolume.profile.TryGet(out _depthOfField);
        _minFloatParameter = _depthOfField.focalLength;

        _minFloatParameter.value = _dofRange.x;
    }

    private void StartCharProps()
    {
        _char = GeneralData.Instance.Player;
        _char.transform.position = _initialCharPosition;

        _charBody = _char.GetComponent<Rigidbody2D>();
        _charBody.simulated = false;

        _fallDeath = _char.FallDeath;
        _char.FallDeath = 999f;
    }

    private void UpdateButtonProps()
    {
        _text.color = new Color(1, 1, 1, GetIncrement(_textAlphaRangeStart, _fadeOutTime / 1.1f));
    }

    private void UpdateVolumeProps()
    {
        _minFloatParameter.value = GetIncrement(_dofRange, _fadeOutTime);
    }

    private void OnAwake()
    {
        _timeAccounted = 0;
        _state.ChangeState(StartState.Ready);
    }

    private void OnDone()
    {
        _timeAccounted = 0;
        _state.ChangeState(StartState.Started);
        _char.GetComponent<CharacterMover>().FallDeath = _fallDeath;

        _canvas.enabled = false;
    }

    private float GetIncrement(Vector2 range, float time)
    {
        return Mathf.Lerp(range.x, range.y, _timeAccounted / time);
    }
}
