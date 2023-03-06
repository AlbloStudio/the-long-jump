using System.Collections;
using UnityEngine;

public class Teleportable : MonoBehaviour
{
    private Rigidbody2D _body;

    private Vector2 _teleportTarget;
    private Vector2 _teleportSource;
    private float _teleportAccounted;
    private float _teleportTime;

    private Renderer[] _objectsToDisable;
    private Behaviour[] _behavioursToDisable;
    private GameObject _teleportCause;
    private bool _isTeleporting = false;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_teleportCause != null)
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

    private IEnumerator FinishTeleporting()
    {
        _teleportAccounted = 0;

        _body.simulated = true;

        SwitchRenderers(true);

        _isTeleporting = false;

        yield return new WaitForSeconds(1f);

        _teleportCause = null;

    }

    public void Teleport(Vector2 position, Renderer[] renderersToDisable, Behaviour[] behavioursToDisable, GameObject cause, float time = 1)
    {
        if (_teleportCause == cause)
        {
            return;
        }

        _isTeleporting = true;

        _objectsToDisable = renderersToDisable;
        _behavioursToDisable = behavioursToDisable;

        SwitchRenderers(false);

        _body.velocity = Vector2.zero;
        _body.simulated = false;

        _teleportCause = cause;
        _teleportSource = transform.position;
        _teleportTarget = position;
        _teleportTime = time;
    }

    public void IsTeleporting()
    {
        if (_isTeleporting)
        {
            _teleportAccounted += Time.fixedDeltaTime;

            transform.position = Vector2.Lerp(_teleportSource, _teleportTarget, _teleportAccounted / _teleportTime);

            if (_teleportAccounted >= _teleportTime)
            {
                _ = StartCoroutine(FinishTeleporting());
            }
        }
    }
}

