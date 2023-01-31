using UnityEngine;

public class Water : MonoBehaviour
{
    private static readonly int _Amplitude = Shader.PropertyToID("_Amplitude");
    private static readonly int _Speed = Shader.PropertyToID("_Speed");
    private static readonly int _Anchor = Shader.PropertyToID("_Anchor");
    private static readonly int _Alpha = Shader.PropertyToID("_Alpha");
    private static readonly int _FoamThickness = Shader.PropertyToID("_FoamThickness");
    private static readonly int _HorizontalSpeed = Shader.PropertyToID("_HorizontalSpeed");
    private static readonly int _VerticalSpeed = Shader.PropertyToID("_VerticalSpeed");

    [SerializeField][Range(0, 10)] private float _amplitude = 0.5f;
    [SerializeField][Range(0, 10)] private float _speed = 0.2f;
    [SerializeField][Range(0, 1)] private float _anchor = 0.3f;
    [SerializeField][Range(0, 1)] private float _alpha = 0.6f;
    [SerializeField][Range(0, 0.3f)] private float _foamThickness = 0.005f;
    [SerializeField][Range(-20f, 20f)] private float _horizontalSpeed = 1f;
    [SerializeField][Range(-20f, 20f)] private float _verticalSpeed = 0f;
    [SerializeField] private Vector2 _mainTextureScale;

    private Material _material;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        _material.SetFloat(_Amplitude, _amplitude);
        _material.SetFloat(_Speed, _speed);
        _material.SetFloat(_Anchor, _anchor);
        _material.SetFloat(_Alpha, _alpha);
        _material.SetFloat(_FoamThickness, _foamThickness);
        _material.SetFloat(_HorizontalSpeed, _horizontalSpeed);
        _material.SetFloat(_VerticalSpeed, _verticalSpeed);

        _material.mainTextureScale = _mainTextureScale;

    }
}
