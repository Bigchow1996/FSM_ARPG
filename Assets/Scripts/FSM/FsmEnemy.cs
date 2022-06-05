using ArpgDemo.Skill;
using Assets.Scripts.Character;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace AI.FSM
{
    //具体状态机：为具体状态以及条件提供成员
    public class FsmEnemy : FsmBase<FsmEnemy>
    {
        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public CharacterStatus status;
        public CharacterStatus target;//目标
        [Tooltip("目标标签")]
        public string[] tags;
        private NavMeshAgent agent;//敌人寻路组件
        [Tooltip("攻击距离")]
        public float atkDistance;
        [Tooltip("攻击间隔")]
        public float atkInterval;
        [HideInInspector]
        public SkillSystem skillSys;
        private new void Awake()
        {
            InitComponent();
            base.Awake();
        }
        private new void Update()
        {
            base.Update();
            SearchTarget();
        }
        /// <summary>
        /// 查找视野范围内的目标
        /// </summary>
        private void SearchTarget()
        {
            List<CharacterStatus> allTarget = new List<CharacterStatus>();
            //根据标签查找所有的目标
            for (int i = 0; i < tags.Length; i++)
            {
                GameObject[] tempGoArr = GameObject.FindGameObjectsWithTag(tags[i]);
                CharacterStatus[] tempCsArr = tempGoArr.Select(e=> e.GetComponent<CharacterStatus>());
                allTarget.AddRange(tempCsArr);
            }
            //条件：视野内最近活动的
            allTarget = allTarget.FindAll(e => e.HP > 0 && Vector3.Distance(transform.position, e.transform.position) < status.sightDistance);
            target = allTarget.ToArray().GetMin(e => Vector3.Distance(transform.position, e.transform.position));
        }
        /// <summary>
        /// 初始化敌人组件
        /// </summary>
        private void InitComponent()
        {
            anim = GetComponentInChildren<Animator>();
            status = GetComponent<CharacterStatus>();
            agent = GetComponent<NavMeshAgent>();
            skillSys = GetComponent<SkillSystem>();
        }
        /// <summary>
        /// 由追逐状态和巡逻状态调用
        /// </summary>
        public void Movement(Vector3 pos)
        {
            agent.SetDestination(pos);
        }
        /// <summary>
        /// 停止移动
        /// </summary>
        public void StopMove()
        {
            agent.enabled = false;
            agent.enabled = true;
        }
    }
}

