using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour 
{
	public float MoveSpeed = 10f;
	
	public void MoveCameraForward(float zPos)
	{
		StartCoroutine(MoveCameraForwardRoutine(zPos));
	}

	private IEnumerator MoveCameraForwardRoutine(float zPos)
	{
		var newZPos = transform.position.z + zPos;
		var movementVector = transform.position;
		while(transform.position.z < (newZPos - 0.5f))
		{
			movementVector.z += Time.deltaTime * MoveSpeed;
			transform.position = movementVector;
			yield return null;
		}
		movementVector.z = newZPos;
		transform.position = movementVector;
	}

}
