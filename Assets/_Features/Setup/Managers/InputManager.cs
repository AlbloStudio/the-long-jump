using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public bool CanPause = true;

    private void Update()
    {
        if (CanPause && Input.GetButtonDown("Pause"))
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        }
    }
}
