using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("脚本/MyGame/EnemySpawn")]
public class EnemySpawn : MonoBehaviour {

    //需要调用协程
    //IEnumerator是协程函数的标识符，它的功能有些类似线程
    //注意将敌人生成器制作成两个，一个是普通敌人，另一个是高级敌人，但是两者的脚本一样

    //获取敌人的预构体
    public Transform m_enemy;

	void Start () {
        //初始化的时候调用协程
        StartCoroutine(SpawnEnemy());
	}
	
    IEnumerator SpawnEnemy ()
    {
        //协程一定要有yield return 返回
        yield return new WaitForSeconds(Random.Range(5f, 30f));
        //在生成器的位置实例化一个个敌人
        //使用协程可以在不堵塞主线程的情况下，让函数进行等待，每隔几秒创建一个敌人，然后循环这个过程
        //调用协程函数一定要使用 StartCoroutine(协程函数()) 函数

        Instantiate(m_enemy, transform.position, transform.rotation);
        //循环进行协程操作，不断产生敌人
        StartCoroutine(SpawnEnemy());
    }
    //用来显示敌人生成器图标的函数，这样方便摆放和设置
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position,"item.png",true);
    }
}
