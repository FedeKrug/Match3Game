using System.Collections.Generic;

using UnityEngine;

public class BoardManager : MonoBehaviour
{

	public static BoardManager instance;
	public List<GameObject> prefabs = new List<GameObject>();
	public GameObject currentCandy;
	[SerializeField] private int _xSize, _ySize;
	[SerializeField] private Transform _candiesParentObject;

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
				GameObject newCandy = Instantiate(currentCandy, new Vector3(startX + (offset.x * x), startY + (offset.y * y), 0), currentCandy.transform.rotation, this.transform);

				newCandy.name = string.Format("Candy[{0}][{1}]", x, y);

				var candyPrefab = prefabs[Random.Range(0, prefabs.Count)];
				Instantiate(candyPrefab, newCandy.GetComponent<Candy>().candyComponent.transform.position, currentCandy.transform.rotation, newCandy.transform);
				//newCandy.GetComponent<Candy>().candyComponent = candyPrefab;
				//candyPrefab.SetActive(true);

				_candies[x, y] = newCandy;
			}
		}
	}
}