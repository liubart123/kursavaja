using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.GamePlay.Scripts.Other.ObjectPull
{
    public interface IObjectPull<T> where T : MonoBehaviour
    {
        //get object from pull or create new
        T GetObject();
        //return object to pull
        void ReturnObject(T obj);
        //initialize fields of pull, such as object to save and size of pull
        void Initialize(GameObject objectOfPull, int size);
        void ClearPool();
    }
}
