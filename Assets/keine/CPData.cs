using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPData : MonoBehaviour
{
    public static bool Right = true;
    

    public static int ArmShot_norml_Lv = 1;
    public static bool ArmShot_norml_Lv1 = true;
    public static bool ArmShot_norml_Lv2 = false;
    public static bool ArmShot_norml_Lv3 = false;

    public static int ArmShot_Penetration_Lv = 1;
    public static bool ArmShot_Penetration_Lv1 = true;
    public static float Penetration_speed=500.0f;



    public static bool CustomNo1 = true;
    public static bool CustomNo2 = false;


    public static bool CustomHigh = false;

    public static bool BOSS_start = false;
    public static bool BOSS_start2 = false;

    //ボスのスクリプトにて使用
    public static bool LastResort = false;


    // BodyCustom、LegCustom用
    public static bool BodyLegCustom_LvUp = false;
}
