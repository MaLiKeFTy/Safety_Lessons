using UnityEngine;

namespace MonoServices.DataTypes.Changers
{
    [System.Serializable]
    public class FloatMultiplierHolder
    {
        [SerializeField] string _name;
        [SerializeField] float _multiplierValue;

        public FloatMultiplierHolder(string name, float multiplierValue)
        {
            _name = name;
            _multiplierValue = multiplierValue;
        }

        public string Name => _name;
        public float MultiplierValue => _multiplierValue;
    }
}