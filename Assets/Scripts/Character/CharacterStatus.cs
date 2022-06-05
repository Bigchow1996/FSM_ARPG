using ArpgDemo.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts.Character
{
    public class CharacterStatus:MonoBehaviour
    {
        [Tooltip("攻击力")]
        public float baseATK = 5;

        [Tooltip("角色动画")]
        public CharacterAnimationParameter characterAnimationParameter;

        [Tooltip("防御力")]
        public float defence = 5;

        [Tooltip("生命值")]
        public float HP = 5;

        [Tooltip("最大生命值")]
        public float maxHP = 5;

        [Tooltip("最大法力值")]
        public float maxSP = 5;

        [Tooltip("玩家的移动速度")]
        public float moveSpeed = 5;

        [Tooltip("玩家的旋转速度")]
        public float rotateSpeed = 5;

        [Tooltip("视野距离")]
        public float sightDistance = 5;

        [Tooltip("法力值")]
        public float SP = 5;

        public void Damge(float value)
        {
            value -= defence;
            if (value <= 0) value = 1;
            HP -= value;
            if (HP <= 0) Death();
        }

        protected virtual void Death()
        {
            GetComponentInChildren<Animator>().SetBool(characterAnimationParameter.death, true);
        }
    }
}
