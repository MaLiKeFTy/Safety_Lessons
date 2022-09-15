using UnityEngine;

namespace MonoServices.Textures
{
    public class TrueBlackAndWhiteTextureChanger : MonoBehaviour
    {
        [SerializeField] Texture2D[] _textures;

        public void ChangeTextureColors()
        {
            foreach (var texture in _textures)
            {
                var pixelColors = texture.GetPixels();

                for (int i = 0; i < pixelColors.Length; i++)
                {

                    var absColor = new Color((int)pixelColors[i].r, (int)pixelColors[i].g, (int)pixelColors[i].b, 1);

                    if (absColor == Color.white)
                        pixelColors[i] = Color.white;
                    else if (absColor == Color.black)
                        pixelColors[i] = Color.black;

                }

                texture.SetPixels(pixelColors);

                texture.Apply();
            }

        }
    }
}