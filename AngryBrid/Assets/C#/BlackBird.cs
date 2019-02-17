﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Brid {

    public List<Pig> blocks = new List<Pig>();

    /// <summary>
    /// 进入触发区域
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Add(collision.gameObject.GetComponent<Pig>());
            
        }
    }

    /// <summary>
    /// 离开触发区域
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }

    public override void ShowSkill()
    {
        base.ShowSkill();
        if(blocks.Count>0 && blocks != null)
        {
            //foreach(var block in  blocks) //foreach 不能移除
            //{
            //    block.Dead();
            //}
           
            for(int i = 0; i < blocks.Count; ++i)
            {
                blocks[i].Dead();
            }
        }
        OnClear();
    }


    /// <summary>
    /// 爆炸以及后续清理
    /// </summary>
    void OnClear()
    {
        rg.velocity = Vector3.zero;
        Instantiate(boom, transform.position, Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        testTrails.ClearTrail();
    }

    public override void Next()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        GameManager._instance.NextBird();
    }
}
