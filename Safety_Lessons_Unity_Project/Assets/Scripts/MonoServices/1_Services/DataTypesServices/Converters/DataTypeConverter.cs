using UnityEngine;

namespace MonoServices.DataTypes.Converter
{
    public static class DataTypeConverter
    {
        public static Vector4 FromColorToVec4(Color colorToCovert, bool normalized)
        {
            Vector4 quaternion = new Vector4(
                colorToCovert.r,
                colorToCovert.g,
                colorToCovert.b,
                colorToCovert.a
                );

            return normalized ? quaternion.normalized : quaternion;
        }

    }
}

