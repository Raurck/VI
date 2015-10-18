using UnityEngine;
using System.Collections;

public class eventEnter : MonoBehaviour
{
    public string eventStoryName;
    public string canvasName = "UICanvas";
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
//        Debug.Log("he is here!!!"+ other.name );
        if (other.CompareTag("hero"))
        {
            var ui = GameObject.FindGameObjectWithTag(canvasName);
            if (ui != null)
            {
                StoryTeller.GetStoryTeller(ui).GetComponent<StoryTeller>().TellTheStory(eventStoryName);
            }
            Destroy(gameObject);
        }
    }
}
