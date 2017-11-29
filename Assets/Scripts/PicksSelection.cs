using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicksSelection : MonoBehaviour 
{
	public List<Picks> Picks;
    public int NumberOfPicks { get; private set; }

	public void PickWinner()
	{
        NumberOfPicks = Random.Range(2, 5);
        RearrangePicks();
        EnablePickObjects(false);
		var winIndex = Random.Range(0, NumberOfPicks);
		for(int index = 0; index < NumberOfPicks; index++)
		{
            Picks[index].gameObject.SetActive(true);
			Picks[index].SetPickWin(false);
		}

		Picks[winIndex].SetPickWin(true);
	}

	public void InitializePicks()
	{
		for(int index = 0; index < NumberOfPicks; index++)
		{
			Picks[index].SetPickWin(false);
		}
	}

    private void EnablePickObjects(bool enable)
    {
        for(int index = 0; index < Picks.Count; index++)
        {
            Picks[index].gameObject.SetActive(false);
        }
    }

    private void RearrangePicks()
    {
        var width = GetPickWidth();

        for(int pickIndex = 0; pickIndex < Picks.Count; pickIndex++)
        {
            Picks[pickIndex].transform.localScale = new Vector3(width, 30f, 10f);
        }

        if(NumberOfPicks % 2 == 0)//even
        {
            var evenIndex = NumberOfPicks / 2;
            var evenPickCount = 0;
            var evenPos = width / 2f + 0.25f;
            for(int evenCount = 0; evenCount < evenIndex; evenCount++)
            {
                var pos = new Vector3(evenPos, Picks[evenPickCount].transform.localPosition.y, Picks[evenPickCount].transform.localPosition.z);
                Picks[evenPickCount++].transform.localPosition = pos;
                pos.x *= -1f;
                Picks[evenPickCount++].transform.localPosition = pos;
                evenPos += (width + 0.5f);
            }
        }
        else//odd
        {
            var oddIndex = NumberOfPicks / 2 + 1;
            bool first = true;
            var oddPickCount = 0;
            var oddPos = 0f;
            var posIncr = width + 0.5f;
            for(int oddCount = 0; oddCount < oddIndex; oddCount++)
            {
                var pos = new Vector3(oddPos, Picks[oddPickCount].transform.localPosition.y, Picks[oddPickCount].transform.localPosition.z);
                if(first)
                {
                    Picks[oddPickCount].transform.localPosition = pos;
                    first = false;
                    oddPickCount++;
                }
                else
                {
                    pos.x = oddPos;
                    Picks[oddPickCount++].transform.localPosition = pos;
                    pos.x *= -1f;
                    Picks[oddPickCount++].transform.localPosition = pos;
                }
                oddPos += posIncr;
            }
        }
    }

    private float GetPickWidth()
    {
        var result = 0f;
        switch(NumberOfPicks)
        {
            case 2:
                result = 60f;
                break;
            case 3:
                result = 40f;
                break;
            case 4:
                result = 25f;
                break;
            case 5:
                result = 20f;
                break;
            default:
                break;
        }

        return result;
    }
}
