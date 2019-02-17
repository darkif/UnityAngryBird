using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelReturn : MonoBehaviour {

    public GameObject map;
    public GameObject panel;

    void Start()
    {
        
    }


    public void ReturnToMap()
    {
        map.SetActive(true);
        panel.SetActive(false);
    }
}
