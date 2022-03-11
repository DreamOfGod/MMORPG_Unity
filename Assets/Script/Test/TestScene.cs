//===============================================
//作    者：
//创建时间：2022-03-11 10:36:49
//备    注：
//===============================================
//===============================================
//作    者：
//创建时间：2022-03-11 10:36:49
//备    注：
//===============================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    [SerializeField]
    private CharacterController m_CharacterController;

    private GameObject box;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject boxPrefab = Resources.Load<GameObject>("Cube");
            box = Instantiate(boxPrefab);
            box.transform.position = m_CharacterController.transform.position;
            box.GetComponent<CharacterController>().Move(new Vector3(0, -1000, 0));
        }
        

        m_CharacterController.Move(new Vector3(0, -1000, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
