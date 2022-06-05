using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Skill
{
    //消耗自身法力
    public class CostSelfSPEffect : ImpactEffect
    {
        public override void StartImpact()
        {
            base.StartImpact();
            deployer.data.ownerStatus.SP -= deployer.data.costSP;
        }
    }
}

