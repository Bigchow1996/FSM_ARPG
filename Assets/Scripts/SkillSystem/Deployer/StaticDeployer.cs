using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Skill
{
    //静止技能释放器
    public class StaticDeployer : SkillDeployer
    {
        [Tooltip("生存时间")]
        public float duration;
        public override void InitDeployer(SkillDataBase data)
        {
            base.InitDeployer(data);
            //对自身产生影响
            effects.Foreach(e => e.StartImpact());
            GameObjectPool.Instance.CollectObject(gameObject, duration);

        }
        private void Update()
        {
            selector.SelectTargets();
            effects.Foreach(e => e.StayImpact());
        }
        private void OnDisable()
        {
            effects.Foreach(e => e.StopImpact());
        }
    }
}

