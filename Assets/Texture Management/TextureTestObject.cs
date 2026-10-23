using UnityEngine;

public class TextureTestObject : MonoBehaviour
{
    [SerializeField] private TextureManager textureManager;
    [SerializeField] private string texturePath = "Textures/Brick";

    private Renderer objectRenderer;
    private Material runtimeMaterial;
    private Texture2D assignedTexture;

    void OnEnable()
    {
        objectRenderer = GetComponent<Renderer>();

        if (textureManager == null)
        {
            Debug.LogError("Assign a TextureManager first.");
            return;
        }

        // Request the texture. The manager loads or reuses it.
        assignedTexture = textureManager.LoadTexture(texturePath);

        if (assignedTexture != null && objectRenderer != null)
        {
            // Create a material instance for this object.
            runtimeMaterial = objectRenderer.material;

            // Assign the texture to the material.
            runtimeMaterial.mainTexture = assignedTexture;
        }
    }

    void OnDestroy()
    {
        if (runtimeMaterial != null)
        {
            // Stop referencing the texture before unloading it.
            runtimeMaterial.mainTexture = null;

            // Release the material instance we created.
            Destroy(runtimeMaterial);
        }

        if (assignedTexture != null && textureManager != null)
        {
            // Tell the manager this object no longer needs the texture.
            textureManager.ReleaseTexture(texturePath);
            assignedTexture = null;
        }
    }
}