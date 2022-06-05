using Assets.Scripts.Character;
using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Skill
{
    public class SelectorSelector : AttackSelector
    {
        [Tooltip("距离")]
        public float distance;
        [Tooltip("角度")]
        public float angle;
        public override void SelectTargets()
        {

            List<CharacterStatus> allTarget = new List<CharacterStatus>();
            //根据标签查找所有的目标
            for (int i = 0; i < data.tags.Length; i++)
            {
                GameObject[] tempGoArr = GameObject.FindGameObjectsWithTag(data.tags[i]);
                CharacterStatus[] tempCsArr = tempGoArr.Select(e => e.GetComponent<CharacterStatus>());
                allTarget.AddRange(tempCsArr);
            }
            //条件：活的 && 距离 && 当前扇形内
            allTarget = allTarget.FindAll(
                e => e.HP > 0 &&
                Vector3.Distance(transform.position, e.transform.position) <= distance &&
                Vector3.Angle(transform.forward, e.transform.position - transform.position) <= angle

            );
            if (selectType == SelectType.Group || allTarget.Count == 0)
            {
                attackTargets = allTarget.ToArray();
            }
            else
            {
                CharacterStatus target = allTarget.ToArray().GetMin(e => Vector3.Distance(transform.position, e.transform.position));
                attackTargets = new CharacterStatus[] { target };
            }
        }
    }
}

