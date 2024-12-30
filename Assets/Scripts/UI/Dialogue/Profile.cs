using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class Profile : MonoBehaviour
{
    public SpriteRenderer ProfileImage;
    public bool IsPlayer;
    public bool Speaking;
    public TMP_Text NameText;
    public string CharacterName;
    public Emotion Emotion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ProfileImage.sprite = Resources.Load<Sprite>("Images/Character/" + gameObject.name + "/" + Emotion.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Updates the profile's visual appearance including name, sprite and speaking state
    /// </summary>
    public void UpdateProfile()
    {
        // Set the character's name in the UI
        NameText.text = CharacterName;
        string character = IsPlayer ? "Player" : CharacterName;
        // Construct path to sprite resource - uses Player folder for player, otherwise character name
        string path = $"Images/Character/{character}/{Emotion.ToString()}";
        // Load the sprite from Resources folder
        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite != null)
        {
            ProfileImage.sprite = sprite;
            sprite.texture.filterMode = FilterMode.Point;
            ProfileImage.drawMode = SpriteDrawMode.Sliced;
            // Scale to parent size with 5% border
            Vector2 parentSize = transform.localScale;
            float borderScale = 0.95f; // 5% border
            ProfileImage.size = parentSize * borderScale;
            
            // Make sprite fully visible when speaking, grey when silent
            ProfileImage.color = Speaking ? Color.white : new Color(0.5f, 0.5f, 0.5f, 0.7f);
            Resources.UnloadUnusedAssets();
        }
    }
}
