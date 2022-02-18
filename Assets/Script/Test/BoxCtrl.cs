//===============================================
//作    者：
//创建时间：2022-02-18 10:48:13
//备    注：
//===============================================
using UnityEngine;

public class BoxCtrl : MonoBehaviour
{
    private Vector3 m_TargetPos = Vector3.zero;

    [SerializeField]
    private float m_Speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.name.Equals("Ground", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    m_TargetPos = hitInfo.point;
                }
            }
        }

        if (m_TargetPos != Vector3.zero)
        {
            if (Vector3.Distance(m_TargetPos, transform.position) > 0.1f)
            {
                //transform.LookAt(m_TargetPos);
                //transform.Translate(Vector3.forward * Time.deltaTime * m_Speed);
                Vector3 dir = (m_TargetPos - transform.position).normalized;
                transform.position = transform.position + dir * Time.deltaTime * m_Speed;
                
            }
        }
    }
}
