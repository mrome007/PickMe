using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealUnits : MonoBehaviour 
{
	public event EventHandler ConcealEnded;
	public event EventHandler RevealEnded;
	public float RevealSpeed = 7.5f;

	public void StartReveal()
	{
		StartCoroutine(RevealTheUnits());
	}

	public void StartConceal()
	{
		StartCoroutine(ConcealTheUnits());
	}

	private IEnumerator RevealTheUnits()
	{
		var movementVector = transform.position;
		while(transform.position.y > -30.5f)
		{
			movementVector.y -= Time.deltaTime * RevealSpeed;
			transform.position = movementVector;
			yield return null;
		}
		movementVector.y = -31f;
		transform.position = movementVector;

		yield return new WaitForSeconds(1f);

		var handler = RevealEnded;
		if(handler != null)
		{
			handler(this, null);
		}
	}

	private IEnumerator ConcealTheUnits()
	{
		var movementVector = transform.position;
		while(transform.position.y < -0.5)
		{
			movementVector.y += Time.deltaTime * RevealSpeed;
			transform.position = movementVector;
			yield return null;
		}
		movementVector.y = 0f;
		transform.position = movementVector;

		var handler = ConcealEnded;
		if(handler != null)
		{
			handler(this, null);
		}
	}
}
