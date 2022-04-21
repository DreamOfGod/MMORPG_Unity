//===============================================
//作    者：
//创建时间：2022-04-21 16:40:50
//备    注：
//===============================================
using UnityEngine;

public class SelectRoleSceneView : MonoBehaviour
{
    [SerializeField]
    private Transform m_MainCameraParent;
    [SerializeField]
    private float m_CameraRotateSpeed = 100;

    private float m_OriginalEulerAngleY;
    private float m_TargetEulerAngleY;
    private float m_RotateTotalTime;
    private float m_RotateElapsedTime;
    private bool m_IsRotating = false;

    public void RotateCamera(UIDirection dir)
    {
        if(dir == UIDirection.LEFT)
        {
            //向左旋转，eulerAngleY增大
            int curEulerAngleY = (int)m_MainCameraParent.localEulerAngles.y;
            m_TargetEulerAngleY = (curEulerAngleY / 90 + 1) * 90;
            m_OriginalEulerAngleY = m_MainCameraParent.localEulerAngles.y;
            m_RotateTotalTime = Mathf.Abs(m_TargetEulerAngleY - m_OriginalEulerAngleY) / m_CameraRotateSpeed;
            m_RotateElapsedTime = 0;
            m_IsRotating = true;
        }
        else if(dir == UIDirection.RIGHT)
        {
            //向右旋转，eulerAngleY减小
            int curEulerAngleY = (int)m_MainCameraParent.localEulerAngles.y;
            m_TargetEulerAngleY = (curEulerAngleY / 90 - 1) * 90;
            m_OriginalEulerAngleY = m_MainCameraParent.localEulerAngles.y;
            m_RotateTotalTime = Mathf.Abs(m_TargetEulerAngleY - m_OriginalEulerAngleY) / m_CameraRotateSpeed;
            m_RotateElapsedTime = 0;
            m_IsRotating = true;
        }
    }

    private void Update()
    {
        if(m_IsRotating)
        {
            m_RotateElapsedTime += Time.deltaTime;
            float t = m_RotateElapsedTime / m_RotateTotalTime;
            if(t >= 1)
            {
                t = 1;
                m_IsRotating = false;
            }
            float y = Mathf.Lerp(m_OriginalEulerAngleY, m_TargetEulerAngleY, t);
            m_MainCameraParent.localEulerAngles = new Vector3(0, y, 0);
        }
    }
}