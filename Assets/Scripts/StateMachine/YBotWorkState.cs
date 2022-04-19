using UnityEngine;

public class YBotWorkState : YBotBaseState
{
	public override void EnterState(YBotStateMachine yBot)
	{
		yBot.animator.SetBool(WorkingHash, true);	
	}

	public override void UpdateState(YBotStateMachine yBot)
	{
		if (yBot.activityDelayTime <= 5f)
			yBot.activityDelayTime += Time.deltaTime;
		else
		{
			yBot.transform.Rotate(Vector3.up, 180f);
			yBot.animator.SetBool(WorkingHash, false);
			yBot.animator.SetBool(WalkingHash,false);
			yBot.SwitchState(yBot.idleState);
		}
	}
}
