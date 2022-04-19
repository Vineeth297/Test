using UnityEngine;

public abstract class YBotBaseState
{
	protected static readonly int WalkingHash = Animator.StringToHash("isWalking");
	protected static readonly int WorkingHash = Animator.StringToHash("isWorking");
	public abstract void EnterState(YBotStateMachine yBot);
	
	public abstract void UpdateState(YBotStateMachine yBot);
}
