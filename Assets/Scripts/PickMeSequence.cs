using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickMeSequence : MonoBehaviour 
{
	public UnitSelection UnitSelection;
	public PicksSelection PicksSelection;
    public Transform CameraTransform;
	public RevealUnits RevealUnits;
	public MoveCamera MoveCamera;

	private int unitCount = 0;

	private void Start()
	{
        PicksSelection.PickWinner();

        RevealUnits.StartConceal();
        RevealUnits.ConcealEnded += HandleConcealEnded;
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

		RevealUnits.StartReveal();
		RevealUnits.RevealEnded += HandleRevealEnded;
    }

	private void HandleRevealEnded(object sender, EventArgs e)
	{
		RevealUnits.RevealEnded -= HandleRevealEnded;
		MoveForward();
		UnitSelection.RearrangeUnits();
		PicksSelection.PickWinner();

		RevealUnits.StartConceal();
		RevealUnits.ConcealEnded += HandleConcealEnded;
	}

	private void HandleConcealEnded(object sender, EventArgs e)
	{
		RevealUnits.ConcealEnded -= HandleConcealEnded;
		if(UnitSelection.NumberOfUnits <= 0)
		{
			StartCoroutine(DelayRestart());
		}
		else
		{
			UnitSelection.SetSelectionMode(true);

			unitCount = 0;
		}
	}

	private IEnumerator DelayRestart()
	{
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene(0);
	}

    private void MoveForward()
    {
        var newPicksPos = PicksSelection.transform.position;
        newPicksPos.z += 45f;
        PicksSelection.transform.position = newPicksPos;

		MoveCamera.MoveCameraForward(45f);

        var newPickMePos = transform.position;
        newPickMePos.z += 45f;
        transform.position = newPickMePos;
    }
}
