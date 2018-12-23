using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("脚本/MyGame/SuperEnemy")]
public class SuperEnemy : Enemy
{
    //模型旋转角度
    public float SpinAngle;
    private int enemypoint_super = 7;
    //获取音效资源
    public AudioClip m_audioClip;
    //获取音效组件
    protected AudioSource m_audio;
    //获取敌人子弹的预构体
    public Transform Enemy_rocket;
    protected float ShootTime = 2f;
    //出生的时候查找主角
    protected Transform player;
    private void Awake()
    {
        GameObject gameObject = GameObject.Find("Player");
        if (gameObject != null)
        {
            //获取主角的位置
            player = gameObject.transform;
        }
        //注意在初始化的时候记得获取音效组件
        m_audio = GetComponent<AudioSource>();
    }

    //重新定义移动
    protected override void UpdateMove()
    {
        ShootTime -= Time.deltaTime;
        //射击时间间隔为2s
        if (ShootTime <= 0)
        {
            ShootTime = 2;
            if (player != null)
            {
                //使用向量计算子弹在飞出时的朝向---向量减法
                Vector3 relativePOS = transform.position - player.position;
                //实例化敌人子弹并且设定朝向为玩家方向
                Instantiate(Enemy_rocket, this.transform.position, Quaternion.LookRotation(relativePOS));
                //实例化后可以输出射击音效
                m_audio.PlayOneShot(m_audioClip);
            }
        }
        //自身不断向前移动
        transform.Translate(new Vector3(0, 0,-movespeed * Time.deltaTime));
    }
    //重写高级敌人撞到子弹的判断函数
    private void OnTriggerEnter(Collider other)
    {
        //如果敌人撞到子弹
        if (other.tag.CompareTo("PlayerRocket") == 0)
        {
            rocket rocket = other.GetComponent<rocket>();
            if (HP != 0)
            {
                HP -= rocket.damage;
                if (HP <= 0)
                {
                    Destroy(this.gameObject);
                    Instantiate(m_explosionFX, transform.position, Quaternion.identity);
                    //如果死亡，就传给GameManager相应增加分数的函数score_up应该增加的函数
                    GameManager.Instance.score_up(enemypoint_super);
                }
            }
        }
        //如果敌人撞到主角
        else if (other.tag.CompareTo("Player") == 0)
        {
            HP = 0;
            Destroy(this.gameObject);
            Instantiate(m_explosionFX, transform.position, Quaternion.identity);
        }
    }
}
