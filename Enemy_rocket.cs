
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("脚本/MyGame/Enemy_rocket")]
public class Enemy_rocket : rocket {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("Player") != 0)
        {
            //print("不是玩家");
            return;
        }
       // print("是玩家");
        Destroy(this.gameObject);
    }
}
