using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour {

    private Animator animator;
    public GameObject button;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }


    /// <summary>
    /// 点击了pause
    /// </summary>
    public void Pause()
    {
        //1.播放动画
        //2.暂停
        animator.SetBool("isPause", true);
        button.SetActive(false);

        
        if (GameManager._instance.birds.Count > 0)
        {
            //如果没有飞出，不能进行操作
            if (GameManager._instance.birds[0].isReleased == false)
            {
                GameManager._instance.birds[0].canMove = false;
            }

        }
    }




    //点击继续按钮
    public void Resume()
    {
        Time.timeScale = 1;//为1才能开始播放动画
        animator.SetBool("isPause", false);

        if (GameManager._instance.birds.Count > 0)
        {
            //如果没有飞出，不能进行操作
            if (GameManager._instance.birds[0].isReleased == false)
            {
                GameManager._instance.birds[0].canMove = true;
            }

        }
    }


    //动画播放完后 暂停
    public void PauseAnimEnd()
    {
        Time.timeScale = 0;//0表示暂停
    }

    public void ResumeAnimEnd()
    {
        button.SetActive(true);//继续动画播放完后显示暂停按钮
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
