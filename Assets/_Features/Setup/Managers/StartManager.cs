using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static Enum;
using Assets.Scripts.managers;
using Assets.Scripts.being;
using Cinemachine;

public class StartManager : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Volume _blurVolume;
    [SerializeField] private float _fadeOutTime;
    [SerializeField] private CinemachineVirtualCamera _startVirtualCamera;

    private readonly StateMachine<StartState> _state = new(StartState.Ready);
    private DepthOfField _depthOfField;
    private TextMeshProUGUI _text;

    private Vector2 dofRange = new(25, 1);
    private Vector2 textAlphaRange = new(1, 0);

    private float _timeAccounted = 0;
    private float _fallDeath;

    // private void Awake()
    // {
    //     _startVirtualCamera.enabled = true;
    // }

    private void Start()
    {
        _text = _startButton.GetComponentInChildren<TextMeshProUGUI>();
        _text.color = new Color(1, 1, 1, 1);

        _ = _blurVolume.profile.TryGet(out _depthOfField);

        ClampedFloatParameter minFloatParameter = _depthOfField.focalLength;
        minFloatParameter.value = dofRange.x;

        _startButton.onClick.AddListener(OnPressStart);

        GeneralData.Instance.Player.GetComponent<Rigidbody2D>().simulated = false;
        _fallDeath = GeneralData.Instance.Player.GetComponent<CharacterMover>().FallDeath;
        GeneralData.Instance.Player.GetComponent<CharacterMover>().FallDeath = 999f;
    }

    private void Update()
    {
        if (_state.IsInState(StartState.Starting))
        {
            _timeAccounted += Time.deltaTime;

            ClampedFloatParameter minFloatParameter = _depthOfField.focalLength;
            minFloatParameter.value = GetIncrement(dofRange, _fadeOutTime);
            _text.color = new Color(1, 1, 1, GetIncrement(textAlphaRange, _fadeOutTime / 1.1f));

            if (_timeAccounted >= _fadeOutTime / 2)
            {
                _startVirtualCamera.enabled = false;
            }

            if (_timeAccounted >= _fadeOutTime)
            {

                _timeAccounted = 0;
                _state.ChangeState(StartState.Started);
                GeneralData.Instance.Player.GetComponent<CharacterMover>().FallDeath = _fallDeath;

                Destroy(_startButton.gameObject);
            }
        }
    }

    public void OnPressStart()
    {
        GeneralData.Instance.Player.GetComponent<Rigidbody2D>().simulated = true;

        _state.ChangeState(StartState.Starting);
    }

    private float GetIncrement(Vector2 range, float time)
    {
        return Mathf.Lerp(range.x, range.y, _timeAccounted / time);
    }
}
