using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //设置固定分辨率
        //Screen.SetResolution(1024,768,false);

        Invoke("Load", 2);
	}
	

    void Load()
    {
        SceneManager.LoadSceneAsync(1);//异步加载场景
    }
}
