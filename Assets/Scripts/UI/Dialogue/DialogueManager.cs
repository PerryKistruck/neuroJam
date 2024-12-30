using System;
using System.Collections;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject ChoicePanel; // Panel to display choices
    public TMP_Text TextBox; // Text box to display dialogue
    public Dialogue[] Dialogues; // Current set of dialogues;
    public Profile PlayerProfile; // Player's profile
    public string PlayerName = "Eliv"; // Player's name
    public Profile OtherProfile; // Other profile
    public float textSpeed = 0.03f; // Speed of text display
    private int currentDialogueIndex = 1;
    private int ChoiceDialogue = 0;

    private void Start()
    {
        ChoicePanel.SetActive(false);
        string jsonPath = Application.dataPath + "/Scripts/UI/Dialogue/Scenes.json";
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
        while (currentDialogueIndex > 0)
        {
            var currentDialogue = Dialogues.FirstOrDefault(x => x.Id == currentDialogueIndex);
            StartCoroutine(DisplayDialogue(currentDialogue));           
            if (currentDialogue.Choices.Count > 0)
            {
                yield return new WaitUntil(() => ChoiceDialogue > 0);
                currentDialogueIndex = ChoiceDialogue;
                ChoiceDialogue = 0;
            }
            else 
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));
                currentDialogueIndex = currentDialogue.NextDialogue;
            }
            
        }
        EndDialogue();
    }

    private IEnumerator DisplayDialogue(Dialogue dialogue)
    {
        UpdateProfiles(dialogue.SpeakerEmotion, dialogue.ListenerEmotion, dialogue.Speaker, dialogue.Listener);

        TextBox.text = "";
        foreach (char letter in Dialogues[currentDialogueIndex - 1].Text.ToCharArray())
        { 
            TextBox.text += letter; 
            yield return new WaitForSeconds(textSpeed); // Adjust the speed of text display 
        } 
        // Display dialogue text, speaker, and emotions here
        Debug.Log($"Speaker: {dialogue.Speaker}, Text: {dialogue.Text}");

        if (dialogue.Choices.Count > 0)
        {
            ChoiceDialogue = 0;
            ChoicePanel.GetComponent<DialogueChoice>().options = dialogue.Choices;
            ChoicePanel.SetActive(true);
            GameObject[] choiceButtons = GameObject.FindGameObjectsWithTag("ChoiceOption");
            
            // Set up button listeners
            for (int i = 0; i < choiceButtons.Length; i++)
            {
                Button button = choiceButtons[i].GetComponent<Button>();
                if (button != null && button.interactable)
                {
                    int choiceIndex = i;
                    button.onClick.AddListener(() => {
                        ChoiceDialogue = button.GetComponent<DialogueChoiceOption>().NextDialogue;
                        ChoicePanel.SetActive(false);
                    });
                }
            }

            // Wait for either button click or number key press
            yield return new WaitUntil(() => {
                // Check for numerical keyboard input
                for (int i = 0; i < choiceButtons.Length; i++)
                {
                    if (Input.GetKeyDown(KeyCode.Alpha1 + i) || Input.GetKeyDown(KeyCode.Keypad1 + i))
                    {
                        ChoiceDialogue = choiceButtons[i].GetComponent<DialogueChoiceOption>().NextDialogue;
                        ChoicePanel.SetActive(false);
                        return true;
                    }
                }
                return ChoiceDialogue > 0;
            });
        }
    }

    private void UpdateProfiles(string speakerEmotion, string responseEmotion, string speaker, string response)
    {
        string playerProfileName = PlayerName;
        bool isSpeakerPlayer = speaker == "Player";
        
        PlayerProfile.Speaking = isSpeakerPlayer;
        OtherProfile.Speaking = !isSpeakerPlayer;

        PlayerProfile.Emotion = Enum.Parse<Emotion>(isSpeakerPlayer ? speakerEmotion : responseEmotion);
        OtherProfile.Emotion = Enum.Parse<Emotion>(isSpeakerPlayer ? responseEmotion : speakerEmotion);

        PlayerProfile.IsPlayer = PlayerName != "Neuro";
        OtherProfile.IsPlayer = false;

        PlayerProfile.CharacterName = playerProfileName;
        OtherProfile.CharacterName = isSpeakerPlayer? OtherProfile.CharacterName : speaker;
        
        PlayerProfile.UpdateProfile();
        OtherProfile.UpdateProfile();
        
        Debug.Log($"Speaker's emotion: {speakerEmotion}");
    }

    private void EndDialogue()
    {
        // Handle end of dialogue
        Debug.Log("Dialogue ended.");
    }
}