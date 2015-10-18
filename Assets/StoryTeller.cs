using UnityEngine;
using System.Collections;

public class StoryTeller : MonoBehaviour {

    const string S_NEXT_BTN = "Далее";
    const string S_TEXT_PLANE = "Storytext";
    const string S_TEXT_RECT = "Story";
    const string S_ANSWER_BTN_PREFAB = "Prefabs/ui_btn_answer";
    const string S_STORYTELLERUI_PREFAB = "Prefabs/StoryTellPanel";

//    GameObject StoryTellerUI;
    GameObject textPanelObj;
    UnityEngine.UI.Text storyTextObj;
    GameObject textRectObj;

    UnityEngine.UI.Button btn;
    UnityEngine.UI.ScrollRect sr;
    

    public static GameObject GetStoryTeller(GameObject owner)
    {
        var go =  Resources.Load<GameObject>("Prefabs/StoryTellPanel");
        var storyTellerUi = Instantiate(go);
        if((owner!=null)&&(owner.transform!=null))
        {
            storyTellerUi.transform.SetParent(owner.transform, false);
        }
        return storyTellerUi;
    }

    void Awake()
    {
        btn = Resources.Load<UnityEngine.UI.Button>(S_ANSWER_BTN_PREFAB);
        textRectObj = GameObject.Find(S_TEXT_RECT);
        textPanelObj = GameObject.Find(S_TEXT_PLANE);
        if (textPanelObj != null)
        {
            storyTextObj = textPanelObj.GetComponent<UnityEngine.UI.Text>();
        }
        sr = GetComponentInChildren<UnityEngine.UI.ScrollRect>();
    }

    public void SetEndBtn()
    {
        var bt = gameObject.GetComponentsInChildren<UnityEngine.UI.Button>();
        foreach (var b in bt)
        {
            b.transform.localPosition = new Vector3(100000, 100000, 100000);
            
        }
        var ui_btn = Instantiate(btn);
        ui_btn.transform.SetParent(transform, false);
        var v3 = ui_btn.transform.localPosition;
        var wd= ((RectTransform)textRectObj.transform);

        v3.x = ((RectTransform)textRectObj.transform).rect.xMin + 0.5f*(wd.rect.width );
        v3.y = ((RectTransform)textRectObj.transform).rect.yMin;

        ui_btn.transform.localPosition = v3;
        ((RectTransform)ui_btn.transform).sizeDelta =new Vector2(wd.rect.width/1.2f, ((RectTransform)ui_btn.transform).rect.height);

        ui_btn.GetComponentInChildren<UnityEngine.UI.Text>().text = S_NEXT_BTN;
        ui_btn.onClick.AddListener(delegate { BtnClicked(null); });


    }

    public void BtnClicked(Answers ans)
    {
        if (ans == null)
        {
            HideStoryTeller();
        }

        if (storyTextObj != null)
        {
            storyTextObj.text = storyTextObj.text + "\n" + ans.text;
            GoStoryBottom();
            if ((ans.nextEventName == null) || (ans.nextEventName == ""))
            {
                SetEndBtn();
            }
        }
    }

    public void GoStoryBottom()
    {
       // var sr = GetComponentInChildren<UnityEngine.UI.ScrollRect>();
        if (sr != null)
        {
            sr.verticalNormalizedPosition=0f;
            Canvas.ForceUpdateCanvases();
        }
        
    }

    void HideStoryTeller()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        globalVars.showGUI = true;
        Canvas.ForceUpdateCanvases();
        return;
    }

    public void TellTheStory (string evName)
    {
        globalVars.showGUI = false;
        Canvas.ForceUpdateCanvases();

        if (evName == null)
        {
            return;
        }
        if (!StoryEvents.Story.namedEvents.ContainsKey(evName))
        {
            return;
        }

        Time.timeScale = 0;
        //string evName = "main_act1_scene_1";

        if (storyTextObj != null)
        {
            storyTextObj.text = StoryEvents.Story[evName].text;
        }
        if (StoryEvents.Story[evName].answers.Count > 0)
        {
            
            if (btn != null)
            {
                for (int i = 0; i < StoryEvents.Story[0].answers.Count; i++)
                {
                    var ui_btn = Instantiate(btn);
                    ui_btn.transform.SetParent(transform, false);
                    var v3 = ui_btn.transform.localPosition;
                    v3.x = ((RectTransform)textRectObj.transform).rect.xMin + (i % 5) * (((RectTransform)ui_btn.transform).rect.width + 3) + 5f + (0.5f) * (((RectTransform)ui_btn.transform).rect.width);
                    v3.y = ((RectTransform)textRectObj.transform).rect.yMin - (i / 5) * (((RectTransform)ui_btn.transform).rect.height + 3);
                    ui_btn.transform.localPosition = v3;

                    ui_btn.GetComponentInChildren<UnityEngine.UI.Text>().text = StoryEvents.Story[evName].answers[i].description;
                    var ans = StoryEvents.Story[evName].answers[i];
                    ui_btn.onClick.AddListener(delegate { BtnClicked(ans); });

                }
            }
        }

        if (sr != null)
        {
            Canvas.ForceUpdateCanvases();
            sr.verticalNormalizedPosition = 1f;
            Canvas.ForceUpdateCanvases();
        }


    }
}
