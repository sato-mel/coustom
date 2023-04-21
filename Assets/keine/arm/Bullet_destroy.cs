using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_destroy : MonoBehaviour
{


    [SerializeField]
    private GameObject Point;

    [SerializeField]
    private GameObject eff;

    [SerializeField]
    private float Destroy_time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 m_Position = Point.transform.position;
        // ��Ŏ擾�����ꏊ�ɁA"bullet"��Prefab���o��������
        GameObject Penetration_Bullet = Instantiate(eff, m_Position, transform.rotation);
        // �o���������{�[����forward(z������)
        Vector3 direction = Penetration_Bullet.transform.right;
        // �e�̔��˕�����newBall��z����(���[�J�����W)�����A�e�I�u�W�F�N�g��rigidbody�ɏՌ��͂�������

        // �o���������{�[���̖��O��"bullet"�ɕύX
        Penetration_Bullet.name = eff.name;
        // �o���������{�[����0.8�b��ɏ���
        Destroy(Penetration_Bullet, Destroy_time);




    }
    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "RightWall" || other.gameObject.tag == "LeftWall")
        {
            Destroy(this.gameObject);
        }
    }

}
