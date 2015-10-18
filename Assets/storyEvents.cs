using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Answers
{
    [XmlAttribute]
    public int number;
    [XmlElement("journalevent")]
    public string journalEvent;
    [XmlAttribute]
    public string description;
    [XmlAttribute]
    public string nextEventName;
    [XmlText]
    public string text;
}

public class StoryEvent
{
    [XmlAttribute]
    public int number;
    [XmlAttribute]
    public string description;
    [XmlAttribute]
    public string evname;
    [XmlText]
    public string text;
    [XmlElement("journalevent")]
    public string journalEvent;
    [XmlElement("answer")]
    public List<Answers> answers;
}

[XmlRoot("story")]
public class StoryEvents
{
    [XmlIgnore]
    private static string _storyPath= "C:\\Users\\Barlog\\Documents\\unity\\VI\\Assets\\TheStory-Ru.xml";
    [XmlIgnore]
    private Dictionary<string, int> _namedEvents;
    [XmlIgnore]
    public Dictionary<string, int> namedEvents
    {
        get { return _namedEvents; }
    }
    [XmlIgnore]
    public StoryEvent this[string evName]
    {
        get
        {
            return this[namedEvents[evName]];
        }
    }

    [XmlIgnore]
    public StoryEvent this[int evNum]
    {
        get
        {
            return storyEvents[evNum];
        }
    }

    [XmlIgnore]
    private static StoryEvents _story;
    [XmlIgnore]
    public static StoryEvents Story
    {
        get {
            if (_story == null)
            {
                var x = new XmlSerializer(typeof(StoryEvents));
                _story = (x.Deserialize(new System.IO.StreamReader(_storyPath)) as StoryEvents);
                if (_story != null)
                {
                    _story._namedEvents = new Dictionary<string, int>();
                    for (int i=0;i<_story.storyEvents.Count; i++)
                    {
                        _story._namedEvents.Add(_story.storyEvents[i].evname, i);
                    }

                }
            }
            return _story;
        }
    }
    
    [XmlElement("event")]
    public List<StoryEvent> storyEvents;

}
