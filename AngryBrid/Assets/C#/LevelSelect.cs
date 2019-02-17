using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {

    public bool isSelect = false;
    public Sprite levelBg;
    private Image image;

    public GameObject[] starts;

    void Awake()
    {
        image = GetComponent<Image>();
    }


	void Start () {
        //如果是第一关则可以选择
        if (transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
            
        }
        else//判断当前关卡是否可以选择
        {
            //获取前一个关卡数字
            int beforeLevel = int.Parse(gameObject.name)-1;//把字符串转换为数字  

            //"level"+关卡名用来保存当前关卡星星个数
            if (PlayerPrefs.GetInt("level" + beforeLevel.ToString()) >0)
            {
                isSelect = true;
            }
        }

        if (isSelect)
        {
            image.overrideSprite = levelBg;
            int count = PlayerPrefs.GetInt("level"+gameObject.name);//获取当前关卡的名字，然后获得对应的星星个数
            //print(count);
            if (count > 0)
            {
                for(int i = 0; i < count; i++)
                {
                    starts[i].SetActive(true);
                }
            }
        }
	}

    public void Selected()
    {
        if (isSelect)
        {
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);
         
            SceneManager.LoadScene(2);
        }
    }

}
