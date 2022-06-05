using ArpgDemo.Skill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ArpgDemo.Character
{
    /// <summary>
    /// 角色输入控制器，检测玩家的输入
    /// </summary>
    public class CharacterInputController : MonoBehaviour
    {
        private ETCJoystick joystick;
        private CharacterMotor characterMotor;
        private Animator animator;
        private PlayerStatus playerStatus;
        private ETCButton[] skillButtons;
        private SkillSystem skillSys;
        //查找组件
        private void Awake()
        {
            joystick = FindObjectOfType<ETCJoystick>();
            characterMotor = GetComponent<CharacterMotor>();//移动
            animator = GetComponentInChildren<Animator>();
            playerStatus = GetComponent<PlayerStatus>();
            skillButtons = FindObjectsOfType<ETCButton>();
            skillSys = GetComponent<SkillSystem>();
        }
        //注册事件
        private void OnEnable()
        {
            joystick.onMove.AddListener(OnJoystickMove);//注册移动方法
            joystick.onMoveStart.AddListener(OnJoystichMoveStart);//注册移动的动画方法
            joystick.onMoveEnd.AddListener(OnJoystichMoveEnd);//注册移动的动画暂停方法
            for(int i = 0;i < skillButtons.Length; i++)
            {
                if (skillButtons[i].name == "BaseSkillButton")
                    skillButtons[i].onPressed.AddListener(OnSkillButtonPressed);
                else
                    skillButtons[i].onDown.AddListener(OnSkillButtonDown);
            }
        }

        //注销事件
        private void OnDisable()
        {
            joystick.onMove.RemoveListener(OnJoystickMove);//注销移动方法
            joystick.onMoveStart.RemoveListener(OnJoystichMoveStart);//注销移动的动画
            joystick.onMoveEnd.RemoveListener(OnJoystichMoveEnd);//注销移动的动画暂停方法
            for (int i = 0; i < skillButtons.Length; i++)
            {
                if (skillButtons[i] == null) continue;//防止如果按钮为空报错
                if (skillButtons[i].name == "BaseSkillButton")
                    skillButtons[i].onPressed.RemoveListener(OnSkillButtonPressed);
                else 
                skillButtons[i].onDown.RemoveListener(OnSkillButtonDown);
            }
        }



        private void OnJoystichMoveEnd()
        {
            animator.SetBool(playerStatus.characterAnimationParameter.run, false);
        }
        private void OnJoystichMoveStart()
        {
            animator.SetBool(playerStatus.characterAnimationParameter.run, true);
        }
        //当摇杆移动的时候
        private void OnJoystickMove(Vector2 dir)
        {
            //2D的Y轴对应3D的Z轴
            characterMotor.LookAtTarget(new Vector3(dir.x, 0, dir.y));
            characterMotor.Movement();
        }
        private void OnSkillButtonDown(string name)//这里要修改easytouch 的ETCButton源码，添加string 泛型
        {
            if (IsAttacking()) return;//防止点击过快造成 后面技能覆盖前面技能,必须所有的动画都处于未释放的状态才可以
            //SkillManager manager = GetComponent<SkillManager>();
            int skillID = 0;
            switch (name)
            {
                case "SkillButton01":
                    skillID = 1001;
                    break;
                case "SkillButton02":
                    skillID = 1002;
                    break;
                case "SkillButton03":
                    skillID = 1003;
                    break;
            }
            //SkillDataBase data = manager.PrepareSkill(skillID);
            //if (data != null)
            //{
            //    manager.GenerateSkill(data);
            //}
            skillSys.AttackUseSkill(skillID);
        }
        private float lastPressedTime;
        public float minAttackTime = 1;
        public float maxAttackTime = 3;
        //当按住普通攻击时执行
        private void OnSkillButtonPressed()
        {
            if (IsAttacking()) return;//防止点击过快造成 后面技能覆盖前面技能,必须所有的动画都处于未释放的状态才可以
            float interval = Time.time - lastPressedTime;
            if (interval < minAttackTime) return;
            bool isBatter = interval < maxAttackTime;
            skillSys.AttackUseSkill(1004, isBatter);//false 单击 true 连击
            lastPressedTime = Time.time;
        }
        private bool IsAttacking()
        {
            return animator.GetBool(playerStatus.characterAnimationParameter.attack1) ||
                animator.GetBool(playerStatus.characterAnimationParameter.attack2) ||
                animator.GetBool(playerStatus.characterAnimationParameter.attack3);
        }
    }
}
