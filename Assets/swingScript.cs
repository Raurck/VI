using UnityEngine;
using System.Collections;

public class swingScript : MonoBehaviour {
    public System.Timers.Timer timer;
    public bool enemyAtttack = false;
    bool needDestroy = false;
    // Use this for initialization
    void Start () {
        timer = new System.Timers.Timer();
        timer.Enabled = true;
        timer.Interval = 150;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
	}

    private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        needDestroy = true;
        (sender as System.Timers.Timer).Stop();
    }

    // Update is called once per frame
    void Update () {
        if (needDestroy)
        {
            Destroy(gameObject);
        }
	
	}

    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if((enemyAtttack)&& (collisionInfo.gameObject.tag == "hero"))
        {
            if (collisionInfo.gameObject.GetComponent<liveObjectStats>() != null)
            {
                collisionInfo.gameObject.GetComponent<liveObjectStats>().damage = 10;
            }
            else
            {
                Destroy(collisionInfo.gameObject);
            }
            Destroy(gameObject);
        }
        if ((!enemyAtttack) && (collisionInfo.gameObject.tag == "enemy"))
        {
            //collisionInfo.gameObject.
            if (collisionInfo.gameObject.GetComponent<liveObjectStats>() != null)
            {
                collisionInfo.gameObject.GetComponent<liveObjectStats>().damage = 10;
            }
            else
            {
                Destroy(collisionInfo.gameObject);
            }
            Destroy(gameObject);
        }

    }
}
