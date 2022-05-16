//===============================================
//作    者：
//创建时间：2022-04-21 16:40:50
//备    注：
//===============================================
using UnityEngine;
using UnityEngine.UI;

public class SelectRoleSceneView : MonoBehaviour
{
    //主摄像机围绕着旋转的父物体
    [SerializeField]
    private Transform m_MainCameraParent;
    //摄像机旋转速度，单位：度/秒
    [SerializeField]
    private float m_CameraRotateSpeed = 100;
    //选择的职业名称
    [SerializeField]
    private Text m_SelectJobName;
    //选择职业描述
    [SerializeField]
    private Text m_SelectJobDesc;

    //起始欧拉角的y值
    private float m_OriginalEulerAngleY;
    //目标欧拉角的y值
    private float m_TargetEulerAngleY;
    //旋转所需的总时间
    private float m_RotateTotalTime;
    //旋转经过的时间
    private float m_RotateElapsedTime;
    //是否需要旋转
    private bool m_IsNeedRotate = false;

    /// <summary>
    /// 旋转摄像机
    /// </summary>
    /// <param name="dir">方向，只能向左或右</param>
    public void RotateCamera(UIDirection dir)
    {
        if(dir == UIDirection.LEFT)
        {
            //向左旋转，欧拉角的y值增大到最近的90度的倍数
            m_TargetEulerAngleY = (Mathf.FloorToInt(m_MainCameraParent.localEulerAngles.y) / 90 + 1) * 90;
            m_OriginalEulerAngleY = m_MainCameraParent.localEulerAngles.y;
            m_RotateTotalTime = Mathf.Abs(m_TargetEulerAngleY - m_OriginalEulerAngleY) / m_CameraRotateSpeed;
            m_RotateElapsedTime = 0;
            m_IsNeedRotate = true;
        }
        else if(dir == UIDirection.RIGHT)
        {
            //向右旋转，eulerAngleY减小到最近的90度的倍数
            m_TargetEulerAngleY = (Mathf.CeilToInt(m_MainCameraParent.localEulerAngles.y / 90) - 1) * 90;
            m_OriginalEulerAngleY = m_MainCameraParent.localEulerAngles.y;
            m_RotateTotalTime = Mathf.Abs(m_TargetEulerAngleY - m_OriginalEulerAngleY) / m_CameraRotateSpeed;
            m_RotateElapsedTime = 0;
            m_IsNeedRotate = true;
        }
    }

    /// <summary>
    /// 旋转摄像机
    /// </summary>
    /// <param name="TargetEulerAngleY">目标欧拉角的y值</param>
    public void RotateCamera(float TargetEulerAngleY)
    {
        m_TargetEulerAngleY = TargetEulerAngleY;
        m_OriginalEulerAngleY = m_MainCameraParent.localEulerAngles.y;
        m_RotateTotalTime = Mathf.Abs(m_TargetEulerAngleY - m_OriginalEulerAngleY) / m_CameraRotateSpeed;
        m_RotateElapsedTime = 0;
        m_IsNeedRotate = true;
    }

    /// <summary>
    /// 设置选择的职业描述
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="jobDesc"></param>
    public void SetSelectJobDesc(string jobName, string jobDesc) 
    {
        m_SelectJobName.text = jobName;
        m_SelectJobDesc.text = jobDesc;
    }

    private void Update()
    {
        if(m_IsNeedRotate)
        {
            m_RotateElapsedTime += Time.deltaTime;
            float t = m_RotateElapsedTime / m_RotateTotalTime;
            if(t >= 1)
            {
                t = 1;
                m_IsNeedRotate = false;
            }
            float y = Mathf.Lerp(m_OriginalEulerAngleY, m_TargetEulerAngleY, t);
            m_MainCameraParent.localEulerAngles = new Vector3(0, y, 0);//欧拉角每个维度的范围都是(-180,180]，左开右闭。超出此范围会自动转换到这个范围内
        }
    }
}