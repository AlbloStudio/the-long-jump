using System.Collections;
using UnityEngine;

public class BodyParts : MonoBehaviour
{
    [SerializeField] private float _timeToMakePartsDisappear = 3;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_timeToMakePartsDisappear);

        Destroy(gameObject);
    }
}
