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

	public float speed;
	public float hAngularSpeed;
	public float vAngularSpeed;
	public Boundary2D boundary2d;
	public Boundary3D boundary3d;
	
	bool is3D;
	
	void FixedUpdate()
	{
		float hMove = Input.GetAxis ("Horizontal");
		float vMove = Input.GetAxis ("Vertical");
		Vector3 movement = is3D ? new Vector3(hMove, vMove, 0.0f) : new Vector3(hMove, 0.0f, vMove);
		rigidbody.velocity = movement * speed;
		
		rigidbody.position = is3D ?
			new Vector3(
				Mathf.Clamp(rigidbody.position.x, boundary3d.xMin, boundary3d.xMax),
				Mathf.Clamp (rigidbody.position.y, boundary3d.yMin, boundary3d.yMax),
				0.0f
				) :
			new Vector3(
				Mathf.Clamp(rigidbody.position.x, boundary2d.xMin, boundary2d.xMax),
				0.0f,
				Mathf.Clamp (rigidbody.position.z, boundary2d.zMin, boundary2d.zMax)
			);
		
		rigidbody.rotation = Quaternion.Euler(rigidbody.velocity.y * -vAngularSpeed, 0.0f, rigidbody.velocity.x * -hAngularSpeed);
	}
	
	public void SetMode(bool _3d)
	{
		is3D = _3d;
		if (is3D)
			rigidbody.position = new Vector3(
				Mathf.Clamp(rigidbody.position.x, boundary3d.xMin, boundary3d.xMax),
				Mathf.Clamp (rigidbody.position.y, boundary3d.yMin, boundary3d.yMax),
				0.0f
				);
	}
}
