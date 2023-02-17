using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchLight : MonoBehaviour
{
    [SerializeField]
    private float _frequencyOfChange = 0.1f;
    [SerializeField]
    private float _minLight = 0.3f;
    [SerializeField]
    private float _maxLight = 1f;

    private Light2D _light;

    private float _nextLight = 0f;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
    }

    private void Update()
    {
        _light.intensity = Mathf.Lerp(_light.intensity, _nextLight, _frequencyOfChange);

        if (Mathf.Abs(_light.intensity - _nextLight) <= _frequencyOfChange)
        {
            _nextLight = Random.Range(_minLight, _maxLight);
        }
    }
}
