using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour,IResettable
{
    private Vector3 targetPos;
    //private void Awake()
    //{
    //    print("子弹的Awake执行了");
    //    targetPos = transform.TransformPoint(0, 0, 50);
    //}
    public void CalculateTargetPos()
    {
        targetPos = transform.TransformPoint(0, 0, 50);
    }

    public void OnReset()
    {
        CalculateTargetPos();
    }

    void Update () {
        print("子弹的Update执行了");
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime*50);
        if (Vector3.Distance(transform.position, targetPos) < 0.1)
        {
            //Destroy(gameObject);
            //通过对象池回收
            GameObjectPool.Instance.CollectObject(gameObject);
        }
	}
}
