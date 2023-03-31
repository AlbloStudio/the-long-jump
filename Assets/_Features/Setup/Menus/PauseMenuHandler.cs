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
            ActivateMenu();
        }
    }

    protected override void WhileFadedIn()
    {
        base.WhileFadedIn();

        if (Input.GetButtonDown("Pause"))
        {
            DeactivateMenu();
        }
    }
}
