using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour 
{
	public List<Transform> Units;

	private bool isSelecting = false;
	private Vector3 mousePosition1;

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			isSelecting = true;
			mousePosition1 = Input.mousePosition;
		}

		if(Input.GetMouseButtonUp(0))
		{
			if(isSelecting)
			{
				for(int index = 0; index < Units.Count; index++)
				{
					if(IsWithinSelectionBounds(Units[index]))
					{
						Debug.Log(Units[index].name);
					}
				}
			}
			isSelecting = false;
		}
	}

	private void OnGUI()
	{
		if(isSelecting)
		{
			var rect = RectUtility.GetScreenRect(mousePosition1, Input.mousePosition);
			RectUtility.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
			RectUtility.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
		}
	}

	private bool IsWithinSelectionBounds(Transform unitTransform)
	{
		if(!isSelecting)
		{
			return false;
		}

		var camera = Camera.main;
		var viewportBounds = RectUtility.GetViewportBounds(camera, mousePosition1, Input.mousePosition);

		return viewportBounds.Contains(camera.WorldToViewportPoint(unitTransform.position));
	}
}
