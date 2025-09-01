using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownShooter
{
    public class SimplePool : MonoBehaviour
    {
        public GameObject InstancedObject;
        [SerializeField]
        private Transform _parent;
        [SerializeField]
        private int _poolSize;

        private List<GameObject> pool;

        void Start()
        {
            pool = new List<GameObject>();
            for (int i = 0; i < _poolSize; i++)
            {
                var newObj = Instantiate(InstancedObject);
                newObj.transform.SetParent(_parent);
                newObj.SetActive(false);
                pool.Add(newObj);
            }
        }

        public GameObject GetPooledObject()
        {
            var obj = getFirstNonActiveFromPool();
            if (obj != null)
            {
                obj.SetActive(true);
                return obj;
            }
            return null;
        }

        private GameObject getFirstNonActiveFromPool()
        {
            for (int i = 0; i < pool.Count; i++)
            {            
                if (!pool[i].activeInHierarchy) 
                {
                    return pool[i];
                }
            }
            return null;
        }

    }
}