using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    // Stores each loaded texture and the number of active users.
    private class TextureEntry
    {
        public Texture2D texture;
        public int referenceCount;

        public TextureEntry(Texture2D loadedTexture)
        {
            texture = loadedTexture;
            referenceCount = 1;
        }
    }

    // Cache: one entry per texture path.
    private Dictionary<string, TextureEntry> loadedTextures =
        new Dictionary<string, TextureEntry>();

    public Texture2D LoadTexture(string path)
    {
        // REUSE: If already loaded, increase its user count.
        if (loadedTextures.TryGetValue(path, out TextureEntry entry))
        {
            entry.referenceCount++;

            Debug.Log($"Reusing {path}. Users: {entry.referenceCount}");
            return entry.texture;
        }

        // ALLOCATE/LOAD: Ask Unity to load the texture asset.
        Texture2D texture = Resources.Load<Texture2D>(path);

        if (texture == null)
        {
            Debug.LogError($"Could not load texture: {path}");
            return null;
        }

        // Cache the texture so future requests reuse it.
        loadedTextures.Add(path, new TextureEntry(texture));

        Debug.Log($"Loaded {path}. Unique textures: {loadedTextures.Count}");

        return texture;
    }

    public void ReleaseTexture(string path)
    {
        // Ignore release requests for textures that aren't cached.
        if (!loadedTextures.TryGetValue(path, out TextureEntry entry))
        {
            Debug.LogWarning($"Texture is not loaded: {path}");
            return;
        }

        // Decrement the count because one user no longer needs it.
        entry.referenceCount--;

        // Keep the texture loaded while other users still need it.
        if (entry.referenceCount > 0)
        {
            Debug.Log($"Keeping {path} loaded. Users: {entry.referenceCount}");
            return;
        }

        // DEALLOCATE/UNLOAD: Remove the cache entry first.
        loadedTextures.Remove(path);

        // The caller must stop using the texture before releasing it.
        // Resources.UnloadAsset releases this loaded asset's memory.
        Resources.UnloadAsset(entry.texture);

        Debug.Log($"Unloaded {path}. Unique textures: {loadedTextures.Count}");
    }

    public int GetLoadedTextureCount()
    {
        return loadedTextures.Count;
    }
}