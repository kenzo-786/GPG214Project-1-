using System.Collections;
using UnityEngine;

public class LoadAssetBundle : MonoBehaviour
{
    // Set this to the name of your AssetBundle file in StreamingAssets (with the extension if applicable)
    public string assetBundleName = "cubeprefab.unity3d"; // Adjust this name based on your AssetBundle file
    // Set this to the exact name of the asset inside the AssetBundle that you want to load
    public string assetName = "Square"; // Change this to the actual asset name in the bundle

    private void Start()
    {
        StartCoroutine(LoadAssetFromBundle());
    }

    private IEnumerator LoadAssetFromBundle()
    {
        // Construct the full path to the AssetBundle in StreamingAssets
        string bundlePath = System.IO.Path.Combine(Application.streamingAssetsPath, assetBundleName);
        Debug.Log("Loading AssetBundle from: " + bundlePath);

        // Load the AssetBundle
        AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromFileAsync(bundlePath);
        yield return bundleRequest;

        AssetBundle bundle = bundleRequest.assetBundle;

        if (bundle != null)
        {
            Debug.Log("AssetBundle loaded successfully!");

            // List all assets in the AssetBundle for debugging purposes
            var assets = bundle.LoadAllAssets<GameObject>();
            foreach (var asset in assets)
            {
                Debug.Log("Found asset in bundle: " + asset.name);
            }

            // Attempt to load the specified asset
            AssetBundleRequest assetLoadRequest = bundle.LoadAssetAsync<GameObject>(assetName);
            yield return assetLoadRequest;

            GameObject loadedAsset = assetLoadRequest.asset as GameObject;
            if (loadedAsset != null)
            {
                // Instantiate the loaded asset in the scene
                Instantiate(loadedAsset);
                Debug.Log("Asset instantiated successfully!");
            }
            else
            {
                Debug.LogError("Failed to load asset: " + assetName);
            }

            // Optionally unload the AssetBundle but keep the loaded assets in memory
            bundle.Unload(false);
        }
        else
        {
            Debug.LogError("Failed to load AssetBundle from path: " + bundlePath);
        }
    }
}
