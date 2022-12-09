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

    public Grid(int width, int height, float cellsize){
        this.width = width;
        this.height = height;
        this.cellsize = cellsize;

        gridArray = new int[width,height];

        for(int i=0; i < gridArray.GetLength(0); i++){
            for(int j=0; j < gridArray.GetLength(1); j++){
                UtilsClass.CreateWorldText(gridArray[i, j].ToString(), null, GetWorldPosition(i, j), 5, Color.white, TextAnchor.MiddleCenter);
                Debug.Log(i + " " + j);
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y){
        return new Vector3(x,y) * cellsize;
    }
}
