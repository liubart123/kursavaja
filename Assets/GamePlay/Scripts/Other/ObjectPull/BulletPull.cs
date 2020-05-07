using Assets.GamePlay.Scripts.Ammo;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Other.ObjectPull
{
    public class BulletPull : MonoBehaviour, IObjectPull<Bullet>
    {
        [SerializeField]
        protected GameObject ParentForPullObjects;
        protected GameObject objectOfPull;
        protected int size;
        protected List<Bullet> freeObjs = new List<Bullet>();
        protected List<Bullet> unFreeObjs = new List<Bullet>();

        public void Initialize(GameObject objectOfPull, int size)
        {
            if (freeObjs.Count!=0 && unFreeObjs.Count != 0)
            {
                ClearPool();
            }
            ParentForPullObjects = GameObject.FindGameObjectWithTag("BulletPull");
            this.objectOfPull = objectOfPull;
            this.size = size;
            FillUpPool();
        }
        protected void FillUpPool()
        {
            for (int i = 0; i < size; i++)
            {
                CreateObjectAndAddToPull();
            }
        }

        public Bullet GetObject()
        {
            Bullet temp;
            if (freeObjs.Count != 0)
            {
                temp = freeObjs.First<Bullet>();
                freeObjs.RemoveAt(0);
            } else
            {
                temp = unFreeObjs.First();
                unFreeObjs.RemoveAt(0);
                //unFreeObjs.RemoveAt(0);
                //temp = CreateObjectAndAddToPull();
            }
            temp.gameObject.SetActive(true);
            unFreeObjs.Add(temp);
            return temp;
        }
        protected Bullet CreateObjectAndAddToPull()
        {
            Bullet temp;
            temp = Instantiate(objectOfPull, Vector2.zero, transform.rotation).GetComponent<Bullet>();

            temp.gameObject.AddComponent<BulletForPull>().SetPull(this);
            temp.gameObject.SetActive(false);
            temp.transform.SetParent(ParentForPullObjects.transform);
            temp.Clone(objectOfPull.GetComponent<Bullet>());
            freeObjs.Add(temp);
            return temp;
        }
        public void ReturnObject(Bullet obj)
        {
            unFreeObjs.Remove(obj);
            freeObjs.Add(obj);
            obj.gameObject.SetActive(false);
        }

        public void ClearPool()
        {
            unFreeObjs.ForEach(el => el.Delete());
        }
    }
}
