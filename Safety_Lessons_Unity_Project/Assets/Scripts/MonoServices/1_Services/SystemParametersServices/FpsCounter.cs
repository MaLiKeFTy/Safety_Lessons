using UnityEngine;
using UnityEngine.UI;


namespace MonoServices.SystemParameters
{
    [RequireComponent(typeof(Text))]

    public class FpsCounter : MonoBehaviour
    {
        int _lastFrameIndex;
        readonly float[] _frameUnscaledDeltaTime = new float[50];
        Text _textMesh;


        void Awake()
        {
            _textMesh = GetComponent<Text>();
        }


        void Update()
        {
            _frameUnscaledDeltaTime[_lastFrameIndex] = Time.unscaledDeltaTime;
            _lastFrameIndex = (_lastFrameIndex + 1) % _frameUnscaledDeltaTime.Length;

            _textMesh.text = Mathf.RoundToInt(CalculateFPS()).ToString();
        }


        float CalculateFPS()
        {
            float total = 0f;

            foreach (var unscaledDelta in _frameUnscaledDeltaTime)
                total += unscaledDelta;

            return _frameUnscaledDeltaTime.Length / total;

        }

    }

}

