//===============================================
//作    者：
//创建时间：2022-03-10 13:35:28
//备    注：
//===============================================
using System;
using UnityEngine;

public abstract class RoleCtrlBase : MonoBehaviour
{
    [SerializeField]
    public Animator Animator;

    [SerializeField]
    public float MoveSpeed = 10f;

    [SerializeField]
    public float RotationSpeed = 5f;

    [SerializeField]
    public Transform HeadBarPos;

    [HideInInspector]
    public CharacterController CharacterController;

    [HideInInspector]
    public Vector3 TargetPos;

    protected StateBase m_CurrState;

    protected RoleInfoBase m_RoleInfo;

    protected IRoleAI m_RoleAI;

    public void Init(RoleInfoBase roleInfo, IRoleAI roleAI)
    {
        m_RoleInfo = roleInfo;
        m_RoleAI = roleAI;
    }

    protected void InitCharacterController()
    {
        CharacterController = GetComponent<CharacterController>();
        if (!CharacterController.isGrounded)
        {
            CharacterController.Move(new Vector3(0, -1000, 0));//让角色贴着地面
        }
    }
}
