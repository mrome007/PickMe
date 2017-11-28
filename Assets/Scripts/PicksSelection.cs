using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicksSelection : MonoBehaviour 
{
	public List<Picks> Picks;

	public void PickWinner()
	{
		var winIndex = Random.Range(0, Picks.Count);
		for(int index = 0; index < Picks.Count; index++)
		{
			Picks[index].SetPickWin(false);
		}

		Picks[winIndex].SetPickWin(true);
	}

	public void InitializePicks()
	{
		for(int index = 0; index < Picks.Count; index++)
		{
			Picks[index].SetPickWin(false);
		}
	}
}
