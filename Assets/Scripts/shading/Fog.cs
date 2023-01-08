using Assets.Scripts.managers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.shading
{
    public class Fog : MonoBehaviour
    {
        private static readonly int Depth = Shader.PropertyToID("_Depth");
        private static readonly int Colors = Shader.PropertyToID("_Colors");

        private List<Material> _materials;

        private void Awake()
        {
            _materials = new List<Material>(GetComponent<Renderer>().materials);
        }

        private void Update()
        {
            SetShaderValues();
        }

        private void SetShaderValues()
        {
            Vector3 fogDataBounds = FogData.Instance.bounds;

            float depth = Mathf.InverseLerp(0, fogDataBounds.y, transform.position.z);

            _materials.ForEach(material =>
            {
                material.SetTexture(Colors, FogData.Instance.colors);
                material.SetFloat(Depth, depth);
            });
        }
    }
}