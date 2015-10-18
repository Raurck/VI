using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class liveObjectStats : MonoBehaviour, IPointerEnterHandler{
    int _liveCount = -1;
    int _liveCountMax =-1;
    int _stepCount = -1;
    int _stepCountMax = -1;
    public int liveCount { get { return _liveCount; } }
    public int liveCountMax { get { return _liveCountMax; } }
    //public int sceneInitiative;
    public int stepCount { get { return _stepCount; } }
    public int stepCountMax { get { return _stepCountMax; } }
    public int damage =0;
    public fraction charFraction = fraction.noFraction;
    public GameObject initiativeElector;
    
    int _turnInitiative = 52;
    bool _isInitiativeBuffed = false;
    public int turnInitiative { get { return _turnInitiative; } }
    public bool isInitiativeBuffed { get { return _isInitiativeBuffed; } }

    public enum fraction
    {
        noFraction,Hearts,Diamonds,Clubs, Spades
    }

    public static liveObjectStats CreateSimple(GameObject owner)
    {
        liveObjectStats stats = owner.GetComponent<liveObjectStats>();
        if ( stats == null)
        {
            stats = owner.AddComponent<liveObjectStats>();
        }
        int __liveCount = UnityEngine.Random.Range(25, 75);
        stats.SetLiveCounters(__liveCount, __liveCount);
        //stats.sceneInitiative = UnityEngine.Random.Range(0, 52);
        return stats;
    }

    public void Assign(liveObjectStats inObj)
    {
        _liveCountMax = inObj.liveCountMax;
        _liveCount = inObj.liveCount;
        _stepCountMax = inObj.stepCountMax;
        _stepCount = inObj.stepCount;
        charFraction = inObj.charFraction;
        _turnInitiative = inObj.turnInitiative;
        _isInitiativeBuffed = inObj.isInitiativeBuffed;
        initiativeElector = inObj.initiativeElector;
    }

    public int GetTurnInitaitve()
    {

        if (initiativeElector == null)
        {
            initiativeElector = GameObject.Find("initiativeDeck");
            /*Object[] list = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject item in list)
            {
                if (item.name== "initiativeDeck")
                {
                    initiativeElector = item;
                    break;
                }
            }*/
            if (initiativeElector == null)
            {
                initiativeElector = new GameObject("initiativeDeck",(typeof(Deck)));
                initiativeElector.tag = "initiativeDeck";
            }
        }

        _turnInitiative = initiativeElector.GetComponent<Deck>().getCard();
        //_turnInitiative =  Random.Range(0, 52);
        _isInitiativeBuffed = false;

        if ((charFraction == fraction.Spades) && (_turnInitiative >= 40) && (_turnInitiative <= 52))
        {
            _isInitiativeBuffed = true;
        }

        if ((charFraction == fraction.Clubs) && (_turnInitiative >= 27) && (_turnInitiative <= 39))
        {
            _isInitiativeBuffed = true;
        }

        if ((charFraction == fraction.Diamonds) && (_turnInitiative >= 14) && (_turnInitiative <= 26))
        {
            _isInitiativeBuffed = true;
        }

        if ((charFraction == fraction.Hearts) && (_turnInitiative >= 1) && (_turnInitiative <= 13))
        {
            _isInitiativeBuffed = true;
        }

        return _turnInitiative;
    }

    // Use this for initialization
    void Start () {
        if (_liveCountMax == -1)
        {
            _liveCountMax = 100;
            _liveCount = 100;
        }
        if (_stepCount == -1)
        {
            _stepCount = 6;
            _stepCountMax = 6;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (damage != 0)
        {
           _liveCount = _liveCount - damage;
           _liveCount = System.Math.Min(_liveCountMax,_liveCount);
           _liveCount = System.Math.Max(0,_liveCount);
           damage = 0;
            if (_liveCount == 0)
            {
                if (gameObject.GetComponent<ChapaevControlScript>() != null)
                {
                    gameObject.GetComponent<ChapaevControlScript>().restart = true;
                    return;
                }
                Destroy(gameObject);
            }
        }
	}

    public void SetLiveCounters(int __liveCount, int __liveCountMax)
    {
        _liveCount = __liveCount;
        _liveCountMax = __liveCountMax;
    }

    public void OnMouseEnter()
    {
        if (FindObjectOfType<infoPanel>() != null)
        {
            FindObjectOfType<infoPanel>().putObjectData(gameObject);
        }
    }


    public void OnMouseExit()
    {
        if (FindObjectOfType<infoPanel>() != null)
        {
            FindObjectOfType<infoPanel>().putObjectData(null);
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        //        int i = 1;
        //i++;
        OnMouseEnter();
        Debug.Log("New!");
        //   EventSystem.current.currentSelectedGameObject.GetComponent<liveObjectStats>().OnMouseEnter();
    }
}
