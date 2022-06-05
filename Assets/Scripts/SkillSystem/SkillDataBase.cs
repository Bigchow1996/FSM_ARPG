using Assets.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Skill
{
    //负责存储技能相关的参数
    [System.Serializable]
    public class SkillDataBase
    {
        /// <summary>
        /// 技能编号
        /// </summary>
        public int skillID;
        /// <summary>
        /// 技能预制件名称
        /// </summary>
        public string prefabName;
        /// <summary>
        /// 技能预制件对象
        /// </summary>
        [HideInInspector]
        public GameObject skillPrefab;
        /// <summary>
        /// 动画名称
        /// </summary>
        public string animationName;
        /// <summary>
        /// 法力消耗
        /// </summary>
        public int costSP;
        /// <summary>
        /// 冷却时间
        /// </summary>
        public int coolTime;
        /// <summary>
        /// 冷却剩余
        /// </summary>
        [HideInInspector]
        public int coolRemain;
        /// <summary>
        /// 目标标签
        /// </summary>
        public string[] tags;
        /// <summary>
        /// 攻击目标对象数组
        /// </summary>
        [HideInInspector]
        public CharacterStatus[] attackTarget;
        /// <summary>
        /// 连击的下一个技能编号
        /// </summary>
        public int nextBatterID;
        /// <summary>
        /// 技能所属角色
        /// </summary>
        [HideInInspector]
        public CharacterStatus ownerStatus;
    }

}
