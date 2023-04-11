using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.being;
using Assets.Scripts.managers;
using System.Collections;
using Cinemachine;

public class StartMenuHandler : MenuHandler
{
    [SerializeField] private Vector3 _initialCharPosition = new(2, 20, 0);
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private CinemachineVirtualCamera _startCamera;

    private CharacterMover _char;
    private Rigidbody2D _charBody;
    private float _fallDeath;

    protected override void Awake()
    {
        base.Awake();

        StartButtonProps();
        StartCharProps();

        _startCamera.enabled = true;

        _ = StartCoroutine(ActivateMenu());
    }

    protected override void WhileFadingIn()
    {
        base.WhileFadingIn();

        if (Input.GetButtonUp("Submit"))
        {
            OnPressStart();
        }
    }

    protected override IEnumerator WhileFadedIn()
    {
        _ = StartCoroutine(base.WhileFadedIn());

        if (Input.GetButtonUp("Submit"))
        {
            OnPressStart();
        }

        yield return null;
    }

    protected override void OnFadedOut()
    {
        base.OnFadedOut();

        _char.FallDeath = _fallDeath;
        _startCamera.enabled = false;
    }

    public void OnPressStart()
    {
        _startButton.onClick.RemoveAllListeners();
        _loadButton.onClick.RemoveAllListeners();

        _charBody.simulated = true;

        DeactivateMenu();
    }

    public void OnLoadPress()
    {
        OnPressStart();
        SaveSystem.Instance.Load();
    }

    private void StartButtonProps()
    {
        _startButton.onClick.AddListener(OnPressStart);
        _loadButton.onClick.AddListener(OnLoadPress);
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
}
