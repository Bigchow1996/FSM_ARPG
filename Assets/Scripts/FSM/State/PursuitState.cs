using AI.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.FSM
{
    public class PursuitState : FsmState<FsmEnemy>
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Fsm.anim.SetBool(Fsm.status.characterAnimationParameter.run, true);
        }

        public override void OnStateStay()
        {
            base.OnStateStay();
            if(Fsm.target != null)
                Fsm.Movement(Fsm.target.transform.position);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Fsm.anim.SetBool(Fsm.status.characterAnimationParameter.run, false);
            Fsm.StopMove();//停止移动
        }
    }
}

