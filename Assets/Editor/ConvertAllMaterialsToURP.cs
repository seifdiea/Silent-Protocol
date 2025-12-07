using UnityEditor;
using UnityEngine;

public class ConvertAllMaterialsToURP
{
    [MenuItem("Tools/Fix Pink Materials")]
    public static void Convert()
    {
        string[] guids = AssetDatabase.FindAssets("t:Material");
        int count = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat != null && mat.shader.name.Contains("Standard"))
            {
                mat.shader = Shader.Find("Universal Render Pipeline/Lit");
                count++;
            }
        }

        Debug.Log("Converted " + count + " materials to URP/Lit.");
    }
}
