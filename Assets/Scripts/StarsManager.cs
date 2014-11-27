using UnityEngine;
using System.Collections;

public class StarsManager : MonoBehaviour {

	public GameObject star;
	
	void Start()
	{
		SpawnStars();
	}
	
	void SpawnStars()
	{
		Quaternion rotation = Quaternion.identity;
		Instantiate(star, new Vector3(-4.0f, 0.0f, 17.0f), rotation);
	}
}
