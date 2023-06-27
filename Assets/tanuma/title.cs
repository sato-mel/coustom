using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ゲームパッド用
        var GamePad = Gamepad.current;
        bool aButtonDown = false; // 左（左スティック）
        bool bButtonDown = false; // 左（左スティック）


        if (GamePad != null)
        {
            if (GamePad.aButton.isPressed) // aボタンを押した時
            {
                aButtonDown = true;
            }
            if (GamePad.bButton.isPressed) // bボタンを押した時
            {
                bButtonDown = true;
            }
        }


        if (Input.GetKeyDown(KeyCode.Return) || aButtonDown || bButtonDown)
        {
            FadeManager.Instance.LoadScene("stage2_test", 1.0f);
            //SceneManager.LoadScene("stage1");    
        }
    }
}
