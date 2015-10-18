using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class battleActorBehavior : MonoBehaviour
{
    public GameObject allowedMovementGround;
    public bool isHexFields;
    private Animator animator;
    private System.Collections.Generic.List<UnityEngine.Object> movementPlates = new System.Collections.Generic.List<UnityEngine.Object>();
    bool _active;
    int myX=0;
    int myY=0;
	public bool active { 
		get {return _active;}
		set {
			_active = value;
			if(animator!=null)
			{
				if(value)
				{
   				    animator.SetInteger ("state", 2);
                    showAllowedMovement();
                }
				else{
					animator.SetInteger ("state", 0);
                    hideAllowedMovement();
                }
			}
	   	}
	}



    // Use this for initialization
    void Start () {
		animator = GetComponent<Animator> ();
		//GetComponent<liveObjectStats>().setLiveCounters (UnityEngine.Random.Range(1,100),100);

       // moveToCoords(3, 2);
    }

    public void moveToCoords(int x, int y, bool intCoords=false)
    {
        float multiX = FindObjectOfType<simpleBattleSceneCreate>().multiX;
        float multiY = FindObjectOfType<simpleBattleSceneCreate>().multiY;

        if (intCoords)
        {
            x = x + myX;
            y = y + myY;
        }

        myY = y;
        myX = x;

        gameObject.transform.SetParent(null, false);
        gameObject.transform.position = new Vector3(x* multiX, (x % 2 == 0 ? y : (y + 0.5f)) * multiY, 0);
        gameObject.transform.SetParent(FindObjectOfType<simpleBattleSceneCreate>().transform, false);
    }
	// Update is called once per frame
	void Update () {
		if (active) {
			if (Input.GetKey (KeyCode.Escape)) {
				active = false;
				FindObjectOfType<infoPanel>().selectedObject=null;
			}
		}
	}

	public void otherObjectSelected()
	{
		animator.SetInteger ("state", 0);
	}
    public void  OnMouseEnter()
    {
        //FindObjectOfType<infoPanel>().putObjectData(gameObject);
		if (animator.GetInteger ("state") != 2) {
			animator.SetInteger ("state", 1);
		}
        ///showStat = true;
    }

    public void OnMouseExit()
    {
        //FindObjectOfType<infoPanel>().putObjectData(null);
		if (animator.GetInteger ("state") != 2) {
			animator.SetInteger ("state", 0);
		}
        //showStat = true;
    }


    void OnMouseUpAsButton()
    {
        FindObjectOfType<infoPanel>().selectedObject=gameObject;

		foreach (battleActorBehavior bAB in FindObjectsOfType<battleActorBehavior> ()) {
			bAB.active = false;
		};
		active = true;
        
    }

    private void showAllowedMovement()
    {
        if((gameObject.GetComponent<liveObjectStats>() == null)||(allowedMovementGround==null))
        {
            return;
        }
        int steps = gameObject.GetComponent<liveObjectStats>().stepCount;
        if (isHexFields)
        {

            float multiY = (float) System.Math.Sqrt(3) / 2;//steps = 2 * steps;
            float multiX = 0.75f;// (float)System.Math.Sqrt(2) / 2;//steps = 2 * steps;
            for (int i = -steps; i <= steps; i++)
            {
                for (int j = -steps; j <= steps; j++)
                {
                    //Vector3 v = new Vector3(i % 2 == 0 ? i  : (i + 0.5f) * 2f, j % 2 == 0 ? j : (j + 0.5f) * 2, 0f);
                    if (( (j<0? (Math.Abs(i) / 2): (Math.Abs(i) - Math.Abs(i) / 2) )+ Math.Abs(j) <= steps))
                    {
                        Vector3 v = new Vector3(i * multiX, (i % 2 == 0 ? j : (j + 0.5f)) * multiY, 0f);
                        UnityEngine.Object obj = Instantiate(allowedMovementGround, v, Quaternion.identity);
                        (obj as GameObject).transform.SetParent(gameObject.transform, false);
                        if ((obj as GameObject).GetComponent<moveTo>() != null)
                        {
                            (obj as GameObject).GetComponent<moveTo>().setCoords(i, j, this);
                        }
                        movementPlates.Add(obj);
                    }
                }

            }
            
            return;
        }
        
        for(int i=-steps;i<=steps;i++)
        {
            for (int j = -steps; j <= steps; j++)
            {
                if ((Math.Abs(i) + Math.Abs(j) <= steps))
                {
                    Vector3 v = new Vector3(i, j, 0f);
                    UnityEngine.Object obj = Instantiate(allowedMovementGround, v, Quaternion.identity);
                    (obj as GameObject).transform.SetParent(gameObject.transform, false);
                    movementPlates.Add(obj);
                }
            }

        }
    }

    private void hideAllowedMovement()
    {
        foreach (UnityEngine.Object obj in movementPlates)
        {
            Destroy(obj);
        }
    }

}
