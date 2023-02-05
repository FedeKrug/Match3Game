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
				SwapCandies(_previousSelected);
				_previousSelected.DeselectCandy();
				//SelectCandy();
			}
		}

		
	}

	public void SwapCandies(Candy newCandy)
	{

		if (GetComponentInChildren<CandyComponent>().GetComponent<SpriteRenderer>()  == newCandy.GetComponentInChildren<CandyComponent>().GetComponent<SpriteRenderer>()) return;

		
		var tempPos = newCandy.transform.position;
		newCandy.transform.position = this.transform.position;
		this.transform.position = tempPos;

		Debug.Log("Swap Candies");

		var tempId = newCandy.id;
		newCandy.id = this.id;
		this.id = tempId;

	}
}
