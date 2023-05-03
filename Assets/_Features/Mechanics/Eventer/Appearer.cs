using Assets.Scripts.managers;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Appearer : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objectsToMakeAppear = new();
    [SerializeField] private List<GameObject> _objectsToMakeDisappear = new();

    private bool _alreadyActivated = false;

    protected virtual void OnTriggerEnter2D(Collider2D collided)
    {
        if (!_alreadyActivated)
        {
            if (collided.transform.Equals(GeneralData.Instance.Player.transform))
            {
                _objectsToMakeAppear.ForEach(go => go.SetActive(true));
                _objectsToMakeDisappear.ForEach(go => go.SetActive(false));

                _alreadyActivated = true;
            }
        }
    }
}
