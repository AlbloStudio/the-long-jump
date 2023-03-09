using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private static readonly int WindSpeed = Shader.PropertyToID("_WindSpeed");
    private static readonly int WindStrength = Shader.PropertyToID("_WindStrength");
    private static readonly int Granularity = Shader.PropertyToID("_Granularity");
    private static readonly int Seed = Shader.PropertyToID("_Seed");

    private List<Material> _materials;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();

        _materials = new List<Material>(_renderer.materials);

        SetShaderValues();
    }

    private void SetShaderValues()
    {
        WindData windData = WindData.Instance;

        _materials.ForEach(material =>
        {
            material.SetFloat(WindSpeed, windData.WindSpeed);
            material.SetFloat(WindStrength, windData.WindStrength);
            material.SetFloat(Granularity, windData.Granularity);
            material.SetFloat(Seed, Random.Range(0f, 1000f));
        });
    }
}
