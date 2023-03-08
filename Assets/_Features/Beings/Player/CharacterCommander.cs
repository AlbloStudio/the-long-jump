using Assets.Scripts.being;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class CommanderCase
{
    public UnityAction OnStart;
    public UnityAction OnUpdate;
    public UnityAction OnEnd;
    public float Time = 1f;
    public Vector3 Target;
    public Vector3 Source;
    public bool ShouldShow = false;
}

public class CharacterCommander : MonoBehaviour
{
    [SerializeField] private Light2D _soul;

    private readonly Queue<CommanderCase> _commanderCases = new();

    private Renderer _renderer;
    private CharacterMover _characterMover;
    private CommanderCase _currentCommanderCase = null;
    private Rigidbody2D _body;

    private float timeCount = 0;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _characterMover = GetComponent<CharacterMover>();
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_currentCommanderCase != null)
        {
            timeCount += Time.deltaTime;

            _currentCommanderCase?.OnUpdate?.Invoke();

            ProgressTeleport();

            if (timeCount >= _currentCommanderCase.Time)
            {
                EndCommand();

                if (_commanderCases.Count > 0)
                {
                    StartNewCommand();
                }
            }
        }
        else if (_commanderCases.Count > 0)
        {
            StartNewCommand();
        }
    }

    private void StartNewCommand()
    {
        _currentCommanderCase = _commanderCases.Dequeue();
        if (_currentCommanderCase.Source != null)
        {
            _currentCommanderCase.Source = transform.position;
        }

        SwitchRenderers(_currentCommanderCase.ShouldShow);
        _body.velocity = Vector2.zero;
        _body.simulated = false;

        _currentCommanderCase?.OnStart?.Invoke();
    }

    private void EndCommand()
    {
        timeCount = 0;

        _currentCommanderCase?.OnEnd?.Invoke();

        SwitchRenderers(true);
        _body.simulated = true;

        _currentCommanderCase = null;
    }

    private void SwitchRenderers(bool enabled)
    {
        _soul.enabled = enabled;
        _renderer.enabled = enabled;
        _characterMover.enabled = enabled;
    }

    private void ProgressTeleport()
    {
        transform.position = Vector2.Lerp(_currentCommanderCase.Source, _currentCommanderCase.Target, timeCount / _currentCommanderCase.Time);
    }

    public void AddCommand(CommanderCase commanderCase)
    {
        _commanderCases.Enqueue(commanderCase);
    }
}