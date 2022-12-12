using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace WWW
{
    public class BundleWebLoader : MonoBehaviour
    {
        [SerializeField] private string bundleUrl;


        private void Start() => 
            StartCoroutine(InstantiateObject());

        private IEnumerator InstantiateObject()
        {
            string url = bundleUrl;        
            var request = UnityWebRequestAssetBundle.GetAssetBundle(url, 0);
            yield return request.SendWebRequest();
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            GameObject background = bundle.LoadAsset<GameObject>("BackgroundSprite_lvl1");
            
            Instantiate(background);
        }
    }
}
