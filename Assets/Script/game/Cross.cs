using UnityEngine;
using UnityEngine.UI;

public class Cross : MonoBehaviour {

    public int grid_x, grid_y;
    public MainLoop mainloop;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            mainloop.on_click(this);
        });
    }

}
