using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class TeleportCase
{
    public GameObject Caller { get; private set; }
    public UnityAction OnTeleport { get; private set; }

    private float _teleportAccounted = 0;

    public UnityEvent Teleported { get; private set; } = new();

    public Vector2 TeleportSource { get; set; }
    public Vector2 TeleportTarget { get; set; }
    public float TeleportTime { get; set; }

    public bool done = false;

    public TeleportCase(GameObject caller, UnityAction onTeleport)
    {
        Caller = caller;
        OnTeleport = onTeleport;

        if (onTeleport != null)
        {
            Teleported.AddListener(onTeleport);
        }
    }

    public Vector2 ProgressTeleport()
    {
        _teleportAccounted += Time.deltaTime;

        Vector2 newPos = Vector2.Lerp(TeleportSource, TeleportTarget, _teleportAccounted / TeleportTime);

        if (_teleportAccounted >= TeleportTime)
        {
            done = true;
        }

        return newPos;
    }
}

public class CharacterTeleport : MonoBehaviour
{
    [SerializeField] private Light2D _soul;

    private Rigidbody2D _body;
    private Renderer _renderer;

    private Renderer[] _objectsToDisable;
    private Behaviour[] _behavioursToDisable;

    private List<TeleportCase> teleportCases = new();

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (teleportCases.Exists(c => c.done))
        {
            FinishTeleporting();
        }

        if (teleportCases.Count > 0)
        {
            IsTeleporting();
        }
    }

    private void SwitchRenderers(bool enabled)
    {

        foreach (Renderer objectToDisable in _objectsToDisable)
        {
            objectToDisable.enabled = enabled;
        }

        foreach (Behaviour behaviourToDisable in _behavioursToDisable)
        {
            behaviourToDisable.enabled = enabled;
        }
    }

    private void FinishTeleporting()
    {
        List<TeleportCase> caseCopy = new(teleportCases.Where(teleportCase => teleportCase.done).ToList());

        foreach (TeleportCase teleportCase in caseCopy.Where(teleportCase => teleportCase.done))
        {
            teleportCase.Teleported.Invoke();
        }

        teleportCases = teleportCases.Where(teleportCase => !teleportCase.done).ToList();

        if (teleportCases.Count == 0)
        {
            _body.simulated = true;

            SwitchRenderers(true);
        }
    }

    public void Teleport(Vector2 position, Behaviour source, GameObject caller, UnityAction onTeleported = null, bool hideWhileTeleporting = true, float time = 1)
    {
        TeleportCase teleportCase = new(caller, onTeleported);

        _objectsToDisable = hideWhileTeleporting ? new Renderer[] { _renderer } : new Renderer[0]; ;
        _behavioursToDisable = hideWhileTeleporting ? new Behaviour[] { _soul, source } : new Behaviour[0]; ;

        SwitchRenderers(false);

        _body.velocity = Vector2.zero;
        _body.simulated = false;

        teleportCase.TeleportSource = transform.position;
        teleportCase.TeleportTarget = position;
        teleportCase.TeleportTime = time;

        teleportCases.Add(teleportCase);
    }

    public void IsTeleporting()
    {
        foreach (TeleportCase teleportCase in teleportCases)
        {
            transform.position = teleportCase.ProgressTeleport();
        }
    }
}

