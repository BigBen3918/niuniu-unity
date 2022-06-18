using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public static class ExtensionMethods
{

    public static Transform[] FindChildren(this Transform transform, string name)
    {
        return transform.GetComponentsInChildren<Transform>().Where(t => t.name == name).ToArray();
    }

    public static IEnumerator GetTextureFromURL(string url, System.Action<Texture2D, bool> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.isDone == false || request.error != null)
        {
            Debug.LogWarning(request.error);
            yield break;
        }
        callback(((DownloadHandlerTexture)request.downloadHandler).texture, true);
    }
    public static void Clear(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}