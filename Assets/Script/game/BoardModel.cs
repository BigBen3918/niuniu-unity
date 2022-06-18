using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardModel : MonoBehaviour {

    public static int[,] map = new int[Board.count, Board.count];
    public static int[,] mv = { { 1, 1, 0, -1 }, { 0, 1, 1, 1 } };
    public static int win_cnt = 0, lose_cnt = 0;

    public static int get_type(int x, int y) // get value of a piece
    {
        if (x < 0 || x >= Board.count)
        {
            return 0;
        }
        if (y < 0 || y >= Board.count)
        {
            return 0;
        }
        return map[x, y];
    }

    public static bool set_type(int x, int y, int k) // place a piece
    {
        if (x < 0 || x >= Board.count)
        {
            return false;
        }
        if (y < 0 || y >= Board.count)
        {
            return false;
        }
        map[x, y] = k;
        return true;
    }

    public static int check_win()
    {
        int i, j, k, l, flag;
        for (i = 0; i < 15; i++)
        {
            for (j = 0; j < 15; j++)
            {
                if (map[i, j] == 0)
                {
                    continue;
                }
                for (k = 0; k < 4; k++)
                {
                    flag = 0;
                    if (i + mv[0, k] * 4 > 14 || i + mv[0, k] * 4 < 0 || j + mv[1, k] * 4 > 14 || j + mv[1, k] < 0)
                    {
                        continue;
                    }
                    for (l = 1; l < 5; l++)
                    {
                        if (map[i + mv[0, k] * l, j + mv[1, k] * l] != map[i, j])
                        {
                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        return map[i, j];
                    }
                }
            }
        }
        return 0;
    }
}