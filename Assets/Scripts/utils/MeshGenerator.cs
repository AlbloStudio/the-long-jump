using Assets.Scripts.utils;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 _planeSize = new(1, 1);
    [SerializeField] private Vector2 _offset = Vector2.zero;
    [SerializeField] private int _planeResolution = 1;
    [SerializeField] private bool _auto = false;

    public Vector2 PlaneSize { get => _planeSize; set => _planeSize = value; }
    public Vector2 Offset { get => _offset; set => _offset = value; }
    public int PlaneResolution { get => _planeResolution; set => _planeResolution = value; }
    public bool Auto { get => _auto; set => _auto = value; }

    public MeshFilter ObjectMeshFilter { get; private set; }

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
            transform,
            Offset,
            ref meshToChange
        );

        ObjectMeshFilter.mesh = meshToChange;
    }
}

