using UnityEngine;
using System.Collections;

public class attackHero : MonoBehaviour {
    public GameObject slash;
    public System.Timers.Timer timer;
    bool doSwing = false;

    // Use this for initialization
    void Start()
    {
        timer = new System.Timers.Timer();
        timer.Interval = 600;
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }

    private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        doSwing = true;

    }
    // Update is called once per frame
    void Update () {
        if (doSwing)
        {
            Vector3 sl = GetComponent<Transform>().position;
            doSwing = false;
            (Instantiate(slash, sl, Quaternion.identity) as GameObject).GetComponent<swingScript>().enemyAtttack = true;
        }
    }
}
