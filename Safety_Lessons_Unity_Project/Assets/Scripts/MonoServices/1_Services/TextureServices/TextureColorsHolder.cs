using UnityEngine;

namespace MonoServices.Textures
{
    [System.Serializable]
    public class TextureColorsHolder
    {
        [SerializeField] Color fromColor = Color.white;
        [SerializeField] Color toColor = Color.white;

        public Color FromColor { get => fromColor; set => fromColor = value; }
        public Color ToColor => toColor;
    }
}