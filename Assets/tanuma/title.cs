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
        // �Q�[���p�b�h�p
        var GamePad = Gamepad.current;
        bool aButtonDown = false; // ���i���X�e�B�b�N�j
        bool bButtonDown = false; // ���i���X�e�B�b�N�j


        if (GamePad != null)
        {
            if (GamePad.aButton.isPressed) // a�{�^������������
            {
                aButtonDown = true;
            }
            if (GamePad.bButton.isPressed) // b�{�^������������
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
