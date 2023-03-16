using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        }
    }
}
