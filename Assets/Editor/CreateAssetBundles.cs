using UnityEditor;
using System.IO;

public class CreateAssetBundles
{
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectoryStandalone = "Assets/StreamingAssets/StandAlone";
        string assetBundleDirectoryAndroid = "Assets/StreamingAssets/Android";
        string assetBundleDirectoryIos = "Assets/StreamingAssets/IOS";
        
        if(!Directory.Exists(assetBundleDirectoryStandalone)) 
            Directory.CreateDirectory(assetBundleDirectoryStandalone);
        
        if(!Directory.Exists(assetBundleDirectoryAndroid)) 
            Directory.CreateDirectory(assetBundleDirectoryAndroid);
        
        if(!Directory.Exists(assetBundleDirectoryIos)) 
            Directory.CreateDirectory(assetBundleDirectoryIos);
        
        BuildPipeline.BuildAssetBundles(assetBundleDirectoryStandalone, 
            BuildAssetBundleOptions.None, 
            BuildTarget.StandaloneWindows);
        
        BuildPipeline.BuildAssetBundles(assetBundleDirectoryAndroid, 
            BuildAssetBundleOptions.None, 
            BuildTarget.Android);
        
        BuildPipeline.BuildAssetBundles(assetBundleDirectoryIos, 
            BuildAssetBundleOptions.None, 
            BuildTarget.iOS);
    }
}
