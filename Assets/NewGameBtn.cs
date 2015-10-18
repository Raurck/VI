using UnityEngine;
using System.Collections;

public class NewGameBtn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        GetComponent<Animator>().SetInteger("State", 1);
    }

    void OnMouseExit()
    {
        GetComponent<Animator>().SetInteger("State", 0);
    }
}
