using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units : MonoBehaviour 
{
	[SerializeField]
	private float speed = 5f;

	private Vector3? target = null;

	public void SetUnitTarget(Vector3 tar)
	{
		tar.y = transform.position.y;
		target = tar;
		transform.LookAt(target.Value);
	}

	public void ClearUnit()
	{
		target = null;
	}

	private void Update()
	{
		if(target.HasValue)
		{
			var step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, target.Value, step);
			var distance = Vector3.Distance(transform.position, target.Value);
			if(distance <= 0.1f)
			{
				ClearUnit();
			}
		}
	}
}
