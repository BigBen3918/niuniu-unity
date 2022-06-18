using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AI {
    
    int[] score1 = { 7, 35, 800, 15000, 800000 }, score2 = { 7, 15, 400, 2500, 100000 };
    // score1: score of own pieces { 0, 1, 2, 3, 4 } in a row
    // score2: score of opponent's pieces { 0, 1, 2, 3, 4 } in a row

    public List<MainLoop.point> count_point(int k) // rank of each point on current board
    {
        List<MainLoop.point> vp = new List<MainLoop.point>();
        MainLoop.point tmp = new MainLoop.point();
        int i, j, x, y, cnt1, cnt2;
        int[,] rank = new int[Board.count, Board.count];
        int[,] mv = { { 1, 1, 0, -1 }, { 0, 1, 1, 1 } }; // move in 4 directions
        for (x = 0; x < 15; x++)
	    {
		    for (y = 0; y < 15; y++)
		    {
			    for (i = 0; i < 4; i++)
			    {
				    cnt1 = 0;
				    cnt2 = 0;
				    if (x + 4 * mv[0, i] < 0 || x + 4 * mv[0, i] > 14 || y + 4 * mv[1, i] < 0 || y + 4 * mv[1, i] > 14)
				    {
					    continue;
				    }
				    for (j = 0; j < 5; j++)
				    {
					    if (BoardModel.map[x + j * mv[0, i], y + j * mv[1, i]] == k)
					    {
						    cnt1++;
					    }
					    else if (BoardModel.map[x + j * mv[0, i], y + j * mv[1, i]] != k && BoardModel.map[x + j * mv[0, i], y + j * mv[1, i]] != 0)
					    {
						    cnt2++; // other color
					    }
				    }
				    if (cnt1 == 0 && cnt2 != 0)
				    {
					    for (j = 0; j < 5; j++)
					    {
						    rank[x + j * mv[0, i], y + j * mv[1, i]] += score2[cnt2];
					    }
				    }
				    else if (cnt1 != 0 && cnt2 == 0)
				    {
					    for (j = 0; j< 5; j++)
					    {
						    rank[x + j * mv[0, i], y + j * mv[1, i]] += score1[cnt1];
					    }
				    }
				    else if (cnt1 == 0 && cnt2 == 0)
				    {
					    for (j = 0; j < 5; j++)
					    {
						    rank[x + j * mv[0, i], y + j * mv[1, i]] += score1[0];
					    }
				    }
			    }
		    }
	    }
	    for (i = 0; i < 15; i++)
	    {
		    for (j = 0; j < 15; j++)
		    {
			    if (BoardModel.map[i, j] != 0)
			    {
				    continue;
			    }
			    tmp.x = i;
			    tmp.y = j;
			    tmp.v = rank[i, j];
			    vp.Add(tmp);
		    }
	    }
	    return vp;
    }
    
    public MainLoop.point select_point()
    {
        List<MainLoop.point> vp = new List<MainLoop.point>();
        int i;
        MainLoop.point point = new MainLoop.point();
        vp = count_point(2);
        vp.Sort((a, b) =>
        {
            if (a.v > b.v)
            {
                return -1;
            }
            return 1;
        });
        if (vp[0].v >= 100000)
        {
            point.x = vp[0].x;
            point.y = vp[0].y;
            return point;
        }
        for (i = 1; i < vp.Count; i++)
        {
            if (vp[i].v != vp[0].v)
            {
                break;
            }
        }
        point = vp[(int)Math.Floor(UnityEngine.Random.Range(0, i - 1) + 0.5)];
        return point;
    }
}
