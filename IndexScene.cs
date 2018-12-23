﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[AddComponentMenu("脚本/MyGame/IndexScece")]

public class IndexScene : MonoBehaviour {
    
	void Start () {
		
	}
	void Update () {
		
	}
    private void OnGUI()
    {
        //文字大小
        GUI.skin.label.fontSize = 50;
        //UI中心对齐
        GUI.skin.label.alignment = TextAnchor.LowerCenter;
        //显示标题
        GUI.Label(new Rect(0, 30, Screen.width, 100), "太空大战");
        //开始游戏按钮
        if (GUI.Button(new Rect(Screen.width * 0.5f - 100, Screen.height * 0.7f, 200, 30), "开始游戏"))
        {
            SceneManager.LoadScene("level1");
        }
    }
}
