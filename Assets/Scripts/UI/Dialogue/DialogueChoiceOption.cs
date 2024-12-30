using TMPro;
using UnityEngine;

public class DialogueChoiceOption : MonoBehaviour
{
    public TMP_Text IdTextBox;
    public TMP_Text OptionTextBox;
    public int Id;
    public string OptionText;
    public int NextDialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IdTextBox.text = Id.ToString() + ")";
        OptionTextBox.text = OptionText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log($"Option {Id} selected");
    }
}
