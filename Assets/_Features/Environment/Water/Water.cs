using UnityEngine;

public class Water : MonoBehaviour
{
    private static readonly int _Amplitude = Shader.PropertyToID("_Amplitude");
    private static readonly int _Speed = Shader.PropertyToID("_Speed");
    private static readonly int _Anchor = Shader.PropertyToID("_Anchor");
    private static readonly int _AnchorX = Shader.PropertyToID("_AnchorX");
    private static readonly int _Alpha = Shader.PropertyToID("_Alpha");
    private static readonly int _FoamThickness = Shader.PropertyToID("_FoamThickness");
    private static readonly int _HorizontalSpeed = Shader.PropertyToID("_HorizontalSpeed");
    private static readonly int _VerticalSpeed = Shader.PropertyToID("_VerticalSpeed");

    [SerializeField][Range(0, 10)] private float _amplitude = 0.5f;
    [SerializeField][Range(0, 10)] private float _speed = 0.2f;
    [SerializeField][Range(-2f, 2f)] private float _anchor = 0.3f;
    [SerializeField][Range(-2f, 2f)] private float _anchorX = 2f;
    [SerializeField][Range(0, 1)] private float _alpha = 0.6f;
    [SerializeField][Range(0, 0.3f)] private float _foamThickness = 0.005f;

    private Material _material;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        if (mesh)
        {
            ParticleSystemRenderer particleSystemRenderer = GetComponent<ParticleSystemRenderer>();
            particleSystemRenderer.mesh = mesh;

            ParticleSystem.ShapeModule shape = GetComponent<ParticleSystem>().shape;
            shape.shapeType = ParticleSystemShapeType.Mesh;
            shape.mesh = mesh;
        }
    }

    private void Update()
    {
        _material.SetFloat(_Amplitude, _amplitude);
        _material.SetFloat(_Speed, _speed);
        _material.SetFloat(_Anchor, _anchor);
        _material.SetFloat(_AnchorX, _anchorX);
        _material.SetFloat(_Alpha, _alpha);
        _material.SetFloat(_FoamThickness, _foamThickness);
    }
}
