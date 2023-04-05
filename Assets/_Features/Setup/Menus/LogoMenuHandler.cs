using System.Collections;
using UnityEngine;

public class LogoMenuHandler : MenuHandler
{
    [SerializeField] protected float _logoTime = 3;

    protected override IEnumerator WhileFadedIn()
    {
        _ = StartCoroutine(base.WhileFadedIn());

        yield return new WaitForSeconds(_logoTime);

        DeactivateMenu();
    }
}
