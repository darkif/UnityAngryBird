using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public List<Brid> birds;
    public List<Pig> pigs;
    public static GameManager _instance;//单例模式
    private Vector3 originPos;

    public GameObject lose;
    public GameObject win;
    public GameObject[] starts;

    private int startsNum=0;

    private int totalLevel = 7;

    void Awake()
    {
        _instance = this;
        if (birds.Count > 0)
        {
            originPos = birds[0].transform.position;
        }
    }

    void Start()
    {
        initialize();
    }

    //初始化小鸟
    private void initialize()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if (i == 0)
            {
                birds[0].canMove = true;
                birds[0].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].springJoint.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].springJoint.enabled = false;
            }
        }
    }
    //判断游戏逻辑
   public  void NextBird()
    {
        if (pigs.Count > 0)
        {
            if (birds.Count > 0)
            {
                //下一只鸟飞
                initialize();
            }
            else
            {
                //lose
                lose.SetActive(true);
            }
        }
        else
        {
            //win
            win.SetActive(true);
        }
    }

    public void ShowStarts()
    {
        StartCoroutine("Show");//协同，星星一个一个出现
        
    }

    IEnumerator Show()
    {
        for(; startsNum < birds.Count+1; ++startsNum)
        {
            if (startsNum > starts.Length)
            {
                break;
            }
            yield return new WaitForSeconds(0.2f);
            starts[startsNum].SetActive(true);
        }
       
    }
 
    public void Replay()
    {
        SaveData();
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        if (startsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            //把startsNum赋值给nowLevel  nowLevel="level"+关卡名字
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), startsNum);
        }
        //nowLevel保存关卡名，第二个是关卡名和对应星星数关联
        //print(PlayerPrefs.GetString("nowLevel"));
        //print(PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")));
        //print(startsNum);
        //存储所有星星的个数
        int sum = 0;
        for(int i = 1; i <totalLevel; i++)
        {

            //print("level" + i.ToString());
           
            sum += PlayerPrefs.GetInt("level" + i.ToString());
        
        }
        PlayerPrefs.SetInt("totalNum", sum);
        /* print(PlayerPrefs.GetInt("totalNum"));*/
    }
}
