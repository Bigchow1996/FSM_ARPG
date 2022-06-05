using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.FSM
{
    //攻击状态
    public class AttackingState : FsmState<FsmEnemy>
    {
        private float atkTime;
        public override void OnStateStay()
        {
            base.OnStateStay();
            if(atkTime < Time.time)
            {
                Fsm.skillSys.RandomUseSkill();
                atkTime = Time.time + Fsm.atkInterval;
            }
            
        }
    }
}

