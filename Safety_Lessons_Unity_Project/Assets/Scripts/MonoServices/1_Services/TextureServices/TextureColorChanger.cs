using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Textures
{
    public class TextureColorChanger : MonoBehaviour
    {
        [SerializeField] Texture2D[] _textures;
        [SerializeField]
        List<TextureColorsHolder> _textureColorsToChange =
            new List<TextureColorsHolder>();

        bool _canSearchForColors = true;

        void OnValidate()
        {

            if (_textures == null)
                return;

            if (_textures.Length == 0)
                return;

            if (_canSearchForColors)
            {
                _canSearchForColors = false;
                DisplayCurrTextureColors();
            }

        }

        Color[] CurrTextureColors()
        {
            List<Color> tempColors = new List<Color>();

            foreach (var texture in _textures)
            {
                var pixelColors = texture.GetPixels();

                foreach (var color in pixelColors)
                {
                    if (!tempColors.Contains(color))
                        tempColors.Add(color);
                }
            }

            return tempColors.ToArray();
        }


        void DisplayCurrTextureColors()
        {
            _textureColorsToChange.Clear();

            foreach (var color in CurrTextureColors())
            {
                TextureColorsHolder textureColorsHolder = new TextureColorsHolder();
                textureColorsHolder.FromColor = color;
                _textureColorsToChange.Add(textureColorsHolder);
            }
        }

        public void ChangeTextureColors()
        {

            foreach (var texture in _textures)
            {
                var pixelColors = texture.GetPixels();

                for (int i = 0; i < pixelColors.Length; i++)
                {
                    foreach (var textureColorToChange in _textureColorsToChange)
                    {
                        if (pixelColors[i] == textureColorToChange.FromColor)
                            pixelColors[i] = textureColorToChange.ToColor;
                    }
                }

                texture.SetPixels(pixelColors);

                texture.Apply();
            }

            foreach (var textureColorToChange in _textureColorsToChange)
                textureColorToChange.FromColor = textureColorToChange.ToColor;

            _canSearchForColors = true;
            DisplayCurrTextureColors();

        }

    }
}