using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class Grid
{
    private int width;
    private int height;
    private float cellsize;
    private int[,] gridArray;
    private TextMesh[,] debugArray;

    public Grid(int width, int height, float cellsize){
        this.width = width;
        this.height = height;
        this.cellsize = cellsize;

        gridArray = new int[width,height];

        for(int i=0; i < gridArray.GetLength(0); i++){
            for(int j=0; j < gridArray.GetLength(1); j++){
                UtilsClass.CreateWorldText(gridArray[i, j].ToString(), null, GetWorldPosition(i, j) + new Vector3(cellsize, cellsize) * .5f, 20, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i, j+1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(i, j), GetWorldPosition(i + 1, j), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int y){
        return new Vector3(x,y) * cellsize;
    }

    public void SetValue(int x, int y, int value){
        gridArray[x, y] = value;
    }
}
