using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.FSM
{
    //进入攻击范围
    public class ReachTargetTrigger : FsmTrigger<FsmEnemy>
    {
        public override bool OnTriggerHandler()
        {
            return Fsm.target!=null && Vector3.Distance(Fsm.target.transform.position,Fsm.transform.position) < Fsm.atkDistance;
        }
    }
}

