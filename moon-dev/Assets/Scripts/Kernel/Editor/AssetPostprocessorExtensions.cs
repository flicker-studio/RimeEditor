using UnityEditor;

namespace Moon.Kernel.Editor
{
    /// <inheritdoc />
    public class AssetPostprocessorExtensions : AssetPostprocessor
    {
        private void OnPreprocessTexture()
        {
            if (!assetPath.Contains(".png"))
            {
                return;
            }

            var textureImporter = (TextureImporter)assetImporter;
            textureImporter.textureType = TextureImporterType.Sprite;
            textureImporter.mipmapEnabled = false;
        }
    }
}