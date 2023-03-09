using UnityEngine;

public class WindData : Singleton<WindData>
{
    [SerializeField] private float _windSpeed = 0;
    [SerializeField] private float _windStrength = 0;
    [SerializeField] private float _granularity = 0;

    public float WindSpeed => _windSpeed;
    public float WindStrength => _windStrength;
    public float Granularity => _granularity;
}

