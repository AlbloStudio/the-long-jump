using Assets.Scripts.utils;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Clouds : MonoBehaviour
{
    [SerializeField] private float _timeForNewCloud = 4;
    [SerializeField] private float _cloudLife = 20;
    [SerializeField] private Cloud _cloudPrefab;
    [SerializeField] private Vector2 zRange = new(3f, 8f);

    private float _timeCount = 0;
    private Collider2D _spawnArea;
    private Cloud[] _cloudInstances;
    private int _index = 0;
    private int _cloudAmount;

    private void Awake()
    {
        _spawnArea = GetComponent<Collider2D>();
        _cloudAmount = Mathf.CeilToInt(_cloudLife / _timeForNewCloud);

        _cloudInstances = new Cloud[_cloudAmount];
    }

    private void Update()
    {
        _timeCount += Time.deltaTime;

        if (_timeCount >= _timeForNewCloud)
        {
            if (_cloudInstances[_index] == null)
            {
                _cloudInstances[_index] = MakeCloud();
            }
            else
            {
                _cloudInstances[_index].transform.position = GetNewPosition();
                _cloudInstances[_index].ReCalculate();
            }

            _timeCount = 0;
            _index++;
        }

        if (_index == _cloudAmount)
        {
            _index = 0;
        }
    }

    private Cloud MakeCloud()
    {
        Cloud cloud = Instantiate(_cloudPrefab, GetNewPosition(), Quaternion.identity);
        ParticleSystem.MainModule main = cloud.GetComponent<ParticleSystem>().main;
        main.startLifetime = _cloudLife;

        return cloud;
    }

    private Vector3 GetNewPosition()
    {
        Vector2 new2DPosition = Geometry.GetRandomPointInArea(_spawnArea);
        float z = Random.Range(zRange.x, zRange.y);

        return new(new2DPosition.x, new2DPosition.y, z);
    }
}
