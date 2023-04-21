using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body_custom : MonoBehaviour
{
    [SerializeField]
    [Tooltip("装備場所")]
    private GameObject backpackPoint;


    [SerializeField]
    [Tooltip("滑空装備")]
    private GameObject glidingEquipment;


    // Update is called once per frame
    void Update()
    {

        GlidingLv();

        /*// グライダ起動切り替え   on/off 
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (CPData.Glid_Flag == false)
            { CPData.Glid_Flag = true; }
            else
            { CPData.Glid_Flag = false; }
        }*/

        // 動作ボタン    motionBottan
        if (CPData.Glid_Flag == true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (CPData.Glid_Lv1)
                {
                    // 通常グライダー  normalGlider
                    Glider_Lv1();
                }
                if (CPData.Glid_Lv2)
                {
                    // 通常グライダー  normalGlider
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
        //  レベル上昇   levelUp
        if(Input.GetKeyDown(KeyCode.O)) // Oは仮置き    temporary storage
        {
            Debug.Log("LvUp");
            Debug.Log(CPData.Glid_Lv);
            CPData.Glid_Lv += 1;

        }
        //  レベル下降   levelDown
        if (Input.GetKeyDown(KeyCode.P)) // Oは仮置き    temporary storage
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
