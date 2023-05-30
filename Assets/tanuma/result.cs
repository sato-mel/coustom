using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class result : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            FadeManager.Instance.LoadScene("title", 1.0f);
            //SceneManager.LoadScene("stage1");    
        }
    }
}
