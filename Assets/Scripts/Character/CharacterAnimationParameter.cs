using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Character
{
    /// <summary>
    /// 角色参数类，封装角色参数
    /// </summary>
    [System.Serializable]
    public class CharacterAnimationParameter
    {
        public string death="death";
        public string run = "run";
        public string idle = "idle";
        public string attack1 = "attack1";
        public string attack2 = "attack2";
        public string attack3 = "attack3";
        public string walk = "walk";
    }
}
