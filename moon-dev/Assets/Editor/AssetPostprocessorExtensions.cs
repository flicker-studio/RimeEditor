using UnityEditor;

public class AssetPostprocessorExtensions : AssetPostprocessor
{
    void OnPreprocessTexture()
    { if (assetPath.Contains(".png"))
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.mipmapEnabled = false;
        }
    }
}
