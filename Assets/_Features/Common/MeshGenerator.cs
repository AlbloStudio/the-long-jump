using Assets.Scripts.utils;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 _planeSize = new(1, 1);
    [SerializeField] private Vector2 _offset = Vector2.zero;
    [SerializeField] private int _planeResolution = 1;
    [SerializeField] private float _planeZ = 0;
    [SerializeField] private bool _auto = false;
    [SerializeField] private bool _updateCollider = false;

    public Vector2 PlaneSize { get => _planeSize; set => _planeSize = value; }
    public Vector2 Offset { get => _offset; set => _offset = value; }
    public int PlaneResolution { get => _planeResolution; set => _planeResolution = value; }
    public float PlaneZ { get => _planeZ; set => _planeZ = value; }
    public bool Auto { get => _auto; set => _auto = value; }
    public bool ShouldUpdateCollider { get => _updateCollider; set => _updateCollider = value; }

    public MeshFilter ObjectMeshFilter { get; private set; }
    public BoxCollider2D BoxCollider2D { get; private set; }

    private void Awake()
    {
        ObjectMeshFilter = GetComponent<MeshFilter>();
    }

    public void GeneratePlane()
    {

        Mesh meshToChange = ObjectMeshFilter.mesh;

        MeshUtils.GenerateMesh(
            PlaneResolution,
            PlaneSize,
            Offset,
            PlaneZ,
            ref meshToChange
        );

        ObjectMeshFilter.mesh = meshToChange;
    }

    public void UpdateCollider()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.size = _planeSize;
        boxCollider2D.offset = (_planeSize / 2f) + _offset;
    }
}

