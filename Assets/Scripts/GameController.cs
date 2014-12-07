using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public Camera Camera2D;
	public Camera Camera3D;
	public PlayerController Player;
	
	void Start()
	{
		Select2D ();
	}
	
	void Update()
	{
		if (Time.realtimeSinceStartup > 4.0f && Time.realtimeSinceStartup < 4.1f)
			Select3D ();
	}
	
	void Select3D()
	{
		Camera2D.animation.Play();
		Player.SetMode(true);
	}
	
	void Select2D()
	{
		Camera2D.camera.enabled = true;
		Camera3D.camera.enabled = false;
		Player.SetMode(false);
	}
}
