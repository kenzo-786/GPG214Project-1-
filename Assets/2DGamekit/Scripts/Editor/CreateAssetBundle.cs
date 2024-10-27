using System;
using UnityEditor;
using UnityEngine;
using System.IO;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        // Make sure the folder exists
        if (!System.IO.Directory.Exists("Assets/StreamingAssets"))
            System.IO.Directory.CreateDirectory("Assets/StreamingAssets");

        // Build the AssetBundle
        BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
