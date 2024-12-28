using System.Collections;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Dialogue[] Dialogues; // Current set of dialogues;
    public TMP_Text TextBox; // Text box to display dialogue
    public float textSpeed = 0.03f; // Speed of text display
    private int currentDialogueIndex = 0;

    private void Start()
    {
        string jsonPath = Application.dataPath + "/Scripts/UI/Scenes.json";
        if (File.Exists(jsonPath))
        {
            string jsonContent = File.ReadAllText(jsonPath);
            var scenes = JsonUtility.FromJson<SceneWrapper>(jsonContent);
            Dialogues = scenes.Scenes[0].DialogueList.ToArray();
        }
        else
        {
            Debug.LogError("dialogue.json not found at: " + jsonPath);
        }

        StartCoroutine(StartDialogue());
    }

    public IEnumerator StartDialogue()
    {
        while (currentDialogueIndex < Dialogues.Length)
        {
            DisplayDialogue(Dialogues[currentDialogueIndex]);
            
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));
            currentDialogueIndex = Dialogues[currentDialogueIndex].NextDialogue;
        }
        EndDialogue();
    }

    private IEnumerator DisplayDialogue(Dialogue dialogue)
    {
        UpdateSpeakerEmotion(dialogue.SpeakerEmotion);
        if (dialogue.Choices is not null)
        {
            // Display choices and wait for input
            for (int i = 0; i < dialogue.Choices.Count; i++)
            {
                Debug.Log($"Choice {i + 1}: {dialogue.Choices[i]}");
            }
            
            // Wait for numeric input (1 to number of choices)
            while (true)
            {
                for (int i = 0; i < dialogue.Choices.Count; i++)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                    {
                        dialogue.NextDialogue = i;
                        break;
                    }
                }
                yield return null;
            }
        }

        TextBox.text = "";
        foreach (char letter in Dialogues[currentDialogueIndex].Text.ToCharArray())
        { 
            TextBox.text += letter; 
            yield return new WaitForSeconds(textSpeed); // Adjust the speed of text display 
        } 
        // Display dialogue text, speaker, and emotions here
        Debug.Log($"Speaker: {dialogue.Speaker}, Text: {dialogue.Text}");
    }

    private void UpdateSpeakerEmotion(string speakerEmotion)
    {
        // Update speaker's emotion
        Debug.Log($"Speaker's emotion: {speakerEmotion}");
    }

    private void EndDialogue()
    {
        // Handle end of dialogue
        Debug.Log("Dialogue ended.");
    }
}