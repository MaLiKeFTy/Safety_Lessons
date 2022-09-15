using System.Collections.Generic;
using UnityEngine;

namespace MonoServices.Spawnning
{
    public static class ObjPoolerProccesor
    {
        readonly static Dictionary<string, Queue<GameObject>> _objPools
            = new Dictionary<string, Queue<GameObject>>();

        public static GameObject GetObject(GameObject obj)
        {
            if (_objPools.TryGetValue(obj.name, out Queue<GameObject> objList))
                return objList.Count == 0 ? CreateNewObj(obj) : DequeueObj(objList);
            else
                return CreateNewObj(obj);
        }

        public static void ReturnObj(GameObject obj)
        {
            if (_objPools.TryGetValue(obj.name, out Queue<GameObject> objList))
            {
                objList.Enqueue(obj);
            }
            else
            {
                Queue<GameObject> newObjQueue = new Queue<GameObject>();
                newObjQueue.Enqueue(obj);
                _objPools.Add(obj.name, newObjQueue);
            }

            obj.SetActive(false);
        }

        static GameObject CreateNewObj(GameObject obj)
        {
            GameObject newGO = Object.Instantiate(obj);
            newGO.name = obj.name;
            return newGO;
        }

        static GameObject DequeueObj(Queue<GameObject> objList)
        {
            GameObject newObj = objList.Dequeue();
            newObj.SetActive(true);
            return newObj;
        }
    }
}