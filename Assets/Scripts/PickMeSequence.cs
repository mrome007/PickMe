using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickMeSequence : MonoBehaviour 
{
	public UnitSelection UnitSelection;
	public PicksSelection PicksSelection;

	private int unitCount = 0;

	private void Start()
	{
		UnitSelection.SetSelectionMode(true);
		PicksSelection.PickWinner();

		unitCount = 0;
	}

	private bool CheckIfAllUnitsHavePicked()
	{
		return unitCount == UnitSelection.NumberOfUnits;
	}

	private void OnTriggerEnter(Collider other)
	{
		unitCount++;
		var allPicked = CheckIfAllUnitsHavePicked();

		if(allPicked)
		{
			
		}
	}
}
