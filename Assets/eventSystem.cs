using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class eventSystem : MonoBehaviour, IPointerEnterHandler
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //if (EventSystem.current.IsPointerOverGameObject())
        {
            //EventSystem.current.
            //if (EventSystem.current.currentSelectedGameObject.GetComponent<liveObjectStats>()!=null)
            {
                //EventSystem.current.currentSelectedGameObject.GetComponent<liveObjectStats>().OnMouseEnter();
            }

        }
        /*
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                Debug.Log("left-click over a GUI element!");

            else Debug.Log("just a left-click!");
        }
        */
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //        int i = 1;
        //i++;
        Debug.Log("New!");
     //   EventSystem.current.currentSelectedGameObject.GetComponent<liveObjectStats>().OnMouseEnter();
    }
}
