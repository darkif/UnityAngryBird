using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Brid : MonoBehaviour {

    private bool isClick = false;
    [HideInInspector]//在属性面板隐藏公有属性
    public  SpringJoint2D springJoint;

    protected Rigidbody2D rg;
    
    public float maxDis = 1.4f;

    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;

    public GameObject boom;

    private TestTrails trails;
    [HideInInspector]
    public bool canMove = false;//如果默认为true，其他小鸟一开也可以点，由于鼠标点击事件不会被禁用

    public float smooth = 3;

    public AudioClip selectAudio;
    public AudioClip flyAudio;

    private bool isFly = false;
    public bool isReleased = false;//鼠标是否抬起,是否释放小鸟

    public Sprite hurt;
    protected SpriteRenderer render;

    protected TestTrails testTrails;

    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();
        trails = GetComponent<TestTrails>();
        render = GetComponent<SpriteRenderer>();
        testTrails=GetComponent<TestTrails>();
    }


    private void OnMouseDown()//鼠标按下
    {

        if (canMove)//飞出去后就不能点击
        {
            AudioPlay(selectAudio);
            isClick = true;
            rg.isKinematic = true;//开启动力学
        }
    }

    private void OnMouseUp()
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f);//延时调用，不然关闭动力学后飞不动
                                //禁用画线
            right.enabled = false;
            left.enabled = false;
        }
       
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())//判断是否点击了UI界面
        {
            //note:取消不想交互的UI 在ui下去掉Raycast Target的勾选
            return;//点击了UI就返回
        }
        if (isClick)//鼠标一直按下
        {
            //进行位置的跟随
            transform.position =Camera.main.ScreenToWorldPoint( Input.mousePosition);//鼠标位置转换为屏幕坐标再赋值给小鸟
            //运行后小鸟的z轴和摄像机一样，所以运行的时候会看不到小鸟（3d可以看到）
            //限定小鸟z轴坐标
            //transform.position += new Vector3(0, 0, 10);
            transform.position += new Vector3(0, 0,- Camera.main.transform.position.z);

            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)//大于maxDis进行一个位置限制
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;//单位化向量
                pos *= maxDis;//最大长度的向量
                transform.position = pos + rightPos.position;

            }
            Line();
        }

        //相机跟随
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position , new Vector3(Mathf.Clamp(posX, -5f, 15),Camera.main.transform.position.y,Camera.main.transform.position.z),smooth*Time.deltaTime);//Mathf.Clamp(posX,0,15)让posX限定在0~15;


        if (isFly)
        {
            if (Input.GetMouseButton(0))//0表示鼠标左键
            {
                ShowSkill();
            }
        }
    }


    private void Fly()
    {
        isReleased = true;
        isFly = true;
        AudioPlay(flyAudio);
        canMove = false;
        trails.TrailStart();
        springJoint.enabled = false;
        Invoke("Next", 5.0f);
    }

    //画线,弹弓
    void Line()
    {
        right.enabled = true;
        left.enabled = true;

        right.SetPosition(0, rightPos.position);//画第一个点，第二个参数为位置
        right.SetPosition(1, transform.position);//画第二个点

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }


   public virtual void Next()//下一只小鸟飞出
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;//碰撞后也不能使用技能
        trails.ClearTrail();
        
    }


    //播放音乐
    public virtual void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }


    //显示技能
    public virtual void ShowSkill()
    {
        isFly = false;//控制技能只能用一次
    }


    //小鸟受伤
    public void Hurt()
    {
        render.sprite = hurt;
    }
}

