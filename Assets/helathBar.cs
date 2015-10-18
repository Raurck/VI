using UnityEngine;
using System.Collections;

public class helathBar : MonoBehaviour {

    // Use this for initialization
    void Start () {

        //GetComponent<Transform>().position.bounds
        /*
        if (GetComponentInParent<ChapaevControlScript>() != null)
        {
            GetComponentInParent<ChapaevControlScript>().poligonChange += hbRepos;
        }
        */
        hbRepos();
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponentInParent<liveObjectStats>() != null)
        {
            Vector3 v = GetComponent<Transform>().localScale;
            v.x =  (float)(GetComponentInParent<liveObjectStats>().liveCount) / GetComponentInParent<liveObjectStats>().liveCountMax;
            GetComponent<Transform>().localScale = v;
        }

    }

    void hbRepos()
    {
        float maxY = 0;
        PolygonCollider2D pc = GetComponentInParent<PolygonCollider2D>();

        foreach (Vector2 v2 in pc.points)
        {
            maxY = System.Math.Max(maxY, v2.y);
        }

        GetComponent<Transform>().localPosition = new Vector3(0, maxY + 0.3f, GetComponent<Transform>().localPosition.z);
    }


}
