using UnityEngine;
using System.Collections;

public class moveTo : MonoBehaviour {
    int _x;
    int _y;
    battleActorBehavior bAH;
    public int x { get { return _x; } }
    public int y { get { return _y; } }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setCoords(int xCoord, int yCoord, battleActorBehavior _bAH)
    {
        _x = xCoord;
        _y = yCoord;
        bAH = _bAH;
    }

    void OnMouseUpAsButton()
    {
        bAH.moveToCoords(x, y, true);
    }

    public void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load("movement_allow_hex_selected", typeof(Sprite)) as Sprite;
    }

    public void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load("movement_allow_hex", typeof(Sprite)) as Sprite;
    }

}
