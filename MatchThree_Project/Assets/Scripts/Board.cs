using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeY;
    [SerializeField] private BackgroundTile [] _tiles;
    [SerializeField] private BackgroundTile[,] _grid;
    void Awake()
    {
        
    }

	private void Start()
	{
        _grid = new BackgroundTile[_sizeX, _sizeY];
        for (int i = 0; i < _sizeX; i++)
        {
            for (int j = 0; j < _sizeY; j++)
            {
                Vector2 tempPos = new Vector2(i, j);

                BackgroundTile backgroundTile = Instantiate(_tiles[Random.Range(0, _tiles.Length)],
                    tempPos, Quaternion.identity);
                _grid[i, j] = backgroundTile;

            }
        }
    }
	void Update()
    {
        
    }

    private void SetUp()
	{
       
        
	}
}
