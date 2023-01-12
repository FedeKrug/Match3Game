using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
    [SerializeField] private Board _boardReference;
    [SerializeField] private TileType _tileType;
    [SerializeField] private int _x, _y;
    void Awake()
    {
        
    }

   
    void Update()
    {
        
    }

    public void Constructor(Board boardManager, int x, int y)
	{
        _x = x;
        _y = y;
        _boardReference = boardManager;
	}

    public void ChangePos(int X, int Y)
	{
        _x = X;
        _y = Y;
	}
}

public enum TileType
{
    Red,
    Orange,
    Green,
    Cyan
}