using Assets.Scripts.Character;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Character
{
    /// <summary>
    /// 玩家状态类，用于存储玩家的数据
    /// </summary>
    public class PlayerStatus : CharacterStatus
    {
        protected override void Death()
        {
            base.Death();
            print("游戏结束");
        }
    }
}

