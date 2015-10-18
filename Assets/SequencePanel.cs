using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SequencePanel : MonoBehaviour {


    float _scaleX=0.1f;
    float _scaleY = 0.1f;
    float _scaleZ = 1f;
    float _imgOfset = 5;
    public GameObject sceneObject;

    string sceneName = "battleSceneCreator";

    

    void Awake () {

        if (sceneObject == null)
            sceneObject = GameObject.Find("battleSceneCreator");

        if (sceneObject != null)
            if (sceneObject.GetComponent<simpleBattleSceneCreate>() != null)
                sceneObject.GetComponent<simpleBattleSceneCreate>().OnTurnSequenceUpdated += OnTurnSequenceUpdate;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTurnSequenceUpdate(object sender, EventArgs e)
    {
        simpleBattleSceneCreate obj = (simpleBattleSceneCreate)sender;
        GameObject facePanel = GameObject.Find("initiativeFacePanel");                                                
        for (int i = 0; i < obj.battelSequence.Count-1; i++)
        {
            Debug.Log(Deck.CardName((obj.battelSequence[i] as GameObject).GetComponent<liveObjectStats>().turnInitiative));
            GameObject gm = new GameObject();
            Image im = gm.AddComponent<Image>();
            im.sprite = (obj.battelSequence[i] as GameObject).GetComponent<SpriteRenderer>().sprite;
            gm.AddComponent<liveObjectStats>().Assign((obj.battelSequence[i] as GameObject).GetComponent<liveObjectStats>());
            gm.transform.SetParent(facePanel.transform);
            _scaleX =  ((RectTransform)facePanel.transform).rect.width/ im.rectTransform.rect.width;
            _scaleY =  ((RectTransform)facePanel.transform).rect.height/ im.rectTransform.rect.height;
            _scaleX = System.Math.Min(_scaleX, _scaleY);
            _scaleY = System.Math.Min(_scaleX, _scaleY);
            gm.transform.localPosition = new Vector3(((RectTransform)facePanel.transform).rect.xMin + _imgOfset + (im.rectTransform.rect.width*_scaleX +_imgOfset)* i, gm.transform.position.y, gm.transform.position.z);
            gm.transform.localScale = new Vector3(_scaleX, _scaleY, _scaleZ);

        }

    }
}
