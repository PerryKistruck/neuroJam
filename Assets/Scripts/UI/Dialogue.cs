using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int Id;
    public string Speaker;
    public string Text;
    public List<Choice> Choices;
    public string SpeakerEmotion;
    public string ListenerEmotion;
    public int NextDialogue;

    public Dialogue(string id, string speaker, string text, string speakerEmotion, string listenerEmotion, string nextDialogue)
    {
        this.Id = int.Parse(id);
        this.Speaker = speaker;
        this.Text = text;
        this.SpeakerEmotion = speakerEmotion;
        this.ListenerEmotion = listenerEmotion;
        this.NextDialogue = int.Parse(nextDialogue);
    }

    // Default constructor
    public Dialogue()
    {
        Id = 0;
        Speaker = "";
        Text = "";
        SpeakerEmotion = "";
        ListenerEmotion = "";
        NextDialogue = 0;
    }
}

[System.Serializable]
public class Scene
{
    public int Id;
    public List<Dialogue> Dialogues;
}

public class Choice
{
    public int Id;
    public string Text;
    public int NextDialogue;

    public Choice(string id, string text, string nextDialogue)
    {
        this.Id = int.Parse(id);
        this.Text = text;
        this.NextDialogue = int.Parse(nextDialogue);
    }

    // Default constructor
    public Choice()
    {
        Id = 0;
        Text = "";
        NextDialogue = 0;
    }
}