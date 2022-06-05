using Assets.Scripts.Character;
using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Skill
{
    //技能管理器，附加到攻击方，负责技能释放前的相关行为
    public class SkillManager : MonoBehaviour
    {
        //技能列表，需要unity中配置每一个skillList
        public List<SkillDataBase> skillList;
        private void Start()
        {
            for (int i = 0; i < skillList.Count; i++)
            {
                InitSkill(skillList[i]);
            }
        }

        private void InitSkill(SkillDataBase skillDataBase)
        {
            skillDataBase.ownerStatus = GetComponent<CharacterStatus>();
            //skillDataBase.skillPrefab = Resources.Load<GameObject>("Prefabs/Skills/Static/"+skillDataBase.prefabName);
            skillDataBase.skillPrefab = ResourceManager.Load<GameObject>(skillDataBase.prefabName);
        }
        /// <summary>
        /// 准备技能，
        /// </summary>
        /// <param name="skillID"></param>
        /// <returns>返回技能数据对象 不成功则返回null</returns>
        public SkillDataBase PrepareSkill(int skillID)
        {
            //法力 冷却剩余时间
            return skillList.Find(e => e.skillID == skillID && e.coolRemain <= 0 && e.costSP <= e.ownerStatus.SP );
        }
        /// <summary>
        /// 生成技能
        /// </summary>
        /// <param name="skillDataBase"></param>
        public void GenerateSkill(SkillDataBase skillDataBase)
        {
            //Instantiate(skillDataBase.skillPrefab, transform.position, transform.rotation);
            GameObject skillGo = GameObjectPool.Instance.CreateObject(skillDataBase.prefabName, skillDataBase.skillPrefab, transform.position, transform.rotation);
            skillGo.GetComponent<SkillDeployer>().InitDeployer(skillDataBase);
            StartCoroutine(CoolTimeDown(skillDataBase));
        }
        private IEnumerator CoolTimeDown(SkillDataBase skillDataBase)
        {
            skillDataBase.coolRemain = skillDataBase.coolTime;
            yield return new WaitForSeconds(skillDataBase.coolTime);
            skillDataBase.coolRemain = 0;
        }
    }
}

