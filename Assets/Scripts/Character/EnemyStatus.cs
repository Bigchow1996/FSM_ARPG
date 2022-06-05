using Assets.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Character
{
    public class EnemyStatus : CharacterStatus
    {
        protected override void Death()
        {
            base.Death();
            print("enemy死亡");
        }
    }
}
