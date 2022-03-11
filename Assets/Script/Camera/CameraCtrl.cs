//===============================================
//作    者：
//创建时间：2022-02-21 16:29:55
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    /// <summary>
    /// 控制摄像机上下
    /// </summary>
    [SerializeField]
    private Transform m_CameraUpAndDown;

    /// <summary>
    /// 摄像机缩放父物体
    /// </summary>
    [SerializeField]
    private Transform m_CameraZoomContainer;

    /// <summary>
    /// 摄像机容器
    /// </summary>
    [SerializeField]
    private Transform m_CameraContainer;

    private Transform m_Target;

    public void SetTarget(Transform target)
    {
        m_Target = target; 
    }

    #region OnEnable OnDisable
    void OnEnable()
    {
        FingerEvent.Instance.OnFingerDrag += OnFingerDrag;
        FingerEvent.Instance.OnZoom += OnZoom;
    }

    void OnDisable()
    {
        FingerEvent.Instance.OnFingerDrag -= OnFingerDrag;
        FingerEvent.Instance.OnZoom -= OnZoom;
    }
    #endregion

    #region OnFingerDrag OnZoom
    void OnFingerDrag(FingerEvent.FingerDir dir)
    {
        switch (dir)
        {
            case FingerEvent.FingerDir.Up:
                setCameraUpAndDown(1);
                break;
            case FingerEvent.FingerDir.Down:
                setCameraUpAndDown(0);
                break;
            case FingerEvent.FingerDir.Left:
                SetCameraRotate(0);
                break;
            case FingerEvent.FingerDir.Right:
                SetCameraRotate(1);
                break;
        }
    }
    void OnZoom(FingerEvent.ZoomType type)
    {
        switch (type)
        {
            case FingerEvent.ZoomType.In:
                setCameraZoom(0);
                break;
            case FingerEvent.ZoomType.Out:
                setCameraZoom(1);
                break;
        }
    }
    #endregion

    void Start()
    {
        m_CameraUpAndDown.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_CameraUpAndDown.transform.localEulerAngles.z, 30, 60));
        m_CameraContainer.localPosition = new Vector3(0, 0, Mathf.Clamp(m_CameraContainer.localPosition.z, -5, 5));
    }

    /// <summary>
    /// 设置摄像机旋转
    /// </summary>
    /// <param name="type">0=左 1=右</param>
    public void SetCameraRotate(int type)
    {
        transform.Rotate(new Vector3(0, 40 * Time.deltaTime * (type == 0 ? -1 : 1), 0));
    }

    /// <summary>
    /// 设置摄像机上下
    /// </summary>
    /// <param name="type">0=上 1=下</param>
    public void setCameraUpAndDown(int type)
    {
        m_CameraUpAndDown.Rotate(new Vector3(0, 0, 30 * Time.deltaTime * (type == 0 ? 1 : -1)));
        m_CameraUpAndDown.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_CameraUpAndDown.transform.localEulerAngles.z, 30, 60));
    }

    /// <summary>
    /// 设置摄像机缩放
    /// </summary>
    /// <param name="type">0=拉近 1=拉远</param>
    public void setCameraZoom(int type)
    {
        m_CameraContainer.Translate(Vector3.forward * 20 * Time.deltaTime * (type == 0 ? 1 : -1));
        m_CameraContainer.localPosition = new Vector3(0, 0, Mathf.Clamp(m_CameraContainer.localPosition.z, -5, 5));
    }

    void Update()
    {
        transform.position = m_Target.position;
        m_CameraContainer.LookAt(m_Target.position);
    }
}
