using Assets.Scripts.Character;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Skill
{
    //伤害生命
    public class DamageEffect : ImpactEffect
    {
        public float damageRotio;//伤害比例
        public float damageInterval;//伤害间隔
        private List<CharacterStatus> damagedList;
        private void Awake()
        {
            damagedList = new List<CharacterStatus>();
        }
        public override void StayImpact()
        {
            base.StayImpact();
            //1.选出新目标
            CharacterStatus[] newTargets = deployer.selector.attackTargets.FindAll(e => !damagedList.Contains(e));
            for (int i = 0; i < newTargets.Length; i++)
            {
                damagedList.Add(newTargets[i]);
                StartCoroutine(RepeatDamage(newTargets[i]));
            }
        }
        /// <summary>
        /// 重复伤害目标
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private IEnumerator RepeatDamage(CharacterStatus target)
        {
            //虽然是死循环，表面上看似一直攻击 但其实待技能被回收时，协程自动停止（不再攻击）
            while(true)
            {
                float atk = deployer.data.ownerStatus.baseATK * damageRotio;
                target.Damge(atk);
                yield return new WaitForSeconds(damageInterval);
                //如果目标离开攻击范围则取消
                if (deployer.selector.attackTargets.Find(e=> e == target) == null)
                {
                    damagedList.Remove(target);//移除
                    yield break; //协程不做了
                }
            }
        }

        public override void StopImpact()
        {
            base.StopImpact();
            damagedList.Clear();
        }
    }
}

