using UnityEngine;

public class FitToCamera : MonoBehaviour
{
    public Camera mainCamera; 
    void Start() 
    { 
        //FitObjectToCamera(); 
    } 
    
    private void FitObjectToCamera() 
    { 
        // Get camera bounds at a specific distance
        float distance = Vector3.Distance(mainCamera.transform.position, transform.position);
        float height = 2.0f * distance * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float width = height * mainCamera.aspect;

        // Get the object's current size
        Bounds bounds = GetComponent<Renderer>().bounds;
        float objectWidth = bounds.size.x;
        float objectHeight = bounds.size.y;

        // Calculate scale factors
        float scaleX = width / objectWidth;
        float scaleY = height / objectHeight;
        float scale = Mathf.Min(scaleX, scaleY);

        // Apply the scale
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
