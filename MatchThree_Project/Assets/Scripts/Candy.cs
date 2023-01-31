using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Candy : MonoBehaviour
{
	private static Color _selectedColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
	private static Candy _previousSelected = null;

	[SerializeField] private SpriteRenderer _spriteR;
	[SerializeField] private bool _isSelected;

	public int id;


	private Vector2[] _adjacentDirections = new Vector2[] //cuatro direcciones
	{
		Vector2.up,
		Vector2.down,
		Vector2.left,
		Vector2.right
	};


	void Awake()
	{

	}


	void Update()
	{

	}
}
