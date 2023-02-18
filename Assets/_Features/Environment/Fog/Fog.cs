using Assets.Scripts.managers;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.Scripts.shading
{
    [ExecuteAlways]
    public class Fog : MonoBehaviour
    {
        private static readonly int Depth = Shader.PropertyToID("_Depth");
        private static readonly int Colors = Shader.PropertyToID("_Colors");
        private static readonly int YBounds = Shader.PropertyToID("_YBounds");

        private List<Material> _materials;
        private Texture2D _colorsTexture;
        private Texture2D _frontColorsTexture;
        private Renderer _renderer;

        private void Start()
        {
            if (Application.IsPlaying(gameObject))
            {
                _renderer = GetComponent<Renderer>();

                _materials = new List<Material>(_renderer.materials);
                _colorsTexture = Gradient2Texture.Create(new[] { FogData.Instance.Colors });
                _frontColorsTexture = Gradient2Texture.Create(new[] { FogData.Instance.FrontColors });
            }
        }

        private void Update()
        {
            if (!Application.IsPlaying(gameObject))
            {
                _renderer = GetComponent<Renderer>();

                if (FogData.Instance.FogInEditor)
                {
                    Gradient colors = FogData.Instance.Colors;
                    Gradient frontColors = FogData.Instance.FrontColors;

                    _colorsTexture = Gradient2Texture.Create(new[] { colors });
                    _frontColorsTexture = Gradient2Texture.Create(new[] { frontColors });

                    _materials = new List<Material>(_renderer.materials);
                    SetShaderValues();
                }
                else
                {
                    _renderer.materials = _renderer.sharedMaterials.Select(m => m = FogData.Instance.FoggyMaterial).ToArray();
                }
            }
            else
            {
                SetShaderValues();
            }
        }

        private void SetShaderValues()
        {
            Vector3 fogDataBounds = FogData.Instance.Bounds;

            float depth = transform.position.z >= 0 ? Mathf.InverseLerp(0, fogDataBounds.y, transform.position.z) : Mathf.InverseLerp(0, -fogDataBounds.x, Mathf.Abs(transform.position.z));

            _materials.ForEach(material =>
            {
                material.SetTexture(Colors, transform.position.z >= 0 ? _colorsTexture : _frontColorsTexture);
                material.SetFloat(Depth, depth);
                material.SetVector(YBounds, transform.position.z <= 0 ? new Vector2(0f, 8f) : new Vector2(0f, 0.2f));
            });
        }
    }
}