using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	[SerializeField] private int _sizeX;
	[SerializeField] private int _sizeY;
	[SerializeField] private BackgroundTile[] _tiles;
	[SerializeField] private BackgroundTile[,] _grid;

	private int _dragX = -1;
	private int _dragY = -1;


	private void Start()
	{
		_grid = new BackgroundTile[_sizeX, _sizeY];
		for (int i = 0; i < _sizeX; i++)
		{
			for (int j = 0; j < _sizeY; j++)
			{
				SetUp(i, j);

			}
		}
	}

	public void Drag(BackgroundTile tile)
	{
		_dragX = tile.x;
		_dragY = tile.y;
	}

	public void Drop(BackgroundTile tile)
	{
		if (_dragX ==-1 || _dragY == -1) return;
		
		SwapTiles(_dragX, _dragY, tile.x, tile.y);
		_dragX = -1;
		_dragY = -1;
	}

	private void SwapTiles(int x1, int x2, int y1, int y2)
	{
		if (x1 == x2 && y1 == y2) return;
		MoveTiles(x1, x2, y1, y2);
		//comprobar match
	}


	private void MoveTiles(int x1, int x2, int y1, int y2)
	{
		if (_grid[x1, y1]!=null)
		{
			_grid[x1, y1].transform.position = new Vector3(x2, y2);
		}
		if (_grid[x2, y2] != null)
		{
			_grid[x2, y2].transform.position = new Vector3(x1, y1);
		}

		BackgroundTile temp = _grid[x1, y1];
		_grid[x1, y1] = _grid[x2, y2];
		_grid[x2, y2] = temp;

		_grid[x1, y1].ChangePos(x1, y1);
		_grid[x2, y2].ChangePos(x2, y2);

	}

	private void SetUp(int x, int y)
	{

		Vector2 tempPos = new Vector2(x, y);

		BackgroundTile backgroundTile = Instantiate(_tiles[Random.Range(0, _tiles.Length)],
			tempPos, Quaternion.identity);
		backgroundTile.Constructor(this, x, y);
		_grid[x, y] = backgroundTile;
	}
}
