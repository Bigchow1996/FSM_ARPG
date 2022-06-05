using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.FSM
{
    //状态基类，代表具体状态，隔离状态机与状态的变化
    public class FsmState<T>
    {
        //条件列表 声明的目的是循环遍历每一个状态，看看每一个状态是否符合其触发条件
        private List<FsmTrigger<T>> triggerList;
        //策划配置的映射表（条件-->状态），声明的目是方便用triggerName去找对应的stateName
        private Dictionary<string, string> map;
        [HideInInspector]
        public T Fsm;
        public FsmState()
        {
            triggerList = new List<FsmTrigger<T>>();
            map = new Dictionary<string, string>();
        }
        /// <summary>
        /// 添加映射 该方法由FsmBase调用，为map赋值，为triggerList赋值，为T fsm赋值
        /// </summary>
        /// <param name="triggerName"></param>
        /// <param name="stateName"></param>
        public void AddMap(string triggerName,string stateName)
        {
            map.Add(triggerName, stateName);
            //创建条件对象
            Type type = Type.GetType("AI.FSM." + triggerName + "Trigger");
            FsmTrigger<T> trigger = Activator.CreateInstance(type) as FsmTrigger<T>;
            //为条件提供状态机引用
            trigger.Fsm = Fsm;
            //把条件对象存入条件对象List中
            triggerList.Add(trigger);
        }
        /// <summary>
        /// 检查条件
        /// </summary>
        /// <returns>根据triggerName 返回对应的状态名</returns>
        public string Check()
        {
            for(int i = 0;i < triggerList.Count; i++)
            {
                //发现满足的条件
                //获取条件对象名
                //策划配置：NoHealth
                //程序类名：NoHealthTrigger
                if (triggerList[i].OnTriggerHandler())
                {
                    string triggerClassName = triggerList[i].GetType().Name;
                    string stateName = map[triggerClassName.Replace("Trigger","")];
                    return stateName;
                }
            }
            return null;
        }
        /// <summary>
        /// 当状态进入
        /// </summary>
        public virtual void OnStateEnter() { }
        /// <summary>
        /// 当状态停留
        /// </summary>
        public virtual void OnStateStay() { }
        /// <summary>
        /// 当状态离开
        /// </summary>
        public virtual void OnStateExit() { }
    }
}

