using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Skill
{
    public class MotionDeployer : SkillDeployer
    {
        [Tooltip("移动距离")]
        public float moveDistance;
        private Vector3 targetPos;
        [Tooltip("移动速度")]
        public float moveSpeed;
        public override void InitDeployer(SkillDataBase data)
        {
            base.InitDeployer(data);
            //对自身产生影响
            effects.Foreach(e => e.StartImpact());//消耗法力CostSelfSPEffect
            //计算移动的目标点
            targetPos = transform.TransformPoint(0, 0, moveDistance);
        }
        private void Update()
        {
            selector.SelectTargets();//获取撞到的敌人
            effects.Foreach(e => e.StayImpact());//
            Movement();//技能不断的靠近 目标点
            //技能到目标点的距离小于0.1，或者已经撞到敌人了，那么就用对象池回收该技能
            if(Vector3.Distance(transform.position,targetPos) < 0.1 || selector.attackTargets.Length > 0)
            {
                GameObjectPool.Instance.CollectObject(gameObject);
            }
        }

        private void Movement()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
        }
        private void OnDisable()
        {
            effects.Foreach(e => e.StopImpact());
        }
    }
}

