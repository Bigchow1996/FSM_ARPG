using Assets.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Skill
{
    public enum SelectType
    {
        Single,
        Group
    }
    //技能的选区
    public abstract class AttackSelector : MonoBehaviour
    {
        [Tooltip("选择类型")]
        public SelectType selectType;
        //选择的是哪些敌人
        [HideInInspector]
        public CharacterStatus[] attackTargets;
        [HideInInspector]
        public SkillDataBase data;
        /// <summary>
        /// 选择目标
        /// </summary>
        /// <param name="data"></param>
        public abstract void SelectTargets();
    }
}

