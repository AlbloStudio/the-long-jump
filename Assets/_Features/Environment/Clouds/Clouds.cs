using Assets.Scripts.utils;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Clouds : MonoBehaviour
{
    [SerializeField] private float _timeForNewCloud = 4;
    [SerializeField] private ParticleSystem _clouds;

    private float _timeCount = 0;
    private Collider2D _spawnArea;

    private void Awake()
    {
        _spawnArea = GetComponent<Collider2D>();
    }

    private void Update()
    {
        _timeCount += Time.deltaTime;

        if (_timeCount > +_timeForNewCloud)
        {
            var cloud = Instantiate(_clouds, Geometry.GetRandomPointInArea(_spawnArea), Quaternion.identity, transform);
            GameObject.Destroy(cloud.gameObject, 10);
            _timeCount = 0;
        }
    }
}
