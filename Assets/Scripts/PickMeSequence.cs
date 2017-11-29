using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickMeSequence : MonoBehaviour 
{
	public UnitSelection UnitSelection;
	public PicksSelection PicksSelection;
    public Transform CameraTransform;

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
            StartCoroutine(RevealSequence());
		}
	}

    private IEnumerator RevealSequence()
    {
        UnitSelection.SetSelectionMode(false);
        yield return new WaitForSeconds(1f);
        PicksSelection.InitializePicks();

        MoveForward();
        UnitSelection.RearrangeUnits();
        if(UnitSelection.NumberOfUnits <= 0)
        {
            Debug.Log("GAME OVER");
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(0);
        }
        else
        {
            UnitSelection.SetSelectionMode(true);
            PicksSelection.PickWinner();

            unitCount = 0;
        }
    }

    private void MoveForward()
    {
        var newPicksPos = PicksSelection.transform.position;
        newPicksPos.z += 45f;
        PicksSelection.transform.position = newPicksPos;

        var newCamPos = CameraTransform.position;
        newCamPos.z += 45f;
        CameraTransform.position = newCamPos;

        var newPickMePos = transform.position;
        newPickMePos.z += 45f;
        transform.position = newPickMePos;

        var newUnitsPos = UnitSelection.transform.position;
        newUnitsPos.z += 45f;
        UnitSelection.transform.position = newUnitsPos;
    }
}
