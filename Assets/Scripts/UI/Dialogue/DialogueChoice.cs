using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DialogueChoice : MonoBehaviour
{        
    public GameObject optionPrefab;
    public RectTransform panelRectTransform;
    public List<Choice> options;
    public float verticalSpacing = 10f;

    void OnEnable()
    {
        float panelHeight = panelRectTransform.rect.height;
        float panelWidth = panelRectTransform.rect.width;
        float optionHeight = optionPrefab.GetComponent<RectTransform>().rect.height;
        float totalSpacing = (options.Count - 1) * verticalSpacing;
        float totalHeight = (options.Count * optionHeight) + totalSpacing;
        float startY = totalHeight / 2 - optionHeight / 2;

        for (int i = 0; i < options.Count; i++)
        {
            var position = new Vector2(0, startY - i * (optionHeight + verticalSpacing));
            GameObject option = Instantiate(optionPrefab, panelRectTransform);
            option.GetComponent<RectTransform>().anchoredPosition = position;
            option.GetComponent<DialogueChoiceOption>().Id = options[i].Id;
            option.GetComponent<DialogueChoiceOption>().OptionText = options[i].Text;
            option.GetComponent<DialogueChoiceOption>().NextDialogue = options[i].NextDialogue;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
