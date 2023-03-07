using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class CharacterTeleport : MonoBehaviour
{
    [SerializeField] private Light2D _soul;

    private Rigidbody2D _body;
    private Renderer _renderer;

    private Vector2 _teleportTarget;
    private Vector2 _teleportSource;
    private float _teleportAccounted;
    private float _teleportTime;

    private Renderer[] _objectsToDisable;
    private Behaviour[] _behavioursToDisable;
    private bool _isTeleporting = false;

    public UnityEvent Teleported { get; private set; } = new();

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        if (_isTeleporting)
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
        _teleportAccounted = 0;

        _body.simulated = true;

        SwitchRenderers(true);

        _isTeleporting = false;

        Teleported.Invoke();

        Teleported.RemoveAllListeners();
    }

    public void Teleport(Vector2 position, UnityAction onTeleported = null, bool hideWhileTeleporting = true, float time = 1)
    {
        if (_isTeleporting)
        {
            return;
        }

        _isTeleporting = true;

        _objectsToDisable = hideWhileTeleporting ? new Renderer[] { _renderer } : new Renderer[0]; ;
        _behavioursToDisable = hideWhileTeleporting ? new Behaviour[] { _soul, this } : new Behaviour[0]; ;

        SwitchRenderers(false);

        _body.velocity = Vector2.zero;
        _body.simulated = false;

        _teleportSource = transform.position;
        _teleportTarget = position;
        _teleportTime = time;

        if (onTeleported != null)
        {
            Teleported.AddListener(onTeleported);
        }
    }

    public void IsTeleporting()
    {
        if (_isTeleporting)
        {
            _teleportAccounted += Time.fixedDeltaTime;

            transform.position = Vector2.Lerp(_teleportSource, _teleportTarget, _teleportAccounted / _teleportTime);

            if (_teleportAccounted >= _teleportTime)
            {
                FinishTeleporting();
            }
        }
    }
}

