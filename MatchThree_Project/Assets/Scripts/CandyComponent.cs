using System.Collections;
using UnityEngine;

public class CandyComponent : MonoBehaviour
{
	public Animator anim;
	[SerializeField] private Candy _candyRef;


	private void Start()
	{
		_candyRef = GetComponentInParent<Candy>();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.V))
		{
			StartCoroutine(DestroyCandies());
		}
		
		if (_candyRef.isSelected) //TODO: Animations for candies being selected
		{
			anim.SetBool("Selected", true);
			//Debug.Log(gameObject.transform.parent.name+ " Selected");
		}
		else
		{
			anim.SetBool("Selected", false);
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