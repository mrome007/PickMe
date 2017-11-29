using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnitSelection : MonoBehaviour 
{
	public List<Units> Units;

	private bool isSelecting = false;
	private bool clearedSelected = false;
	private Vector3 mousePosition1;

	private int pickLayerMask;
	private int unitLayerMask;
	private List<Units> selectedUnits;

	private bool selectionMode = false;
	private int numTurns = 0;
	public int NumberOfUnits { get; private set; }

	public void SetSelectionMode(bool canSelect)
	{
		selectionMode = canSelect;
	}

	private void Start()
	{
		pickLayerMask = 1 << LayerMask.NameToLayer("Picks");
		unitLayerMask = 1 << LayerMask.NameToLayer("Units");

		selectedUnits = new List<Units>();
		NumberOfUnits = Units.Count;
		numTurns = 1;
	}

	private void Update()
	{
		if(!selectionMode)
		{
			return;
		}

		if(Input.GetMouseButtonDown(0))
		{
			isSelecting = true;
			mousePosition1 = Input.mousePosition;

			//TODO: make a better way to select individually.
			SelectUnitsIndividually();
		}

		if(Input.GetMouseButtonUp(0))
		{
			if(isSelecting)
			{
				for(int index = 0; index < Units.Count; index++)
				{
					var unit = Units[index];
					if(unit == null)
					{
						continue;
					}

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
				Debug.Log(selectedUnits.Count);
			}
			clearedSelected = false;
			isSelecting = false;
		}

		CheckPicks();
	}

	private void OnGUI()
	{
		if(!selectionMode)
		{
			return;
		}
		
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
				if(selectedUnits.Count > 0)
				{
					foreach(var unit in selectedUnits)
					{
						var xPos = Random.Range(hit.transform.position.x - 3f, hit.transform.position.x + 3f);
						var zPos = Random.Range(hit.transform.position.z - 3f, hit.transform.position.z + 3f);
						var position = new Vector3(xPos, 1f, zPos);
						unit.SetUnitTarget(position);
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
			selectedUnits.Add(hit.collider.gameObject.GetComponent<Units>());
			Debug.Log(selectedUnits.Count);
		}
	}

	public void RearrangeUnits()
	{
		Units = Units.Where(unit => unit != null).ToList();
		NumberOfUnits = Units.Count;

        for(int index = 0; index < NumberOfUnits; index++)
        {
			var newPos = new Vector3(Random.Range(-8f, 8), 1f, Random.Range(-1f, 10f) + 45f * numTurns);
			Units[index].SetUnitTarget(newPos);
        }
		numTurns++;
	}
}
