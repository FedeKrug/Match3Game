using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Candy : MonoBehaviour
{
	private static Color _selectedColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
	private static Candy _previousSelected = null;

	public bool isSelected;

	public int id;



	private Vector2[] _adjacentDirections = new Vector2[] //cuatro direcciones
	{
		Vector2.up,
		Vector2.down,
		Vector2.left,
		Vector2.right
	};

	private void SelectCandy()
	{
		isSelected = true;
		_previousSelected = gameObject.GetComponent<Candy>();
	}
	private void DeselectCandy()
	{
		isSelected = false;
		_previousSelected = null;
	}
	private void OnMouseDown()
	{
		if (this.GetComponentInChildren<CandyComponent>() == null || BoardManager.instance.isShifting)
		{
			return;
		}
		if (isSelected)
		{
			DeselectCandy();
		}
		else
		{
			if (_previousSelected == null)
			{
				SelectCandy();
			}
			else
			{
				if (CanSwipe())
				{
					SwapCandies(_previousSelected);
					_previousSelected.DeselectCandy();

				}
				else
				{
					_previousSelected.DeselectCandy();
					SelectCandy();
				}
			}
		}


	}

	public void SwapCandies(Candy newCandy)
	{

		if (GetComponentInChildren<CandyComponent>().GetComponent<SpriteRenderer>() == newCandy.GetComponentInChildren<CandyComponent>().GetComponent<SpriteRenderer>()) return;


		var tempPos = newCandy.transform.position;
		newCandy.transform.position = this.transform.position;
		this.transform.position = tempPos;

		Debug.Log("Swap Candies");

		var tempId = newCandy.id;
		newCandy.id = this.id;
		this.id = tempId;

	}

	private GameObject GetNeighbor(Vector2 direction)
	{
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction);
		if (hit.collider != null)
		{
			return hit.collider.gameObject;
		}
		return null;

	}

	private List<GameObject> GetAllNeighbors()
	{
		List<GameObject> neighbors = new List<GameObject>();

		foreach (Vector2 direction in _adjacentDirections)
		{
			neighbors.Add(GetNeighbor(direction));
		}

		return neighbors;

	}

	private bool CanSwipe()
	{
		return GetAllNeighbors().Contains(_previousSelected.gameObject);
	}

	private List<GameObject> FindMatch(Vector2 direction)
	{
		List<GameObject> matchingCandies = new List<GameObject>();
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction);

	}

}
