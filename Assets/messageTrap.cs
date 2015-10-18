using UnityEngine;
using System.Collections;

public class messageTrap : MonoBehaviour {
    public GameObject charStat;
    //System.Collections.Generic.List<Object> statPanels = new System.Collections.Generic.List<Object>();
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void createStatWindow(GameObject senderObject )
    {
        Vector3 sl = new Vector3(0.5f/* + statPanels.Length * (charStat.GetComponent<RectTransform>().rect.width + 0.5f)*/,0f,0f);
        Object go = Instantiate(charStat, sl, Quaternion.identity);
        (go as GameObject).GetComponent<charStatWindow>().heroObject = senderObject;
        //(go as GameObject).transform.SetParent( gameObject.transform);
        (go as GameObject).GetComponent<RectTransform>().SetParent(gameObject.transform);
        //(go as GameObject).GetComponent<RectTransform>(). .rect.xMin = 0;
        //(go as GameObject).GetComponent<RectTransform>().rect.yMin = 0;
        sl.y=(go as GameObject).GetComponent<RectTransform>().rect.max.y+0.2f;
        sl.x=(go as GameObject).GetComponent<RectTransform>().rect.max.x + 0.2f;
        (go as GameObject).GetComponent<RectTransform>().position = sl;
        //(go as GameObject).transform.position = sl;
        //System.Type cT = charStat.GetType();
        //gameObject. .AddComponent<charStat>;

        //UnityEditor.PrefabUtility.GetPrefabObject()
        //UnityEditor.PrefabUtility.InstantiatePrefab();
    }
}
