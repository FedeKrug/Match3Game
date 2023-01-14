using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
	[SerializeField] private int _sizeX;
	[SerializeField] private int _sizeY;
	[SerializeField] private BackgroundTile[] _tiles;
	[SerializeField] private BackgroundTile[,] _grid;
	[SerializeField] private GameObject _tilesContainer;
	private int _dragX = -1;
	private int _dragY = -1;
	[SerializeField] private bool _canMove = false;

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
		Check();
	}


	void Check()
	{
		List<BackgroundTile> tilesToCheck = CheckHorizontalMatches();
		tilesToCheck.AddRange(CheckVerticalMatches());

		for (int i = 0; i < tilesToCheck.Count; i++)
		{
			if (tilesToCheck[i].y < _sizeY)
			{
				MoveTiles(tilesToCheck[i].x, tilesToCheck[i].y,
					tilesToCheck[i].x, tilesToCheck[i].y + _sizeY);
			}

		}
		StartCoroutine(GravityEffect());
	}

	IEnumerator GravityEffect()
	{
		bool switched = false;
		_canMove = false;

		while (!switched)
		{
			for (int j = 0; j < _sizeY * 2; j++)
			{
				for (int i = 0; i < _sizeX; i++)
				{
					if (Fall(i, j))
					{
						switched = true;
					}
				}

				if (j <= _sizeY)
				{
					yield return null;

				}
			}
		}
		Check();
		_canMove = true;
	}

	bool Fall(int x, int y)
	{
		if (x < 0 || y < 0 || x >= _sizeX || y >= _sizeY *2)
		{
			return false;
		}
		if (_grid[x, y] != null)
		{
			return false;
		}
		if (_grid[x, y - 1] != null)
		{
			return false;
		}
		MoveTiles(x, y, x, y - 1);
		return true;
	}

	List<BackgroundTile> CheckVerticalMatches()
	{
		List<BackgroundTile> tilesToCheck = new List<BackgroundTile>();
		List<BackgroundTile> tilesToReturn = new List<BackgroundTile>();
		string type = "";

		for (int i = 0; i < _sizeX; i++)
		{
			for (int j = 0; j < _sizeY; j++)
			{
				if (_grid[i, j].tileType != type)
				{
					if (tilesToCheck.Count >= 3)
					{
						tilesToReturn.AddRange(tilesToCheck);
					}
					tilesToCheck.Clear();
				}

				type = _grid[i, j].tileType;
				tilesToCheck.Add(_grid[i, j]);
			}

			if (tilesToCheck.Count >= 3)
			{
				tilesToReturn.AddRange(tilesToCheck);
			}
			tilesToCheck.Clear();


		}


		return tilesToReturn;
	}

	List<BackgroundTile> CheckHorizontalMatches()
	{
		List<BackgroundTile> tilesToCheck = new List<BackgroundTile>();
		List<BackgroundTile> tilesToReturn = new List<BackgroundTile>();
		string type = "";

		for (int j = 0; j < _sizeX; j++)
		{
			for (int i = 0; i < _sizeY; i++)
			{
				if (_grid[j, i].tileType != type)
				{
					if (tilesToCheck.Count >= 3)
					{
						tilesToReturn.AddRange(tilesToCheck);
					}
					tilesToCheck.Clear();
				}

				type = _grid[j, i].tileType;
				tilesToCheck.Add(_grid[j, i]);
			}

			if (tilesToCheck.Count >= 3)
			{
				tilesToReturn.AddRange(tilesToCheck);
			}
			tilesToCheck.Clear();


		}


		return tilesToReturn;
	}

	public void Drag(BackgroundTile tile)
	{
		if (!_canMove) return;
		_dragX = tile.x;
		_dragY = tile.y;
	}

	public void Drop(BackgroundTile tile)
	{
		if (!_canMove) return;

		if (_dragX == -1 || _dragY == -1) return;

		SwapTiles(_dragX, _dragY, tile.x, tile.y);
		_dragX = -1;
		_dragY = -1;
	}

	private void SwapTiles(int x1, int x2, int y1, int y2)
	{
		if (x1 == x2 && y1 == y2) return;
		MoveTiles(x1, y1, x2, y2);
		//comprobar match
		List<BackgroundTile> tilesToCheck = CheckHorizontalMatches();
		tilesToCheck.AddRange(CheckVerticalMatches());

		if (tilesToCheck.Count == 0)
		{
			MoveTiles(x1, y1, x2, y2);
		}
		Check();
	}


	private void MoveTiles(int x1, int x2, int y1, int y2)
	{
		if (_grid[x1, y1] != null)
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
		if (_grid[x1, y1])
		{
			_grid[x1, y1].ChangePos(x1, y1);
		}
		if (_grid[x2, y2])
		{
			_grid[x2, y2].ChangePos(x2, y2);
		}
	}

	private void SetUp(int x, int y)
	{

		Vector2 tempPos = new Vector2(x, y);

		BackgroundTile backgroundTile = Instantiate(_tiles[Random.Range(0, _tiles.Length)],
			tempPos, Quaternion.identity, _tilesContainer.transform);
		backgroundTile.Constructor(this, x, y);
		_grid[x, y] = backgroundTile;
	}
}
