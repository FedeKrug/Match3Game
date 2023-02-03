using System.Collections.Generic;

using UnityEngine;

public class BoardManager : MonoBehaviour
{

	public static BoardManager instance;
	public List<GameObject> prefabs = new List<GameObject>();
	public GameObject currentCandy;
	[SerializeField] private int _xSize, _ySize;

	private GameObject[,] _candies;

	public bool isShifting { get; set; }
	private Candy _selectedCandy;

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

		int idX = -1;

		for (int x = 0; x < _xSize; x++)
		{
			for (int y = 0; y < _ySize; y++)
			{
				GameObject newCandy = Instantiate(currentCandy, new Vector3(startX + (offset.x * x), startY + (offset.y * y), 0), currentCandy.transform.rotation, this.transform);

				newCandy.name = string.Format("Candy[{0}][{1}]", x, y);

				do
				{
					idX = Random.Range(0, prefabs.Count);

				}
				while ((x > 0 && idX == _candies[x - 1, y].GetComponent<Candy>().id) || (y > 0 && idX == _candies[x, y - 1].GetComponent<Candy>().id)); //ejecuta la asignacion de un valor aleatorio tomando en cuenta los candy de la izquierda y abajo respectivamente
				

					var candyPrefab = prefabs[idX];
				Instantiate(candyPrefab, newCandy.GetComponent<Candy>().transform.position, currentCandy.transform.rotation, newCandy.transform);
				newCandy.GetComponent<Candy>().id = idX;

				_candies[x, y] = newCandy;
			}
		}
	}
}