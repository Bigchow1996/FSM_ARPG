using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArpgDemo.Character
{
    public class CharacterMotor : MonoBehaviour
    {
        private CharacterController characterController;
        private PlayerStatus playerStatus;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            playerStatus = GetComponent<PlayerStatus>();
        }
        /// <summary>
        /// 通过角色控制器，让当前角色向前移动
        /// </summary>
        public void Movement()
        {
            Vector3 dir = transform.forward;
            dir.y = -10;//Move 需要自己添加Y方向的重力，单位m/s  而SimpleMove自带重力
            characterController.Move( dir * Time.deltaTime * playerStatus.moveSpeed);
        }
        /// <summary>
        /// 当前角色朝向指定方向
        /// </summary>
        /// <param name="dir"></param>
        public void LookAtTarget(Vector3 dir)
        {
            if (dir == Vector3.zero) return;
            Quaternion quaternion = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation,quaternion,Time.deltaTime * playerStatus.rotateSpeed);
        }
    }
}

