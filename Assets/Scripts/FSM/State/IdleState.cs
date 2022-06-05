using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    //闲置状态
    public class IdleState : FsmState<FsmEnemy>
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Fsm.anim.SetBool(Fsm.status.characterAnimationParameter.idle, true);
        }
        public override void OnStateExit()
        {
            base.OnStateExit();
            Fsm.anim.SetBool(Fsm.status.characterAnimationParameter.idle, false);
        }
    }
}

