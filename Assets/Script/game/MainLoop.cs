using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainLoop : MonoBehaviour {

    public struct point
    {
        public int x, y, v;
    };

    public GameObject white_prefab;
    public GameObject black_prefab;
    int state;
    bool flag = false;
    Board board;
    AI ai;

    public bool check_point(point p) // check if valid
    {
        return !(p.x > 14 || p.x < 0 || p.y > 14 || p.y < 0 || BoardModel.map[p.x, p.y] != 0);
    }

    int place_chess(Cross cross, bool is_black)
    {
        if (cross == null)
        {
            return 0;
        }
        var new_chess = GameObject.Instantiate<GameObject>(is_black ? black_prefab : white_prefab);
        new_chess.transform.SetParent(cross.gameObject.transform, false);
        BoardModel.set_type(cross.grid_x, cross.grid_y, is_black ? 1 : 2);
        int win = BoardModel.check_win();
        return win;
    }

    public void Restart()
    {
        state = 1;
        flag = false;
        ai = new AI();
        board.Reset();
    }

    public void on_click(Cross cross) // player's turn
    {
        if (state != 1)
        {
            return;
        }
        point tmp = new point();
        tmp.x = cross.grid_x;
        tmp.y = cross.grid_y;
        if (check_point(tmp))
        {
            if (place_chess(cross, true) == 1) // player wins
            {
                state = 3;
            }
            else
            {
                state = 2;
            }
        }
    }
    
    void Start ()
    {
        board = GetComponent<Board>();
        Restart();
    }
	
	void Update ()
    {

        switch (state)
        {
            case 2:
                {
                    point tmp = new point();
                    tmp = ai.select_point();

                    if (place_chess(board.GetCross(tmp.x, tmp.y), false) == 2) // AI wins
                    {
                        state = 4;
                    }
                    else
                    {
                        state = 1;
                    }
                    break;
                }
            case 3:
                {
                    if (!flag)
                    {
                        BoardModel.win_cnt++;
                        Text result_text = GameObject.Find("Canvas/Result").GetComponent<Text>();
                        result_text.text = "Congratulations! You won the game!";
                        flag = true;
                    }
                    break;
                }
            case 4:
                {
                    if (!flag)
                    {
                        BoardModel.lose_cnt++;
                        Text result_text = GameObject.Find("Canvas/Result").GetComponent<Text>();
                        result_text.text = "Too bad. You lost the game......";
                        flag = true;
                    }
                    break;
                }
        }

        Text win_text = GameObject.Find("Canvas/WinCount").GetComponent<Text>();
        win_text.text = Convert.ToString(BoardModel.win_cnt);
        Text lose_text = GameObject.Find("Canvas/LoseCount").GetComponent<Text>();
        lose_text.text = Convert.ToString(BoardModel.lose_cnt);
    }
}
