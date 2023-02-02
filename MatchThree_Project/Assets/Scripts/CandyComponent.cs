
using UnityEngine;

public class CandyComponent : MonoBehaviour
{
	public Animator anim;
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.V))
		{
			anim.SetTrigger("Explode");
			Debug.Log("Explosion Animation activated");
		}
	}
}