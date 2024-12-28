using UnityEngine;

public class Table : MonoBehaviour
{
    public GameObject gridObjectPrefab; // The prefab you want to place in the grid 
    public int columns = 5; // Number of columns in the grid 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateSlots() 
    {
        // Get the parent's size (assuming it has a RectTransform component)
        RectTransform rectTransform = GetComponent<RectTransform>();
        float parentWidth = rectTransform != null ? rectTransform.rect.width : 10f;
        float parentHeight = rectTransform != null ? rectTransform.rect.height : 10f;

        // Calculate available space with padding
        float padding = 0.1f * parentWidth; // 10% padding
        float totalWidth = parentWidth - (2 * padding);
        float spacing = totalWidth / (columns - 1); // Distribute across edges            
        float availableHeight = parentHeight - (2 * padding);

        // Get prefab size for reference
        Vector3 prefabSize = gridObjectPrefab.GetComponent<Renderer>().bounds.size;
        float prefabScale = (spacing / prefabSize.x) * 0.8f; // Scale to fit within spacing
        float heightScale = availableHeight;

        for (int x = 0; x < columns; x++)
        {
            float xPosition = -totalWidth / 2 + (x * spacing);
            GameObject instance = Instantiate(gridObjectPrefab, transform);
            instance.transform.localPosition = Vector3.right * xPosition;
            instance.transform.localScale = new Vector3(prefabScale, heightScale, prefabScale);
            instance.name = "CardSlot" + x;
        }
    }

}
