using System.Collections;
using System.Collections.Generic;
//需要调用Unity中的UI类库
using UnityEngine.UI;
//切换场景需要调用Unity中的SceneManagement类库
using UnityEngine.SceneManagement;
using UnityEngine;
[AddComponentMenu("脚本/MyGame/GameManager")]
public class GameManager : MonoBehaviour {
    //静态实例,保存这个组件到instance中,这样所有的脚本都可以通过GameManager这个类来访问他
    //在Start函数中指向自身，这样可以方便在其他类的对象中引用 GameManager 实例
    //这种做法通常只用于只有一个游戏对象的时候
    public static GameManager Instance;
    //显示分数的UI界面
    public Transform m_canvas_main;
    //显示游戏失败的游戏界面
    public Transform m_canvas_gameover;
    //得分UI文字
    public Text m_text_score;
    //最高分UI文字
    public Text m_text_best;
    //生命UI文字
    public Text m_text_HP;

    //游戏得分
    protected int m_score=0;
    //最高分
    public static int m_bestscore=0;
    //游戏主角
    protected Player m_player;

    //背景音乐
    public AudioClip m_background_music;
    //背景音乐声音源
    public AudioSource m_background_source;

    void Start () {
        //获取实例
        Instance = this;
        //使用代码来添加音效组件
        m_background_source = this.gameObject.AddComponent<AudioSource>();
        m_background_source.clip = m_background_music;
        m_background_source.loop = true;
        m_background_source.Play();

        //获取主角
        //这里获取的Player组件是 Player脚本组件
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //获取UI
        m_text_score = m_canvas_main.Find("Text_Score").GetComponent<Text>();
        m_text_best = m_canvas_main.Find("Text_Best").GetComponent<Text>();
        m_text_HP = m_canvas_main.Find("Text_HP").GetComponent<Text>();
        //初始化UI显示
        m_text_score.text = string.Format("分数   {0}",m_score);
        m_text_best.text = string.Format("最佳   {0}", m_bestscore);
        m_text_HP.text = string.Format("HP   {0}", m_player.HP);

        //var是隐式定义
        //获取重新开始按钮
        var restart_button = m_canvas_gameover.transform.Find("Button").GetComponent<Button>();
        //按钮事件回调，重新开始关卡
        //监听事件,定义delegate委托类型来传递方法
        restart_button.onClick.AddListener(delegate(){
            //中间是传递的方法
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
        //在游戏开始的时候默认游戏结束界面是隐藏的，即m_canvas_gameover的游戏对象组件默认是不显示的
        m_canvas_gameover.gameObject.SetActive(false);
    }

    //设置游戏分数增加的函数
    public void score_up(int point) {
        m_score += point;
        //每次都更新高分记录
        if (m_score > m_bestscore)
        {
            m_bestscore = m_score;
        }
        //刷新输出分数和最高分
        //通过不断更改Text中的文本就可以实现
        m_text_score.text = string.Format("分数    {0}", m_score);
        m_text_best.text = string.Format("最佳    {0}", m_bestscore);
    }

    //设置更改生命显示的函数
    public void HP_display(float life)
    {
        //不断更新生命值
        m_text_HP.text = string.Format("HP     {0}", m_player.HP);
        if (m_player.HP <= 0)
        {
            //通过设置游戏结束场景为显示就可以切换到游戏结束界面
            m_canvas_gameover.gameObject.SetActive(true);
        }
    }
}
