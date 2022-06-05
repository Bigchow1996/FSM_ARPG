using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Skill
{
    //技能系统类，封装技能系统，提供更简单的调用方式
    public class SkillSystem : MonoBehaviour
    {
        private SkillManager manager;
        private Animator anim;
        [HideInInspector]
        public SkillDataBase data;
        private void Start()
        {
            manager = GetComponent<SkillManager>();
            anim = GetComponentInChildren<Animator>();
            anim.GetComponent<AnimationEventBehaviour>().AttackHandler += DeploySkill;
        }
        /// <summary>
        /// 为玩家提供 的技能释放方法
        /// </summary>
        /// <param name="id"></param>
        public void AttackUseSkill(int id,bool isBatter = false) //1004 -> 1005 ->1006
        {
            //如果连击，则使用上一个技能的data获取即将释放技能的编号
            if (isBatter && data != null)
            {
                id = data.nextBatterID;
            }
            data = manager.PrepareSkill(id);
            if (data != null)
            {
                anim.SetBool(data.animationName, true);//播放动画的时候会触发技能释放事件
            }
        }
        private void DeploySkill()
        {
            manager.GenerateSkill(data);
        }
        /// <summary>
        /// 供NPC使用的随机技能使用方法
        /// </summary>
        /// <param></param>
        public void RandomUseSkill()
        {
            //1.先获取所有可以释放的技能
            List<SkillDataBase> userableList = manager.skillList.FindAll(e => manager.PrepareSkill(e.skillID) != null);
            //2.随机选取一个
            SkillDataBase data = userableList[Random.Range(0, userableList.Count)];
            AttackUseSkill(data.skillID);
        }

    }
}

