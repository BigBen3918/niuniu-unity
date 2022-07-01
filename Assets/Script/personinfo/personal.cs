using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class personal : MonoBehaviour
{
    public Text username, userid;
    public RawImage userimage;

    // Start is called before the first frame update
    void Start()
    {
        username.text = GlobalDatas.authdata.username;
        userid.text = GlobalDatas.authdata.id.ToString();
        StartCoroutine(ExtensionMethods.GetTextureFromURL(GlobalDatas.authdata.image, (Texture2D coverImage, bool isSuccess) =>
        {
            if (!isSuccess) return;
            userimage.texture = coverImage;
        }));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void exit()
    {
        SceneManager.LoadScene(1);
    }
}