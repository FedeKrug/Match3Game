using System.Collections.Generic;

using UnityEngine;

public class BoardManager : MonoBehaviour
{

	public static BoardManager instance;
	public List<Sprite> prefabs = new List<Sprite>();
	public GameObject currentCandy;
	[SerializeField] private int _xSize, _ySize;


	private GameObject[,] _candies;

	public bool isShifting { get; set; }


	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		Vector2 offset = currentCandy.GetComponent<BoxCollider2D>().size;
		CreateInitialBoard(offset);
	}

	private void CreateInitialBoard(Vector2 offset)
	{
		_candies = new GameObject[_xSize, _ySize];

		float startX = this.transform.position.x;
		float startY = this.transform.position.y;

		for (int x = 0; x < _xSize; x++)
		{
			for (int y = 0; y < _ySize; y++)
			{
				GameObject newCandy = Instantiate(currentCandy, new Vector3(startX + (offset.x * x), startY + (offset.y * y), 0), currentCandy.transform.rotation);

				newCandy.name = string.Format("Candy[{0}][{1}]", x, y);
				_candies[x, y] = newCandy;
			}
		}
	}
}