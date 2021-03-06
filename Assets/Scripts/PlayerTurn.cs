﻿using UnityEngine;

public class PlayerTurn : StateMachineBehaviour {
	internal JujubeBoard jujubeBoard;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		jujubeBoard.OnPlayerTurnEnter();
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		jujubeBoard.OnPlayerTurnExit();
	}
}