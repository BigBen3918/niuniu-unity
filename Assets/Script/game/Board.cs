using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour {

    public GameObject cross_prefab;
    const float cross_size = 46; // size of crosses
    public const int count = 15, place = 323; // number and initial position of crosses
    Dictionary<int, Cross> cross_map = new Dictionary<int, Cross>();

    static int MakeKey(int x, int y) // hash
    {
        return x * 10000 + y;
    }

    public void Reset() // reset the board
    {
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        var mainLoop = GetComponent<MainLoop>();
        cross_map.Clear();
        BoardModel.map = new int[Board.count, Board.count];

        int x, y;
        for (x = 0; x < count; x++) // init all crosses
        {
            for (y = 0; y < count; y++)
            {
                var cross_object = GameObject.Instantiate<GameObject>(cross_prefab);
                cross_object.transform.SetParent(gameObject.transform);
                cross_object.transform.localScale = Vector3.one;

                var pos = cross_object.transform.localPosition;
                pos.x = -place + x * cross_size;
                pos.y = -place + y * cross_size;
                pos.z = 1;
                cross_object.transform.localPosition = pos;

                var cross = cross_object.GetComponent<Cross>();
                cross.grid_x = x;
                cross.grid_y = y;
                cross.mainloop = mainLoop;

                cross_map.Add(MakeKey(x, y), cross);
            }
        }

        Text result_text = GameObject.Find("Result").GetComponent<Text>();
        result_text.text = "";
    }

    public Cross GetCross(int x, int y)
    {
        Cross cross;
        if (cross_map.TryGetValue(MakeKey(x, y), out cross))
        {
            return cross;
        }
        return null;
    }
    
	void Start ()
    {
        Reset();
	}

}
