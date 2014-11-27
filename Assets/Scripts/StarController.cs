using UnityEngine;
using System.Collections;

public class StarController : MonoBehaviour {

	public float speed = 12.0f;
	
	void Update()
	{
		Vector3 movement = new Vector3(0.0f, 0.0f, speed);
		transform.position -= movement * Time.deltaTime;
	}
}
