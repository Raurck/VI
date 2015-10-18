using UnityEngine;
using System.Collections;

public class infoPanel : MonoBehaviour {
    public GameObject lsPanel;
    public GameObject selectedObject;
	bool isSelectedData = false;
    System.Collections.Generic.List<Object> ol = new System.Collections.Generic.List<Object>();

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (!isSelectedData) {
			showObjectInfo(selectedObject);
			return;
		}
	}

	void showObjectInfo(GameObject sender)
	{
		foreach (Object o in ol)
		{
			Destroy(o);
		}

		if (sender == null) {
			return;	
		}

		Object go =  Instantiate(lsPanel, new Vector3(0, 0, 0), Quaternion.identity);
		(go as GameObject).transform.SetParent(gameObject.transform);
		(go as GameObject).transform.localPosition = new Vector3(0, 0, 0);
		if((go as GameObject).GetComponentsInChildren<UnityEngine.UI.Text>()!=null){
			UnityEngine.UI.Text[] t = (go as GameObject).GetComponentsInChildren<UnityEngine.UI.Text>();
			//Debug.Log(t.Length);
			foreach(UnityEngine.UI.Text obj in t)
			{
				if(obj.name=="Text")
				{ 
					obj.text = string.Format("Жизнь : {0}\nИнициатива : {1}", sender.GetComponent<liveObjectStats>().liveCount, Deck.CardName(sender.GetComponent<liveObjectStats>().turnInitiative));
				}
			}
		}
		ol.Add(go);
	}

    public void putObjectData(GameObject sender)
    {
        if (sender == null)
        {
            foreach (Object o in ol)
            {
                Destroy(o);
            }
			isSelectedData = false;
            return;
        }
        if (sender.GetComponent<liveObjectStats>() != null)
        {
			isSelectedData = true;

			showObjectInfo(sender);
		/*	Object go =  Instantiate(lsPanel, new Vector3(0, 0, 0), Quaternion.identity);
            (go as GameObject).transform.SetParent(gameObject.transform);
            (go as GameObject).transform.localPosition = new Vector3(0, 0, 0);
			if((go as GameObject).GetComponentsInChildren<UnityEngine.UI.Text>()!=null){
				UnityEngine.UI.Text[] t = (go as GameObject).GetComponentsInChildren<UnityEngine.UI.Text>();
				//Debug.Log(t.Length);
				foreach(UnityEngine.UI.Text obj in t)
				{
					if(obj.name=="Text")
					{ 
						obj.text = string.Format("Жизнь: {0}", sender.GetComponent<liveObjectStats>().liveCount);
					}
				}
			}
            ol.Add(go);*/

            return;
        }



    }
}
