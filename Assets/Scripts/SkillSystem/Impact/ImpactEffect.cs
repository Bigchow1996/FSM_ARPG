using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Skill
{
    //技能的影响效果
    public class ImpactEffect : MonoBehaviour
    {
        [HideInInspector]
        public SkillDeployer deployer;
        public virtual void StartImpact() { }
        public virtual void StayImpact() { }
        public virtual void StopImpact() { }
    }
}

