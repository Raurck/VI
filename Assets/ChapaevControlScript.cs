using System;
using System.Collections;
using UnityEngine;

public class ChapaevControlScript : MonoBehaviour
{
    static GameObject isMainChar = null;
    static Vector3 startPoint;

    private const float c_maxHorseSpeed  = 20f;
    private const float c_maxFootSpeed   = 10f;
    private const float c_jumpForceHorse = 800f;
    private const float c_jumpForceFoot  = 400f;
    private const float c_swingHorse = 2.5f;
    private const float c_swingFoot  = 1f;

    public GameObject slash;
    private float move;
    public float maxSpeed = c_maxFootSpeed;
    public float jumpForce = c_jumpForceFoot;
    public float swingDist = c_swingFoot;
    private bool faceingRight = false;

    public Transform groundCheck;
    public float groundRadius = 2f;
    public LayerMask whatIsGround;
    public bool isGrounded;
    public bool canExit = false;
    public bool isHorsed = false;
    public bool haveShashka = false;
    public bool restart = false;

    //public delegate void PoligonChange();
    //public PoligonChange poligonChange;

    



    //public bool mySprite;

    // Use this for initialization
    void Start()
    {
        if (isMainChar != null)
        {

            startPoint = GetComponent<Transform>().position;
            isMainChar.GetComponent<Transform>().position = startPoint;
            isMainChar.GetComponent<ChapaevControlScript>().requestForStatWindow();
            FindObjectOfType<newcam>().target= (isMainChar as GameObject).transform;
            Destroy(gameObject);

            return;
        }

        isMainChar = gameObject;
        Flip();
        setGroundCheckPosition();
        startPoint = GetComponent<Transform>().position;
        requestForStatWindow();
        DontDestroyOnLoad(gameObject);
    }

    void FixedUpdate()
    {
        move = Input.GetAxis("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    }

    void Flip()
    {
        faceingRight = !faceingRight;
        Vector3 scale = GetComponent<Transform>().localScale;
        scale.x = -1 * scale.x;
        GetComponent<Transform>().localScale = scale;
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if ((faceingRight && move < 0) || (!faceingRight && move > 0))
        {
            Flip();
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }

        if ((Input.GetKeyDown(KeyCode.R)) && canExit)
        {
            /*
            for (int i=1; i<=1000;i++)
            {
                Vector3 scale = GetComponent<Transform>().localScale;
                scale.x = scale.x / i;
                scale.y = scale.y / i;
                GetComponent<Transform>().localScale = scale;
                UnityEditor.SceneView.RepaintAll();
            }
            */
            canExit = false;
            Application.LoadLevel(Application.loadedLevel);
        }
        if (Input.GetKeyDown(KeyCode.Space) && haveShashka)
        {

            Vector3 sl = GetComponent<Transform>().position;
            if (faceingRight)
            {
                sl.x += swingDist;
            }
            else
            {
                sl.x -= swingDist;
            }
            Instantiate(slash, sl,Quaternion.identity);
            /*
            sl= slash.GetComponent<Transform>().localScale;
            sl.x = -1;
            slash.GetComponent<Transform>().localScale = sl;
            */
        }
        if (restart)
        {
            returnToLevelStart();
            restart = false;
        }
    }


    private void requestForStatWindow()
    {
        FindObjectOfType<messageTrap>().createStatWindow(gameObject);
        //SendMessage("createStatWindow", gameObject, SendMessageOptions.DontRequireReceiver);
    }

    private void dismount()
    {
        if (!isHorsed) { return; }

        float turnMultipler = 1;
        isHorsed = false;
        GetComponent<SpriteRenderer>().sprite = Resources.Load("chap_foot", typeof(Sprite)) as Sprite;
        if (faceingRight)
        {
            turnMultipler = -1;
        }

        Vector3 v;

        v = GetComponent<Transform>().localScale;
        v.x = 1f * turnMultipler;
        v.y = 1f;
        GetComponent<Transform>().localScale = v;

        setGroundCheckPosition();

        maxSpeed = c_maxFootSpeed;
        jumpForce = c_jumpForceFoot;
        swingDist = c_swingFoot;

        setHealthBarPosition();
        /*
        if (poligonChange != null)
        {
            poligonChange();
        }*/

    }

    private void setGroundCheckPosition()
    {
        Destroy(GetComponent<PolygonCollider2D>());

        PolygonCollider2D pc = gameObject.AddComponent<PolygonCollider2D>();

        //Vector3 v = groundCheck.GetComponent<Transform>().position;
        //v.y = pc.bounds.min.y;
        

        float minY = 0;

        foreach (Vector2 v2 in pc.points)
        {
            minY = System.Math.Min(minY, v2.y);
        }

        groundCheck.GetComponent<Transform>().localPosition = new Vector3(0, minY, -5);
        //GetComponent<Transform>().localPosition = new Vector3(0, minY , -5);
    }

    void mountOnHorse()
    {

        if (isHorsed) { return; }

        float turnMultipler = 1;
        isHorsed = true;
        GetComponent<SpriteRenderer>().sprite = Resources.Load("chap_mount", typeof(Sprite)) as Sprite;
        if (faceingRight)
        {
            turnMultipler = -1;
        }
                
        Vector3 v;

        v = GetComponent<Transform>().position;
        v.y = v.y + 2f;
        GetComponent<Transform>().position = v;


        v = GetComponent<Transform>().localScale;
        v.x = 2f* turnMultipler;
        v.y = 2f;
        GetComponent<Transform>().localScale = v;

        setGroundCheckPosition();

        maxSpeed = c_maxHorseSpeed;
        jumpForce = c_jumpForceHorse;
        swingDist = c_swingHorse;

        setHealthBarPosition();

        /*
        if (poligonChange != null)
        {
            poligonChange();
        }
        */

    }

    void setHealthBarPosition()
    {
        BroadcastMessage("hpRepos", null, SendMessageOptions.DontRequireReceiver);
    }

    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.gameObject.name == "levelExit")
        {
            canExit = true;
        }
        if (collisionInfo.gameObject.name == "horse")
        {
            mountOnHorse();
        }

        if (collisionInfo.gameObject.name == "shashka")
        {
            getShashka();
        }

        if (collisionInfo.gameObject.name == "dieCollider")
        {
            returnToLevelStart();
        }

        if (collisionInfo.gameObject.tag == "collectible")
        {
            Destroy(collisionInfo.gameObject);
        }


    }

    private void getShashka()
    {
        haveShashka = true;
    }

    private void returnToLevelStart()
    {
        dismount();
        haveShashka = false;
        if (gameObject.GetComponent<liveObjectStats>() != null)
        {
            gameObject.GetComponent<liveObjectStats>().SetLiveCounters(100, 100);
        }
        GetComponent<Transform>().position = startPoint;
    }

    void OnTriggerExit2D(Collider2D collisionInfo)
    {
        if (collisionInfo.gameObject.name == "levelExit")
        {
            //GUI.Box(new Rect(0, 0, 25, 25), "");
            canExit = false;
        }
    }

    void OnGUI()
    {
        if (globalVars.showGUI)
        {
            string s = "W,A,S,D - движение\nSpace - рубить, есть шашка есть\nС - панель персонажа\n";
            if (canExit)
            {
                s = s + "Нажмите R для продолжения";
            }

            GUI.Box(new Rect(0, 0, 250, 75), s);
        }

//        GUI.Box();
    }
}