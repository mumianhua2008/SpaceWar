using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//可以自定义脚本在菜单栏中的位置
[AddComponentMenu("脚本/MyGame/Player")]

public class Player : MonoBehaviour {

    //定义飞船的生命值
    public float HP = 100;
    //定义飞船移动的速度
    public float movespeed = 6f;
    //获取子弹的预构体
    public Transform rocket;
    //子弹的发射计时器
    public float rocketTimer;
    //加载声音，从面板拖入到当前脚本组件上来添加
    public AudioClip m_shootClip;
    //声音源组件，用于播放声音
    protected AudioSource m_audio;
    //爆炸特效
    //注意爆炸特效是一个预构体，需要用Transform的类接受
    public Transform m_explosionFX;
    //定义目标位置
    // protected Vector3 m_targetPOS;
    //鼠标射线碰撞层
    // public LayerMask m_inputMask;
	void Start () {
        //在初始化的时候获取音效组件
        m_audio = this.GetComponent<AudioSource>();
        //m_targetPOS = this.transform.position;
    }
    void Update () {
        //MoveTo();
        //每次进行刷新的时候的移动侦测
        //在每次都先重置位置
        float movex = 0;
        float movez = 0;
        //上下左右移动
        if (Input.GetKey(KeyCode.W))
        {
            //上
            movez -= movespeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //下
            movez += movespeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //左
            movex += movespeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //右
            movex -= movespeed * Time.deltaTime;
        }
        //移动
        transform.Translate(new Vector3(movex, 0f, movez));
        //发射子弹
        //if (Input.GetMouseButton(0))
        //{
        //   Instantiate(rocket, transform.position, transform.rotation);
        //}
        //连续发射的太快，于是要修改发射频率
        rocketTimer -= Time.deltaTime;
        if (rocketTimer<=0)
        {
            rocketTimer = 0.1f;
            if (Input.GetMouseButton(0))
          {
                //实例化rocket的prefab
             Instantiate(rocket, transform.position, transform.rotation);
                //在实例化之后还可以加上声音
                m_audio.PlayOneShot(m_shootClip);
          }
        }
    }
    //碰撞器
    //一定要注意 碰撞器一定要和刚体组件一起使用
    void OnTriggerEnter(Collider other)
    {
        //定义主角在发生碰撞时的事件
        if (other.tag.CompareTo("PlayerRocket")!=0)
        {
            if (other.tag.CompareTo("Enemy")==0)
            {
                HP -= 5;
            }
            else if(other.tag.CompareTo("SuperEnemy")==0)
            {
                HP -= 15;
            }
            else if (other.tag.CompareTo("EnemyRocket")==0)
            {
                HP -= other.GetComponent<Enemy_rocket>().damage;
            }
            //在UI中显示更改后的生命
            GameManager.Instance.HP_display(HP);
            if (HP <= 0)
            {
                //在销毁之前添加爆炸特效，使用实例化来添加特效
                //Quaternion.identity就是不作旋转
                Instantiate(m_explosionFX, transform.position,Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
    //获取鼠标射线
    //void MoveTo()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        Vector3 ms = Input.mousePosition;
    //        Ray ray1 = Camera.main.ScreenPointToRay(ms);
    //        RaycastHit hitinfo;
    //        bool iscast = Physics.Raycast(ray1, out hitinfo, 1000, m_inputMask);
    //        if (iscast)
    //        {
    //            //如果射中目标，记录射线碰撞点
    //            m_targetPOS = hitinfo.point;
    //        }
    //        //使用Vector3向量提供的MoveTowards函数，获取朝目标移动的位置
    //        Vector3 pos = Vector3.MoveTowards(transform.position, m_targetPOS, 5*movespeed * Time.deltaTime);
    //        //更新位置
    //        this.transform.position = pos;
    //    }
    //}
}
