using Assets.Scripts.utils;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(MeshGenerator))]
[CanEditMultipleObjects]
public class MeshGeneratorInspector : Editor
{
    [SerializeField] private VisualTreeAsset _inspectorXML;

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement customInspector = new();
        if (_inspectorXML)
        {
            _inspectorXML.CloneTree(customInspector);
        }

        Button generateMeshButton = (Button)customInspector.Query("generateMesh").First();

        VisualElement planeResolution = customInspector.Query("planeResolution").First();
        planeResolution.RegisterCallback<ChangeEvent<int>>(OnIntChange);

        VisualElement planeZ = customInspector.Query("planeZ").First();
        planeZ.RegisterCallback<ChangeEvent<float>>(OnFloatChange);

        VisualElement planeSize = customInspector.Query("planeSize").First();
        planeSize.RegisterCallback<ChangeEvent<Vector2>>(OnVector2Change);

        VisualElement offset = customInspector.Query("offset").First();
        offset.RegisterCallback<ChangeEvent<Vector2>>(OnVector2Change);

        generateMeshButton.clicked += GenerateAllPlanes;

        return customInspector;
    }

    private void OnVector2Change(ChangeEvent<Vector2> evt)
    {
        OnDataChange();
    }

    private void OnIntChange(ChangeEvent<int> evt)
    {
        OnDataChange();
    }

    private void OnFloatChange(ChangeEvent<float> evt)
    {
        OnDataChange();
    }

    private void OnDataChange()
    {
        List<Object> meshGeneratorObjects = new(targets);

        foreach (Object meshGeneratorObject in meshGeneratorObjects)
        {
            MeshGenerator meshGenerator = meshGeneratorObject as MeshGenerator;

            if (meshGenerator.Auto)
            {
                GeneratePlane(meshGenerator);
            }
        }
    }

    private void GenerateAllPlanes()
    {
        List<Object> meshGeneratorObjects = new(targets);

        foreach (Object meshGeneratorObject in meshGeneratorObjects)
        {
            GeneratePlane(meshGeneratorObject as MeshGenerator);
        }
    }

    private void GeneratePlane(MeshGenerator meshGenerator)
    {

        MeshFilter meshFilter = meshGenerator.GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.sharedMesh;

        MeshUtils.GenerateMesh(
            meshGenerator.PlaneResolution,
            meshGenerator.PlaneSize,
            meshGenerator.Offset,
            meshGenerator.PlaneZ,
            ref mesh
        );

        meshGenerator.GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
