using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.FSM
{
    //丢失目标
    public class LoseTargetTrigger : FsmTrigger<FsmEnemy>
    {
        public override bool OnTriggerHandler()
        {
            return Fsm.target == null;
        }
    }
}

