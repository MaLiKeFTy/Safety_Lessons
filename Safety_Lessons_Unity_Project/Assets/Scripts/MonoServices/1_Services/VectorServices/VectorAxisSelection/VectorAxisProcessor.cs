using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MonoServices.Vectors
{
    public static class VectorAxisProcessor
    {
        static Dictionary<VectorAxisEnum, VectorAxis> _vectorAxis = new Dictionary<VectorAxisEnum, VectorAxis>();
        static bool _isInitialised;

        static void Initialise()
        {
            _vectorAxis.Clear();

            var assembly = Assembly.GetAssembly(typeof(VectorAxis));

            var allVectorAxis = assembly.GetTypes()
                .Where(t => typeof(VectorAxis).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var axis in allVectorAxis)
            {
                VectorAxis vecAxis = Activator.CreateInstance(axis) as VectorAxis;
                _vectorAxis.Add(vecAxis.VectorAxisEnum, vecAxis);
            }
        }

        public static Vector3 GetAxisTarget(VectorAxisEnum vectorAxisEnum)
        {
            if (!_isInitialised)
                Initialise();

            var vecAxis = _vectorAxis[vectorAxisEnum];
            return vecAxis.SelectedVectorAxis();
        }
    }
}