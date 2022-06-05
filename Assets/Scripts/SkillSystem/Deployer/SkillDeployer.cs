using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Skill
{
    //技能释放器
    public class SkillDeployer : MonoBehaviour
    {
        [HideInInspector]
        public SkillDataBase data;
        [HideInInspector]
        public AttackSelector selector;//这里要用public 不能用protected 因为Effect的子类会用到
        [HideInInspector]
        protected ImpactEffect[] effects;//为什么要用数组？因为可能有多种类型的影响，比如灼伤，减速等
        private void Awake()
        {
            selector = GetComponent<AttackSelector>();
            effects = GetComponents<ImpactEffect>();
        }
        public virtual void InitDeployer(SkillDataBase data)
        {
            this.data = data;//给释放方式赋值技能的参数
            if(selector!=null) selector.data = data;//把技能的参数赋值给选区
            //foreach (var item in effects)
            //{
            //    item.data = data;
            //}
            effects.Foreach(e => e.deployer = this);//把释放的方式给每一个影响
        }
    }
}

