using UnityEngine;
using System.Collections;

public class charStatWindow : MonoBehaviour {
    public GameObject heroObject;
    const string  c_liveCountName = "Жизнь";
    const string c_initiativeCountName = "Инициатива";
    // Use this for initialization
    void Start() {
       
	}
	
	// Update is called once per frame
	void Update () {
        if (heroObject == null)
        {
            return;
        }
        if (heroObject.GetComponent<liveObjectStats>() != null)
        {
            UnityEngine.UI.Text[] tx;
            tx = gameObject.GetComponentsInChildren<UnityEngine.UI.Text>(); //)).text = string.Format("{0} : {1}", c_liveCountName, heroObject.GetComponent<liveObjectStats>().liveCount);
            foreach(UnityEngine.UI.Text t in tx)
            {
                if (t.name == "Text")
                {
                    t.text = string.Format("{0} : {1}\n{2} : {3}", c_liveCountName, heroObject.GetComponent<liveObjectStats>().liveCount, c_initiativeCountName, heroObject.GetComponent<liveObjectStats>().turnInitiative) ;
                }
            }
        }
        if (heroObject.GetComponent<ChapaevControlScript>() != null)
        {
            Sprite imgSpriteHorse;
            Sprite imgSpriteShashka;
            string horseSpriteName = "horse_no";
            string shashkaSpriteName = "shashka_no"; ;
            if (heroObject.GetComponent<ChapaevControlScript>().isHorsed)
            {
                horseSpriteName = "horse_yes";

            }
            if (heroObject.GetComponent<ChapaevControlScript>().haveShashka)
            {
                shashkaSpriteName = "shashka_yes";
            }

            imgSpriteHorse = Resources.Load(horseSpriteName, typeof(Sprite)) as Sprite;
            imgSpriteShashka = Resources.Load(shashkaSpriteName, typeof(Sprite)) as Sprite;
            UnityEngine.UI.Image[] im;
            im = gameObject.GetComponentsInChildren<UnityEngine.UI.Image>();
            foreach(UnityEngine.UI.Image img in im)
            {
                if (img.name == "Image1")
                {
                    img.sprite = imgSpriteHorse;
                }
                if (img.name == "Image2")
                {
                    img.sprite = imgSpriteShashka;
                }

            }
        }

    }
}
