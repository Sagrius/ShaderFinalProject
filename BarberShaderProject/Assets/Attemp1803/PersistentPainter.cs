using UnityEngine;

public class PersistentPainter : MonoBehaviour
{
    public RenderTexture canvasTexture;  // The persistent painting surface
    public Material brushMaterial, canvasMaterial;       // Material that applies the brush stroke

  [SerializeField]  private RenderTexture tempTexture;   // Temporary buffer for double buffering

    public float brushSize = 0.05f;      // Brush size to control the stroke width
    private Camera _camera;              // Camera to convert screen coordinates to world space

    void Start()
    {
        _camera = Camera.main; // Get the main camera
                               // Create a temporary texture that has the same dimensions as the canvas
        canvasMaterial = GetComponent<Renderer>().materials[0];
       // Graphics.Blit(canvasTexture, tempTexture); // Initialize temp texture with the canvas texture
    }

    void Update()
    {
        // Left mouse button to paint
        if (Input.GetMouseButton(0))
        {
            // Raycast from the camera to the surface being painted
            //omnomnom
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector2 uv = hit.textureCoord; // Get texture UV coordinates where the mouse clicked
                Paint(uv);
            }
        }
    }

    public void Paint(Vector2 uv)
    {
        // Set brush position and size in the shader
        brushMaterial.SetVector("_BrushUV", uv); // Passing the UV coordinates
        brushMaterial.SetFloat("_BrushSize", brushSize); // Passing the brush size
                                                         //tempTexture =  RenderTexture.GetTemporary(canvasTexture.width, canvasTexture.height, 0, RenderTextureFormat.ARGB32);

        // // Step 1: Copy the current canvas to the temporary texture (to preserve the current strokes)
        // Graphics.Blit(canvasTexture, tempTexture);

        // tempTexture.Create();
        // // Step 2: Apply the brush stroke on the temporary texture (this is where the new stroke is applied)
        // Graphics.Blit(tempTexture, canvasTexture, brushMaterial, 1); // Update the canvas texture with the new stroke
        // tempTexture.Create();
        //// RenderTexture.ReleaseTemporary(tempTexture);
        ///
        RenderTexture temp = RenderTexture.GetTemporary(canvasTexture.width, canvasTexture.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(canvasTexture, temp);
        Graphics.Blit(temp, canvasTexture, brushMaterial,1);
        RenderTexture.ReleaseTemporary(temp);
    }

    void OnDestroy()
    {
        tempTexture.Release(); // Clean up the temporary buffer
    }
}