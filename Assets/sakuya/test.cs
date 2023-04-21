using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private int upForce;

    private bool isGlid = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        Glid();

    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            isGlid = true;
        }
        if(Input.GetKeyUp(KeyCode.U))
        {
            isGlid = false;
        }
    }

    void Glid()
    {           
    //  this.rb.AddForce(new Vector3(0, -1, 0));

        if (isGlid)
        {
            this.rb.AddForce(new Vector3(0, upForce, 0));
        }
    }
}
