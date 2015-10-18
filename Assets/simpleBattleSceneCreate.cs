using UnityEngine;
using System.Collections;
using System;

public class simpleBattleSceneCreate : MonoBehaviour {
    public GameObject terrainMain;
    public GameObject participatorMain;
    public bool isHexGround = true;
    public int groundSizeX = 40;
    public int groundSizeY = 40;
    public float multiY = (float)System.Math.Sqrt(3) / 2;//steps = 2 * steps;
    public float multiX = 0.75f;// (float)System.Math.Sqrt(2) / 2;//steps = 2 * steps;
    public float transitionEvenY = 0.5f;
    public float actorsPlane = 5f;
    public float terrainPlane = 10f;

    private System.Collections.Generic.List<UnityEngine.Object> groundPlates = new System.Collections.Generic.List<UnityEngine.Object>();

    private System.Collections.Generic.List<UnityEngine.Object> battelActors = new System.Collections.Generic.List<UnityEngine.Object>();

    public System.Collections.Generic.List<UnityEngine.GameObject> battelSequence = new System.Collections.Generic.List<UnityEngine.GameObject>();
    public delegate void TurnSequenceApdated(object sender, EventArgs e);

    public event TurnSequenceApdated OnTurnSequenceUpdated;
    // Use this for initialization
    void Start() {
        DrawGround();

        for (int i = 1; i <= 25; i++)
        {
            AddBattleParticipator(null, UnityEngine.Random.Range(0, 10), i);
        }

        BuildTurnSequence();
    }


    private void BuildTurnSequence()
    {
        
        battelSequence.Clear();
        foreach (GameObject item in battelActors)
        {
            bool isInserted = false;

            item.GetComponent<liveObjectStats>().GetTurnInitaitve();
            /*
            if (battelSequence.Count == 0)
            {
                battelSequence.Add(item);
                continue;
            }
            */
            for (int i = 0; i < battelSequence.Count ; i++)
            {
                if(Deck.CompareCards( item.GetComponent<liveObjectStats>().turnInitiative, battelSequence[i].GetComponent<liveObjectStats>().turnInitiative))
                {
                    isInserted = true;
                    battelSequence.Insert(i, item);
                    break;
                }
            }

            if (!isInserted)
            {
                battelSequence.Add(item);
            }
        }

        RaiseTurnSequenceUpdated();
    }

    protected virtual void RaiseTurnSequenceUpdated()
    {
        if (OnTurnSequenceUpdated != null)
        {
            OnTurnSequenceUpdated(this, new EventArgs());
        }
    }

    public void AddBattleParticipator(liveObjectStats _stats, int _posX, int _posY )
    {
        Vector3 v = new Vector3(0,0, actorsPlane);
        UnityEngine.Object ba = Instantiate(participatorMain, v, Quaternion.identity);
        (ba as GameObject).GetComponent<battleActorBehavior>().moveToCoords(_posX, _posY);

       //DestroyImmediate((ba as GameObject).GetComponent<liveObjectStats>());
        
        if (_stats == null)
        {
            _stats = liveObjectStats.CreateSimple((ba as GameObject));
        }

        //(ba as GameObject).GetComponent<liveObjectStats>() = _stats;
        //(ba as GameObject).AddComponent<liveObjectStats>;

        battelActors.Add(ba);
        return;       
            
    }


    private void DrawGround()
    {
        if (terrainMain == null)
        {
            return;
        }
        if (isHexGround)
        {
            int xSize = groundSizeX / 2;
            int ySize = groundSizeY / 2;
            for (int i = -xSize; i <= xSize; i++)
            {
                for (int j = -ySize; j <= ySize; j++)
                {
                    //Vector3 v = new Vector3(i % 2 == 0 ? i  : (i + 0.5f) * 2f, j % 2 == 0 ? j : (j + 0.5f) * 2, 0f);

                    Vector3 v = new Vector3(i * multiX, (i % 2 == 0 ? j : (j + transitionEvenY)) * multiY, terrainPlane);
                    UnityEngine.Object obj = Instantiate(terrainMain, v, Quaternion.identity);
                    (obj as GameObject).transform.SetParent(gameObject.transform, false);
                    groundPlates.Add(obj);
                }

            }

            return;
        }

        for (int i = 0; i <= 10; i++)
        {
            for (int j = 0; j <= 10; j++)
            {
                Vector3 v = new Vector3(i, j, 10);
                UnityEngine.Object obj = Instantiate(terrainMain,v , Quaternion.identity);
                (obj as GameObject).transform.SetParent(gameObject.transform, false);
                groundPlates.Add(obj);
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
