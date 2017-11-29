using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picks : MonoBehaviour 
{
	private Rigidbody rigidBody;
	private bool winner = false;

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
		if(rigidBody == null)
		{
			Debug.LogError("No Rigidbody");
		}
		winner = false;
	}

	public void SetPickWin(bool win)
	{
		winner = win;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(winner)
		{
			return;
		}

		if(other.tag == "Units")
		{
			Destroy(other.gameObject);
		}
	}
}
