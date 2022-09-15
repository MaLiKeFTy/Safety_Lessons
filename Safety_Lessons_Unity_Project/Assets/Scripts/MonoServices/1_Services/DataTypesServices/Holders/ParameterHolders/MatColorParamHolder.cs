using UnityEngine;

namespace MonoServices.Holders
{
    [System.Serializable]
    public class MatColorParamHolder
    {
        [SerializeField] string _name;
        [SerializeField] Color _color;
        [SerializeField] string[] _shaderParams;

        public MatColorParamHolder(string name, Color color, string[] shaderParams)
        {
            _name = name;
            _color = color;
            _shaderParams = shaderParams;
        }

        public string Name => _name;

        public Color Color => _color;

        public string[] ShaderParams => _shaderParams;
    }
}