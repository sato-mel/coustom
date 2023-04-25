using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body_custom : MonoBehaviour
{
    private Rigidbody2D rb;


    [SerializeField]
    [Tooltip("‘•”õêŠ")]
    private GameObject backpackPoint;

    [SerializeField]
    [Tooltip("ŠŠ‹ó‘•”õ")]
    private GameObject glidingEquipment;


    private int normalUpForce;
    private bool isGlid = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalUpForce = 12;

        // ‘•”õ‚ğ•\¦‚·‚éêŠ‚Ìæ“¾
 //       Vector3 backPackPosition = backpackPoint.transform.position;
        // ‘•”õ‚Ì•\¦
 //       GameObject newGulid = Instantiate(glidingEquipment, backPackPosition, transform.rotation);

    }

    void FixedUpdate()
    {
        Glid();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isGlid = true;
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            isGlid = false;
        }

    }

    void Glid()
    {
        if(isGlid)
        {

            this.rb.AddForce(new Vector3(0, normalUpForce, 0));
        }

    }


}
