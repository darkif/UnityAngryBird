using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

    public float maxSpeed = 10;
    public float minSpedd = 5;
    public Sprite hurt;
    public GameObject boom;
    public GameObject score;
    private SpriteRenderer render;

    public bool isPig = false;

    public AudioClip hurtClip;
    public AudioClip deathClip;
    public AudioClip birdCollisionClip;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioPlay(birdCollisionClip);
            collision.gameObject.GetComponent<Brid>().Hurt();
        }

        if (collision.relativeVelocity.magnitude > maxSpeed)//相对速度大于max,magnitude表示把向量数字化
        {
            //直接死亡
            Dead();
        }
        else if(collision.relativeVelocity.magnitude>minSpedd && collision.relativeVelocity.magnitude < maxSpeed)
        {
            //受伤
            AudioPlay(hurtClip);
            render.sprite = hurt;
        }
    }

   public  void Dead()
    {
        if (isPig)
        {
            GameManager._instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        
        GameObject go = Instantiate(score, transform.position+new Vector3(0,0.5f,0), Quaternion.identity);
        Destroy(go, 1.5f);//1.5s后销毁物体
        AudioPlay(deathClip);
    }


    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
