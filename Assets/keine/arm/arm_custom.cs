using UnityEngine;

public class arm_custom : MonoBehaviour
{
    [SerializeField]
    [Tooltip("弾の発射場所")]
    private GameObject firingPoint;

    [SerializeField]
    [Tooltip("弾の発射場所二個目")]
    private GameObject firingPoint2;

    [SerializeField]
    [Tooltip("弾の発射場所3個目")]
    private GameObject firingPoint3;

    [SerializeField]
    [Tooltip("弾の発射場所4個目")]
    private GameObject firingPoint4;

    [SerializeField]
    [Tooltip("弾の発射場所5個目")]
    private GameObject firingPoint5;

    [SerializeField]
    [Tooltip("弾")]
    private GameObject bullet;

    [SerializeField]
    [Tooltip("弾2")]
    private GameObject bullet2;

    [SerializeField]
    [Tooltip("弾3")]
    private GameObject bullet3;

    [SerializeField]
    [Tooltip("弾4")]
    private GameObject bullet4;

    [SerializeField]
    [Tooltip("弾5")]
    private GameObject bullet5;

    [SerializeField]
    [Tooltip("弾6")]
    private GameObject bullet6;

    private GameObject custom;


    [Tooltip("弾の速さ")]
    private float speed;

    // Update is called once per frame
    void Update()
    {

        BulletLv();







        //発射ボタン
        if (Input.GetKeyDown(KeyCode.G))
        {
            //メインカスタム
            if (CPData.CustomNo1)
            {
                if (CPData.ArmShot_norml_Lv1)
                {
                    // ノーマルの弾
                    ShotNolmal_Lv1();
                }
                if (CPData.ArmShot_norml_Lv2)
                {
                    //ノーマルの弾レベル2
                    ShotNolmal_Lv2();
                }
            }
            //サブカスタム
            if (CPData.CustomNo2)
            {
                //  Debug.Log("aaaaaaaaaaaaaaaaaaa");
                ShotPenetrationLv1();

            }




        }








    }


    private void BulletLv()
    {
        //レベルアップ
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("レベルアップ");
            Debug.Log(CPData.ArmShot_norml_Lv);
            CPData.ArmShot_norml_Lv += 1;
        }
        //レベルダウン
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("レベルダウン");
            CPData.ArmShot_norml_Lv -= 1;
        }
        //メインウェポン
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Custom_false();
            CPData.CustomNo1 = true;
            // Debug.Log("メインウェポンに変わった");
        }
        //サブウェポン
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Custom_false();
            CPData.CustomNo2 = true;
            // Debug.Log("サブウェポンに変わった");
        }




        if (CPData.ArmShot_norml_Lv == 1)
        {
            //  Debug.Log("レベル1");
            Bullet_false();
            CPData.ArmShot_norml_Lv1 = true;

        }
        if (CPData.ArmShot_norml_Lv == 2)
        {
            //  Debug.Log("レベル2");
            Bullet_false();
            CPData.ArmShot_norml_Lv2 = true;
        }
    }

    private void Bullet_false()
    {
        CPData.ArmShot_norml_Lv1 = false;
        CPData.ArmShot_norml_Lv2 = false;
    }

    private void Custom_false()
    {
        CPData.CustomNo1 = false;
        CPData.CustomNo2 = false;
    }



    /// ノーマル弾の発射
    private void ShotNolmal_Lv1()
    {
        //弾の速さ
        speed = 600;

        // 弾を発射する場所を取得
        Vector3 bulletPosition = firingPoint.transform.position;
        // 上で取得した場所に、"bullet"のPrefabを出現させる
        GameObject newBall = Instantiate(bullet, bulletPosition, transform.rotation);
        // 出現させたボールのforward(z軸方向)
        Vector3 direction = newBall.transform.right;
        // 弾の発射方向にnewBallのz方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加える
        if (CPData.Right)
        {
            newBall.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Force);
        }
        else
        {
            //弾飛んでく方向反転
            newBall.GetComponent<Rigidbody2D>().AddForce(direction * -speed, ForceMode2D.Force);
        }
        // 出現させたボールの名前を"bullet"に変更
        newBall.name = bullet.name;
        // 出現させたボールを0.8秒後に消す
        Destroy(newBall, 5f);


        //   newBall;
    }
    /// ノーマル弾の発射Lv2
    private void ShotNolmal_Lv2()
    {


        //弾の速さ
        speed = 1000;

        // 弾を発射する場所を取得
        //  Vector3 bulletPosition = firingPoint.transform.position;
        Vector3 bulletPosition2 = firingPoint2.transform.position;
        // 上で取得した場所に、"bullet"のPrefabを出現させる
        // GameObject Bullet = Instantiate(bullet, bulletPosition, transform.rotation);
        GameObject BulletLv2 = Instantiate(bullet2, bulletPosition2, transform.rotation);
        // 出現させたボールのforward(z軸方向)
        Vector3 direction = BulletLv2.transform.right;
        //  Vector3 direction2 = BulletLv2.transform.right;
        // 弾の発射方向にnewBallのz方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加える
        if (CPData.Right)
        {
            //  Bullet.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Force);
            BulletLv2.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Force);
        }
        else
        {
            //弾飛んでく方向反転
            // Bullet.GetComponent<Rigidbody2D>().AddForce(direction * -speed, ForceMode2D.Force);
            BulletLv2.GetComponent<Rigidbody2D>().AddForce(direction * -speed, ForceMode2D.Force);
        }
        // 出現させたボールの名前を"bullet"に変更
        // Bullet.name = bullet.name;
        BulletLv2.name = bullet2.name;
        // 出現させたボールを0.8秒後に消す
        // Destroy(Bullet, 5f);
        Destroy(BulletLv2, 5f);




    }

    void ShotPenetrationLv1()
    {
        //弾の速さ
        speed = CPData.Penetration_speed;

        // 弾を発射する場所を取得
        Vector3 bulletPosition = firingPoint3.transform.position;
        // 上で取得した場所に、"bullet"のPrefabを出現させる
        GameObject Penetration_Bullet = Instantiate(bullet3, bulletPosition, transform.rotation);
        // 出現させたボールのforward(z軸方向)
        Vector3 direction = Penetration_Bullet.transform.right;
        // 弾の発射方向にnewBallのz方向(ローカル座標)を入れ、弾オブジェクトのrigidbodyに衝撃力を加える
        if (CPData.Right)
        {
            Penetration_Bullet.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Force);
        }
        else
        {
            //弾飛んでく方向反転
            Penetration_Bullet.GetComponent<Rigidbody2D>().AddForce(direction * -speed, ForceMode2D.Force);
        }
        // 出現させたボールの名前を"bullet"に変更
        Penetration_Bullet.name = bullet.name;
        // 出現させたボールを0.8秒後に消す
        Destroy(Penetration_Bullet, 1.0f);

    }


    void Body_armerLv1()
    {

    }

    void Leg_armer()
    {

    }


}
















