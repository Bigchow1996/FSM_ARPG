using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public interface IResettable
    {
        void OnReset();
    }
    /// <summary>
    /// 游戏对象池
    /// </summary>
    public class GameObjectPool:MonoSingleton<GameObjectPool>
    {
        //1.池
        private Dictionary<string, List<GameObject>> cache;

        protected override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }
        //2.创建
        public GameObject CreateObject(string type,GameObject prefab,Vector3 pos,Quaternion dir)
        {
            //查找可以使用的游戏对象
            GameObject go = null;
            if (cache.ContainsKey(type))
                go = cache[type].Find(e => !e.activeInHierarchy);
            else
                cache.Add(type, new List<GameObject>());
            //2.如果没有则创建并加入池中
            if(go == null)
            {
                go = Instantiate(prefab);
                cache[type].Add(go);
            }
            //3.使用对象

            go.transform.position = pos;
            go.transform.rotation = dir;
            go.SetActive(true);
            //go.GetComponent<Bullet>().CalculateTargetPos();
            //获取IResettable组件，但是IResettable是一个接口，所以这里获取的是实现IResettable的组件
            //go.GetComponent<IResettable>().OnReset();
            foreach (var item in go.GetComponents<IResettable>())
            {
                item.OnReset();
            }
            return go;
        }
        //3.回收
        public void CollectObject(GameObject go,float delay = 0)
        {
            StartCoroutine(DelayCollectObject(go,delay));
        }

        //延迟回收
        private IEnumerator DelayCollectObject(GameObject go,float delay)
        {
            yield return new WaitForSeconds(delay);
            CollectObject(go);
        }
        private void CollectObject(GameObject go)
        {
            go.SetActive(false);
        }
    }
}

