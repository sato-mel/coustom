using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body_custom : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�����ꏊ")]
    private GameObject backpackPoint;


    [SerializeField]
    [Tooltip("���󑕔�")]
    private GameObject glidingEquipment;


    // Update is called once per frame
    void Update()
    {

        GlidingLv();

        /*// �O���C�_�N���؂�ւ�   on/off 
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (CPData.Glid_Flag == false)
            { CPData.Glid_Flag = true; }
            else
            { CPData.Glid_Flag = false; }
        }*/

        // ����{�^��    motionBottan
        if (CPData.Glid_Flag == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (CPData.Glid_Lv1)
                {
                    // �ʏ�O���C�_�[  normalGlider
                    Glider_Lv1();
                }
                if (CPData.Glid_Lv2)
                {
                    // �ʏ�O���C�_�[  normalGlider
                    Glider_Lv2();
                }
            }
            if(Input.GetKeyUp(KeyCode.Space))
            {
                Glider_Neutral();
            }
        }
    }

    private void GlidingLv()
    {
        //  ���x���㏸   levelUp
        if(Input.GetKeyDown(KeyCode.O)) // O�͉��u��    temporary storage
        {
            Debug.Log("LvUp");
            Debug.Log(CPData.Glid_Lv);
            CPData.Glid_Lv += 1;

        }
        //  ���x�����~   levelDown
        if (Input.GetKeyDown(KeyCode.P)) // O�͉��u��    temporary storage
        {
            Debug.Log("LvDown");
            Debug.Log(CPData.Glid_Lv);
            CPData.Glid_Lv -= 1;

        }

        if (CPData.Glid_Lv == 1)
        {
            GlidFalse();
            CPData.Glid_Lv1 = true;
        }
        if (CPData.Glid_Lv == 2) 
        {
            GlidFalse();
            CPData.Glid_Lv2 = true;
        }

        
    }

    private void GlidFalse()
    {
        CPData.Glid_Lv1 = false;
        CPData.Glid_Lv2 = false;
    }


    private void Glider_Neutral()
    {
        Physics.gravity = new Vector3(0.0f, 1.0f, 0.0f);
    }
    private void Glider_Lv1()
    {
        Physics.gravity = new Vector3(0.0f, 0.5f, 0.0f);
    }
    private void Glider_Lv2()
    {
        Physics.gravity = new Vector3(0.0f, 0.5f, 0.0f);
    }

    private void Appearance_Glid()
    {
        if(CPData.Glid_Flag = true)
        {

        }
    }

}
