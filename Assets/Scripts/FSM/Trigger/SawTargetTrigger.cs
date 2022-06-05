using AI.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.FSM
{
    public class SawTargetTrigger : FsmTrigger<FsmEnemy>
    {
        public override bool OnTriggerHandler()
        {
            return Fsm.target != null;
        }
    }
}

