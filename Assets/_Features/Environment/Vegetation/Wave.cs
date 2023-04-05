using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private static readonly int WindSpeed = Shader.PropertyToID("_WindSpeed");
    private static readonly int WindStrength = Shader.PropertyToID("_WindStrength");
    private static readonly int Granularity = Shader.PropertyToID("_Granularity");

    private List<Material> _materials;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();

        _materials = new List<Material>(_renderer.materials);
        SetShaderValues(false);

    }

    private void Update()
    {
        SetShaderValues(true);
    }

    private void SetShaderValues(bool isStart)
    {
        WindData windData = WindData.Instance;

        _materials.ForEach(material =>
        {
            material.SetFloat(WindSpeed, windData.WindSpeed);
            material.SetFloat(WindStrength, windData.WindStrength);
            if (isStart)
            {
                material.SetFloat(Granularity, windData.Granularity);
            }
        });
    }
}
