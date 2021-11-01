using Assets.Scripts.managers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.shading
{
    public class Fog : MonoBehaviour
    {
        private static readonly int Depth = Shader.PropertyToID("_Depth");
        private static readonly int Top = Shader.PropertyToID("_Top");
        private static readonly int Bottom = Shader.PropertyToID("_Bottom");
        private static readonly int VerticalLimits = Shader.PropertyToID("_VerticalLimits");

        private List<Material> _materials;

        private void OnEnable()
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
            float depth = Mathf.InverseLerp(fogDataBounds.x, fogDataBounds.y, transform.position.z);

            _materials.ForEach(material =>
            {
                material.SetColor(Top, FogData.Instance.topColor);
                material.SetColor(Bottom, FogData.Instance.bottomColor);
                material.SetVector(VerticalLimits, FogData.Instance.verticalLimits);
                material.SetFloat(Depth, depth);
            });
        }
    }
}