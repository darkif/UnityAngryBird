using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelect : MonoBehaviour {

    public int startsNum = 0;
    public bool isSelect = false;

    public GameObject myLock;
    public GameObject start;

    public GameObject panel;
    public GameObject map;

    public Text starsText;

    //关卡开始和结束
   public int startnNum = 1;
    public int endNum = 6;

    void Start()
    {
        //清理所有数据，导出的时候使用
        //PlayerPrefs.DeleteAll();


        //PlayerPrefs Unity自带存储，采用键值对
        if (PlayerPrefs.GetInt("totalNum",0)>=startsNum)//"totalNum",0 表示totalNum没有存储过的话为0
        {
            isSelect = true;
        }

        if (isSelect)
        {
            myLock.SetActive(false);
            start.SetActive(true);

            //text显示该map已获得星星数
            int counts = 0;
            for(int i = startnNum; i <= endNum; i++)
            {
                counts += PlayerPrefs.GetInt("level" + i.ToString(),0);

            }
            starsText.text = counts.ToString();
        }
    }


    /// <summary>
    /// 鼠标点击
    /// </summary>
    public void Selected()
    {
        if (isSelect)
        {
            panel.SetActive(true);
            map.SetActive(false);
        }
    }
}
