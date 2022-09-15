using UnityEngine;

namespace MonoServices.ObjectsSO
{
    [CreateAssetMenu(fileName = "GameObjectsSO", menuName = "ScriptableObjects/ObjsSO/GameObjectsSO")]
    public class GameobjsSO : ObjectsSO<GameObjsListHolder> { }

    [System.Serializable]
    public class GameObjsListHolder
    {
        [SerializeField] string name = "sample";
        [SerializeField] GameObject[] _gameoObjs;

        public GameObject[] GameoObjs { get => _gameoObjs; set => _gameoObjs = value; }
        public string Name => name;
    }
}