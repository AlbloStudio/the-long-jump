using Assets.Scripts.shading;
using Assets.Scripts.utils;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Terrain))]
[CanEditMultipleObjects]
public class TerrainFillVariant : Editor
{
    [SerializeField] private VisualTreeAsset _inspectorXML;

    private EdgeCollider2D _collider;
    private PolygonCollider2D _polygon;
    private Renderer _renderer;

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement customInspector = new();
        if (_inspectorXML)
        {
            _inspectorXML.CloneTree(customInspector);
        }

        Button addVariantsButton = (Button)customInspector.Query("addVariants").First();
        Button resetVariantsButton = (Button)customInspector.Query("resetVariants").First();

        addVariantsButton.clicked += AddVariants;
        resetVariantsButton.clicked += ResetVariants;

        return customInspector;
    }

    private void AddVariants()
    {
        List<Object> terrainObjects = new(targets);

        foreach (Object terrain in terrainObjects)
        {
            Terrain terrainObject = terrain as Terrain;
            if (terrainObject.Variants.Count <= 0)
            {
                continue;
            }

            _renderer = terrainObject.GetComponent<Renderer>();
            Transform variantsFolder = terrainObject.transform.Find("variants");

            if (!variantsFolder)
            {
                variantsFolder = new GameObject().transform;
                variantsFolder.parent = terrainObject.transform;
                variantsFolder.name = "variants";
            }

            _collider = terrainObject.GetComponent<EdgeCollider2D>();
            _polygon = terrainObject.gameObject.AddComponent<PolygonCollider2D>();
            _polygon.points = _collider.points;

            int amount = terrainObject.AmountToAdd;

            while (amount > 0)
            {
                Vector2 point = Geometry.GetRandomPointInArea(_polygon);
                if (_polygon.OverlapPoint(point))
                {
                    SpriteRenderer renderer = new GameObject().AddComponent<SpriteRenderer>();
                    renderer.name = "variant";

                    int whichToAdd = Random.Range(0, terrainObject.Variants.Count);
                    renderer.sprite = terrainObject.Variants[whichToAdd];
                    renderer.sortingOrder = _renderer.sortingOrder + 1;
                    renderer.sortingLayerName = _renderer.sortingLayerName;
                    renderer.sharedMaterial = _renderer.sharedMaterial;
                    renderer.transform.parent = variantsFolder;
                    renderer.transform.position = new Vector3(point.x, point.y, _renderer.transform.position.z);

                    _ = Others.AddComponent(renderer.gameObject, _renderer.GetComponent<Fog>());

                    amount--;
                }
            }

            DestroyImmediate(_polygon);
        }
    }

    private void ResetVariants()
    {
        List<Object> terrainObjects = new(targets);

        foreach (Object terrain in terrainObjects)
        {
            Terrain terrainObject = terrain as Terrain;
            Transform variantsFolder = terrainObject.transform.Find("variants");

            SpriteRenderer[] sprites = variantsFolder.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer sprite in sprites)
            {
                Undo.DestroyObjectImmediate(sprite.gameObject);
            }
        }
    }
}
