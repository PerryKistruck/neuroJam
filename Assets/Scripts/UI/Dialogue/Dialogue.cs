using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int Id;
    public string Speaker;
    public string Listener;
    public string Text;
    public List<Choice> Choices;
    public string SpeakerEmotion;
    public string ListenerEmotion;
    public int NextDialogue;

    public Dialogue(string id, string speaker, string listener, string text, string speakerEmotion, string listenerEmotion, string nextDialogue, List<Choice> choices)
    {
        this.Id = int.Parse(id);
        this.Speaker = speaker;
        this.Listener = listener;
        this.Text = text;
        this.SpeakerEmotion = speakerEmotion;
        this.ListenerEmotion = listenerEmotion;
        this.NextDialogue = int.Parse(nextDialogue);
        this.Choices = choices;
    }
}

[System.Serializable]
public class Scene
{
    public int Id;
    public List<Dialogue> DialogueList;
}

[System.Serializable]
public class SceneWrapper
{
    public List<Scene> Scenes;
}

[System.Serializable]
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
}

public enum Emotion
{
    Neutral,
    Happy,
    Sad,
    Angry,
    Surprised,
    Disgusted,
    Scared
}