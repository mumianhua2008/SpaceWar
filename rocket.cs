using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("脚本/MyGame/Rocket")]
public class rocket : MonoBehaviour {
    //定义子弹的属性
    public float movespeed = 10f;
    public float livetime = 1;
    public float damage = 10f;

	void Start () {
        Destroy(gameObject, livetime);
	}
	
	void Update () {
        transform.Translate(new Vector3(0,0, -movespeed * Time.deltaTime));
	}

    //一定要注意 碰撞器一定要和刚体组件一起使用
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("Enemy")==0 || other.tag.CompareTo("SuperEnemy")==0)
        {
            Destroy(this.gameObject);
        }
    }
}
