using System.Collections;
using UnityEngine;

public class CandyComponent : MonoBehaviour
{
	public Animator anim;
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.V))
		{
			StartCoroutine(DestroyCandies());
		}
	}
	private IEnumerator DestroyCandies()
	{
		anim.SetTrigger("Explode");
		Debug.Log("Explosion Animation activated");
		yield return new WaitForSeconds(0.7f);
		Destroy(gameObject);
	}
}