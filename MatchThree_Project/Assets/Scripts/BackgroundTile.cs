using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTile : MonoBehaviour
{
	[SerializeField] private Board _boardReference;
	[SerializeField] private TileType _tileType;
	[SerializeField] public int x, y;
	void Awake()
	{

	}


	void Update()
	{

	}

	public void Constructor(Board boardManager, int X, int Y)
	{
		this.x = X;
		this.y = Y;
		_boardReference = boardManager;
	}

	public void ChangePos(int X, int Y)
	{
		this.x = X;
		this.y = Y;
	}

	private void OnMouseDown()
	{
		_boardReference.Drag(this);
		Debug.Log($"Drag {x}, {y}");

	}
	private void OnMouseOver()
	{
		if (Input.GetMouseButtonUp(0))
		{
			_boardReference.Drop(this);
			Debug.Log($"Drop {x}, {y}");

		}
	}

}

public enum TileType
{
	Red,
	Orange,
	Green,
	Cyan
}