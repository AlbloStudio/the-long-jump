using System.Collections;
using UnityEngine;

public class InstructionsMenuHandler : MenuHandler
{
    [SerializeField] protected float _logoTime = 3;
    [SerializeField] private PauseMenuHandler pauseMenuHandler;

    private bool _askedForPause = false;

    protected override void WhileFadingIn()
    {
        base.WhileFadingIn();

        CheckPause();
    }

    protected override void OnFadedOut()
    {
        base.OnFadedOut();

        if (_askedForPause)
        {
            _ = StartCoroutine(pauseMenuHandler.ActivateMenu());
        }

        _askedForPause = false;
    }

    protected override IEnumerator WhileFadedIn()
    {
        _ = StartCoroutine(base.WhileFadedIn());

        CheckPause();

        yield return new WaitForSeconds(_logoTime);

        DeactivateMenu();
    }

    protected override void WhileFadingOut()
    {
        base.WhileFadingOut();

        CheckPause();
    }

    private void CheckPause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            _askedForPause = true;
            _fadeTimes = new Vector2(_fadeTimes.x, 0);
            DeactivateMenu();
        }
    }
}
