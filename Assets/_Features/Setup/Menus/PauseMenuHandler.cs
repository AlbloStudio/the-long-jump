using System.Collections;
using UnityEngine;

public class PauseMenuHandler : MenuHandler
{
    public bool CanPause = true;

    protected override void OnFadingOut()
    {
        base.OnFadingOut();

        Time.timeScale = 1;
    }

    protected override void OnFadedIn()
    {
        base.OnFadedIn();

        Time.timeScale = 0;
    }

    protected override void WhileFadedOut()
    {
        base.WhileFadedOut();

        if (CanPause && Input.GetButtonDown("Pause"))
        {
            _ = StartCoroutine(ActivateMenu());
        }
    }

    protected override IEnumerator WhileFadedIn()
    {
        _ = StartCoroutine(base.WhileFadedIn());

        if (Input.GetButtonDown("Pause"))
        {
            DeactivateMenu();
        }

        yield return null;
    }
}
