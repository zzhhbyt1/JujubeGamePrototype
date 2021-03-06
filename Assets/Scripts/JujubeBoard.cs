﻿using System.Collections.Generic;
using UnityEngine;

public class JujubeBoard : MonoBehaviour {
	[SerializeField]
	private GameObject groupPrefab;

	[SerializeField] // Warning: can be changed in Unity Editor
	private int[] maxCountForGroups = { 3, 5, 7 }; // According to game basic rules

	private List<CanvasGroup> allCanvasGroups = new List<CanvasGroup>();
	private List<JujubeGroup> allJujubeGroups = new List<JujubeGroup>();

	internal void MakeMove(int groupIndex, int moveAmount) {
		allJujubeGroups[groupIndex].MoveSome(moveAmount);
	}

	internal List<int> GetJujubeCounts() {
		List<int> jujubeCounts = new List<int>();
		foreach (var childScript in allJujubeGroups) {
			jujubeCounts.Add(childScript.JujubeCount);
		}
		return jujubeCounts;
	}

	internal int[] MaxCountForGroups {
		get {
			return maxCountForGroups;
		}
	}

	internal void SetAllGroupsInteractable(bool value) {
		foreach (CanvasGroup canvasGroup in allCanvasGroups) {
			canvasGroup.interactable = value;
		}
	}

	internal bool CheckGameOver() {
		foreach (var childScript in allJujubeGroups) {
			if (!childScript.IsEmpty) {
				return false;
			}
		}
		return true;
	}

	internal void OnPlayerTurnEnter() {
		SetAllGroupsInteractable(true);
		foreach (var childScript in allJujubeGroups) {
			childScript.OnNewTurn();
		}
	}

	internal void OnPlayerTurnExit() {
		SetAllGroupsInteractable(false);
		foreach (var childScript in allJujubeGroups) {
			childScript.OnEndTurn();
		}
	}

	internal void MakeOtherGroupsNotInteractable(CanvasGroup theRemainingGroup) {
		foreach (CanvasGroup canvasGroup in allCanvasGroups) {
			if (canvasGroup != theRemainingGroup) {
				canvasGroup.interactable = false;
			}
		}
	}

	void Awake() {
		foreach (int max in maxCountForGroups) {
			GameObject child = Instantiate(groupPrefab, transform);
			allCanvasGroups.Add(child.GetComponent<CanvasGroup>());

			JujubeGroup childScript = child.GetComponent<JujubeGroup>();
			allJujubeGroups.Add(childScript);
			childScript.MaxForGroup = max;
		}
	}
}