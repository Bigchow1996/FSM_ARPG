using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    //条件基类，代表具体条件，隔离状态与条件的变化
    public abstract class FsmTrigger<T>
    {
        public abstract bool OnTriggerHandler();
        public T Fsm;//由FsmState赋值
    }

}
