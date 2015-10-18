using UnityEngine;
using System.Collections;

public class newcam : MonoBehaviour {
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
    private Vector3 moveMax = new Vector3(20,20,0);

    public Transform target;
    bool _isMoving = false;
    Vector3 startPos = Vector3.zero;

    // Update is called once per frame
    void Update () 
	{
        
        
        if (Input.GetMouseButtonDown(1))
        {
                    _isMoving = true;
        }

        if (Input.GetMouseButtonUp(1))
            _isMoving = false;
        
        if(_isMoving)
        {

            float w = Input.GetAxis("Mouse X");
            float h = Input.GetAxis("Mouse Y");
            if (w > 0)
            {
                w = System.Math.Min(w, moveMax.x);
            }
            else
            {
                w = System.Math.Max(w, -moveMax.x);
            }
            if(h>0)
            {
                h = System.Math.Min(h, moveMax.y);
            }
            else
            {
                h = System.Math.Max(h, -moveMax.y);
            }
            Vector3 delta = new Vector3(-w, -h, 0f);
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
           // transform.Translate(new Vector3(w, h,0));// .position = Vector3.SmoothDamp(transform.position, destination*5, ref velocity, dampTime);

        }
        if (target)
		{
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(new Vector3(target.position.x, target.position.y+0.75f,target.position.z));
			Vector3 delta = new Vector3(target.position.x, target.position.y+0.75f,target.position.z) - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;


			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
		
	}

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown((int)KeyCode.Mouse1))
        {
            _isMoving = true;
        }
    }

    void OnMouseUp()
    {
        if (Input.GetMouseButtonUp((int)KeyCode.Mouse1))
        {

            _isMoving = false;
        }
    }
}
