using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject gridObjectPrefab; // The prefab you want to place in the grid 
    public int rows = 5; // Number of rows in the grid 
    public int columns = 5; // Number of columns in the grid 



    #region init
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    void CreateGrid() 
    {
        // Get the parent's size
        Vector3 parentSize = transform.localScale;
        
        // Calculate spacing based on parent size and grid dimensions
        float spacingX = parentSize.x / (columns - 1);
        float spacingZ = parentSize.z / (rows - 1);
        
        // Calculate starting position to center the grid
        Vector3 startPos = new Vector3(-parentSize.x / 2, 0, -parentSize.z / 2);
        
        for (int i = 0; i < rows; i++) 
        { 
            for (int j = 0; j < columns; j++) 
            { 
            Vector3 position = startPos + new Vector3(i * spacingX, 0, j * spacingZ);
            GameObject gridObject = Instantiate(gridObjectPrefab, position, Quaternion.identity);
            gridObject.transform.SetParent(transform);
            
            // Scale the grid object to fit if needed
            float objectScale = Mathf.Min(spacingX, spacingZ) * 0.9f; // 90% of spacing to add margins
            gridObject.transform.localScale = new Vector3(objectScale, objectScale, objectScale);
            } 
        } 
    }
}
