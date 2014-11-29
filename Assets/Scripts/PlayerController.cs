using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary2D
{
	public float xMin, xMax, zMin, zMax;
}

[System.Serializable]
public class Boundary3D
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

	public float _speed;
	public float _3Dspeed;
	public float _hAngularSpeed;
	public float _vAngularSpeed;
	public Boundary2D _boundary2d;
	public Boundary3D _boundary3d;
	
	bool is3D;
	
	void FixedUpdate()
	{
		float hMove = Input.GetAxis ("Horizontal");
		float vMove = Input.GetAxis ("Vertical");
		Vector3 movement = is3D ? new Vector3(hMove, vMove, 0.0f) : new Vector3(hMove, 0.0f, vMove);
		rigidbody.velocity = movement * _speed;
		
		rigidbody.position = is3D ?
			new Vector3(
				Mathf.Clamp(rigidbody.position.x, _boundary3d.xMin, _boundary3d.xMax),
				Mathf.Clamp (rigidbody.position.y, _boundary3d.yMin, _boundary3d.yMax),
				0.0f
				) :
			new Vector3(
				Mathf.Clamp(rigidbody.position.x, _boundary2d.xMin, _boundary2d.xMax),
				0.0f,
				0.0f//Mathf.Clamp (rigidbody.position.z, boundary2d.zMin, boundary2d.zMax)
			);
		
		rigidbody.rotation = Quaternion.Euler(rigidbody.velocity.y * -_vAngularSpeed, 0.0f, rigidbody.velocity.x * -_hAngularSpeed);
	}
	
	public void SetMode(bool _3d)
	{
		is3D = _3d;
		if (is3D)
		{
			rigidbody.position = new Vector3(
				Mathf.Clamp(rigidbody.position.x, _boundary3d.xMin, _boundary3d.xMax),
				Mathf.Clamp (rigidbody.position.y, _boundary3d.yMin, _boundary3d.yMax),
				0.0f
				);
			_speed = _3Dspeed;
		}
	}
}
