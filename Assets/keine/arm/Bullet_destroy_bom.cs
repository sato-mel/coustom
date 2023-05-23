using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_destroy_bom : MonoBehaviour
{


    [SerializeField]
    private GameObject Point;

    [SerializeField]
    private GameObject eff;

    [SerializeField]
    private float Destroy_time;

  //  GameObject Penetration_Bullet=null;

    float effCount=0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 m_Position = Point.transform.position;
        // 上で取得した場所に、"bullet"のPrefabを出現させる


        effCount += Time.deltaTime;


        if (effCount > 2)
        {
            GameObject Penetration_Bullet = Instantiate(eff, m_Position, transform.rotation);
            effCount = 0.0f;

            // 出現させたボールのforward(z軸方向)
            //  Vector3 direction = Penetration_Bullet.transform.right;
            // 弾の発射方向にnewBallのz方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加える

            // 出現させたボールの名前を"bullet"に変更
            Penetration_Bullet.name = eff.name;
            // 出現させたボールを0.8秒後に消す
            Destroy(Penetration_Bullet, Destroy_time);

        }


    }
    private void OnTriggerEnter2D(Collider2D other)
    { 
        //if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "RightWall" || other.gameObject.tag == "LeftWall" || other.gameObject.tag == "BOSS")
        //{
        //    Destroy(this.gameObject);
        //}
    }

}
