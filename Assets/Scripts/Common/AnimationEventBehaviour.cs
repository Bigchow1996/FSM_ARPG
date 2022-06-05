using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    //动画事件行为类,子物体动画播放时，不能调用父物体的方法，所以该类应运而生
    public class AnimationEventBehaviour : MonoBehaviour 
    {
        public event Action AttackHandler;
        private Animator anim;
        private void Start()
        {
            anim = GetComponent<Animator>();
        }
        //由Unity动画调用
        private void OnAttack()
        {
            if (AttackHandler != null)
            {
                AttackHandler();
            }
        }

        private void OnCancelAnim(string animName)
        {
            anim.SetBool(animName, false);
        }
    }   
}

