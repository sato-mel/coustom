using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_attack : MonoBehaviour
{
    [SerializeField]
    private GameObject firingPoint;

    [SerializeField]
    private GameObject firingPoint2;

    [SerializeField]
    private GameObject firingPoint3;

    [SerializeField]
    private GameObject firingPoint4;

    [SerializeField]
    [Tooltip("UŒ‚")]
    private GameObject attack;


    private float startCount = 0.0f;

    private bool isAttack = true;

    [SerializeField]
    private float destroyTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CPData.BOSS_start2)
        {
            startCount += Time.deltaTime;
            if (startCount > 3 && isAttack)
            {
                Attack_bom();
                // startCount = 0;
            }
        }


    



    }


    void Attack_bom()
    {
        int speed = 500;
        isAttack = false;


        
        // ’e‚ğ”­Ë‚·‚éêŠ‚ğæ“¾
        Vector3 attackPosition1 = firingPoint.transform.position;
        // ã‚Åæ“¾‚µ‚½êŠ‚ÉA"bullet"‚ÌPrefab‚ğoŒ»‚³‚¹‚é
        GameObject attackEff = Instantiate(attack, attackPosition1, transform.rotation);
        Vector3 direction = attackEff.transform.right;
        attackEff.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Force);


        ////
        //Vector3 attackPosition2 = firingPoint2.transform.position;
        //GameObject attackEff2 = Instantiate(attack, attackPosition2, transform.rotation);

        ////
        //Vector3 attackPosition3 = firingPoint3.transform.position;
        //GameObject attackEff3 = Instantiate(attack, attackPosition3, transform.rotation);

        ////
        //Vector3 attackPosition4 = firingPoint4.transform.position;
        //GameObject attackEff4 = Instantiate(attack, attackPosition4, transform.rotation);

        
        //Destroy(attackEff, destroyTime);
        //Destroy(attackEff2, destroyTime);
        //Destroy(attackEff3, destroyTime);
        //Destroy(attackEff4, destroyTime);

    }








}
