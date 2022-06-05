using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    //有限状态机基类
    public class FsmBase<T> : MonoBehaviour where T:class
    {
        //声明的目的是便于状态机选中某个状态，并执行该状态
        private List<FsmState<T>> stateList;
        [Tooltip("有限状态机配置文件名称")]
        public string configName;
        protected void Awake()
        {
            ConfigFsm();
            initDefaultState();
        }
        private void ConfigFsm()
        {
            //读取配置文件
            //形成数据结构
            var map = new FsmConfigReader(configName).map;
            //配置有限状态机
            stateList = new List<FsmState<T>>();
            foreach (string mainKey in map.Keys)
            {
                Type type = Type.GetType("AI.FSM." + mainKey + "State");
                FsmState<T> state = Activator.CreateInstance(type) as FsmState<T>;
                state.Fsm = this as T;
                foreach (var subMap  in map[mainKey])
                {
                    state.AddMap(subMap.Key, subMap.Value);//每一个state里面都有对应的trigger-->state
                }
                stateList.Add(state);
            }
        }
        [Tooltip("默认状态")]
        public string defaultStateName;
        private FsmState<T> currentState;
        private FsmState<T> defaultState;
        private void initDefaultState()
        {
            //在状态列表中查找默认状态
            defaultState = stateList.Find(e=>e.GetType().Name == defaultStateName+"State");
            //默认状态赋值给当前状态
            currentState = defaultState;
            //进入当前状态
            currentState.OnStateEnter();
        }

        protected void Update()
        {
            currentState.OnStateStay();
            //检查下一状态
            string nextStateName = currentState.Check();//去check当前state里面的trigger
            if (nextStateName != null)
            {
                ChangeState(nextStateName);
            }
        }

        private void ChangeState(string stateName)
        {
            //离开之前的状态
            currentState.OnStateExit();
            //切换
            if (stateName == "Default")//Default写在配置文件中
                currentState = defaultState;//而真正的Default是程序员去unity面板里配置
            else
                currentState = stateList.Find(e => e.GetType().Name == stateName + "State");
            //进入当前状态
            currentState.OnStateEnter();
        }
    }
}

