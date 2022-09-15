using MonoServices.ObjectsSO;
using UnityEngine;

namespace MonoServices.MeshTexts
{
    [System.Serializable]
    public class MeshTextInfoHolder
    {
        [SerializeField] string _meshChangerTag;
        [SerializeField] StringsSO _textList;

        public string MeshChangerTag => _meshChangerTag;
        public StringsSO TextList => _textList;
    }
}
