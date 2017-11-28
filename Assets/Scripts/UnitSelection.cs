using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour 
{
	public List<Units> Units;

	private bool isSelecting = false;
	private bool clearedSelected = false;
	private Vector3 mousePosition1;

	private int pickLayerMask;
	private int unitLayerMask;
	private List<Units> selectedUnits;

	private void Start()
	{
		pickLayerMask = 1 << LayerMask.NameToLayer("Picks");
		unitLayerMask = 1 << LayerMask.NameToLayer("Units");

		selectedUnits = new List<Units>();
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			isSelecting = true;
			mousePosition1 = Input.mousePosition;

			SelectUnitsIndividually();
		}

		if(Input.GetMouseButtonUp(0))
		{
			if(isSelecting)
			{
				for(int index = 0; index < Units.Count; index++)
				{
					if(IsWithinSelectionBounds(Units[index].transform))
					{
						if(!clearedSelected)
						{
							selectedUnits.Clear();
							clearedSelected = true;
						}
						Debug.Log(Units[index].gameObject.name);
						selectedUnits.Add(Units[index]);
					}
				}
			}
			clearedSelected = false;
			isSelecting = false;
		}

		CheckPicks();
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

	private void CheckPicks()
	{
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Input.GetMouseButtonDown(0))
		{
			if(Physics.Raycast(ray, out hit, 100f, pickLayerMask))
			{
				Debug.Log(hit.collider.gameObject.name);
				if(selectedUnits.Count > 0)
				{
					foreach(var unit in selectedUnits)
					{
						unit.InitializeUnit(hit.transform.position);
					}
					selectedUnits.Clear();
				}
			}
		}
	}

	private void SelectUnitsIndividually()
	{
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, 100f, unitLayerMask))
		{
			Debug.Log(hit.collider.name);
			selectedUnits.Add(hit.collider.gameObject.GetComponent<Units>());
		}
	}
}
