using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("脚本/MyGame/Enemy")]
public class Enemy : MonoBehaviour {

    //初级敌人的一些参数
    public float movespeed;
    public float HP;
    private int enemypoint=2;
    public Transform m_explosionFX;
	void Start () {
        renderer1 = this.GetComponent<Renderer>();
    }
    //刷新中进行的操作
	void Update () {
        UpdateMove();
        //每一帧都侦测是否移动到了屏幕外部
        if (isActiv && !this.renderer1.isVisible)
        {
            Destroy(this.gameObject);
        }
        //让敌人旋转
        //transform.Rotate(new Vector3(0,transform.position.y,0), SpinAngle);
	}
    //为了将来的扩展功能，将UpdateMove写成一个虚函数，继承使用的时候更改更加方便
    protected virtual void UpdateMove()
    {
        //随事件变化的左右随机移动
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;
        //前进和左右随机移动共同呈现了敌人的移动
        transform.Translate(new Vector3(movespeed * Time.deltaTime, 0,rx ));
    }
    //一定要注意 碰撞器一定要和刚体组件一起使用
    private void OnTriggerEnter(Collider other)
    {
        //如果敌人撞到子弹
        if (other.tag.CompareTo("PlayerRocket")==0)
        {
            rocket rocket = other.GetComponent<rocket>();
            if (HP!=0)
            {
                HP -= rocket.damage;
                if (HP<=0)
                {
                    Destroy(this.gameObject);
                    Instantiate(m_explosionFX, transform.position, Quaternion.identity);
                    //如果死亡，就传给GameManager相应增加分数的函数score_up应该增加的函数
                    GameManager.Instance.score_up(enemypoint);
                }
            }
        }
        //如果敌人撞到主角
        else if (other.tag.CompareTo("Player")==0)
        {
            HP = 0;
            Destroy(this.gameObject);
            Instantiate(m_explosionFX, transform.position, Quaternion.identity);
        }
    }
    //侦测是否进入屏幕，并且进行操作
    //首先要定义两个组件
    //1.在初始化Update中获得模型渲染组件renderer
    internal Renderer renderer1;
    //2.决定是否激活的组件
    internal bool isActiv = false;
    private void OnBecameVisible()
    {
        isActiv = true;
    }
}
