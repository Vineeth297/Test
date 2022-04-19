using UnityEngine;

public class YBotIdleState : YBotBaseState
{
	public override void EnterState(YBotStateMachine yBot)
	{
		Debug.Log("Character is in Idle State");
		yBot.activityDelayTime = 0f;
	}

	public override void UpdateState(YBotStateMachine yBot)
	{
		if(yBot.activityDelayTime <= 2f)
			yBot.activityDelayTime += Time.deltaTime;
		else
			yBot.SwitchState(yBot.walkState);
	}
}
