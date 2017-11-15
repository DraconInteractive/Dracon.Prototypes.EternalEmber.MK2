#pragma strict

var target			: GameObject;
var animControl		: Animator;

var isStatue		: boolean		= false;
var isWalking		: boolean		= false;

var statuePanel		: GameObject;
var groundPanel		: GameObject;

var walkingText		: GameObject;

var headLayerID		: int			= 1;			// Layer # of the head mask

var desHeadWeight	: float			= 0.0;

function Start () {
	animControl		= target.GetComponent(Animator);
}

function Update(){
	if (desHeadWeight != animControl.GetLayerWeight(headLayerID))
	{
		var newWeight	: float			= Mathf.Lerp(animControl.GetLayerWeight(headLayerID), desHeadWeight, Time.deltaTime * 2);
		animControl.SetLayerWeight(headLayerID, newWeight);
	}
}

function SwitchToFlying(){
	target.GetComponent(dragonHeight).StartFlying();
	animControl.SetTrigger("flyIdle");
}

function SwitchToGround(){
	target.GetComponent(dragonHeight).StartGround();
	animControl.SetTrigger("idle");
}

function Die(){
	animControl.SetTrigger("die");
}

function IdleBreak(){
	animControl.SetTrigger("idleBreak");
}

function GotHit1(){
	animControl.SetTrigger("gotHit1");
}

function GotHit2(){
	animControl.SetTrigger("gotHit2");
}

function Idle(){
	animControl.SetTrigger("idle");
}

function Walk(){
	animControl.SetTrigger("walk");
}

function WalkBackward(){
	animControl.SetTrigger("walkBackward");
}

function TailWhipLeft(){
	animControl.SetTrigger("tailWhipLeft");
}

function TailWhipRight(){
	animControl.SetTrigger("tailWhipRight");
}

function Run(){
	animControl.SetTrigger("run");
}

function BreathFire(){
	animControl.SetTrigger("breathFire");
}

function Attack1(){
	animControl.SetTrigger("attack1");
}

function Attack2(){
	animControl.SetTrigger("attack2");
}

function FlyIdle(){
	animControl.SetTrigger("flyIdle");
}

function FlyFast(){
	animControl.SetTrigger("flyFast");
}

function FlyAttack(){
	animControl.SetTrigger("flyAttack");
}

function Fly(){
	animControl.SetTrigger("fly");
}

function FlyBackward(){
	animControl.SetTrigger("flyBackward");
}

function FlyBreathFire(){
	animControl.SetTrigger("flyBreathFire");
}

function FlyDie(){
	animControl.SetTrigger("flyDie");
}

function FlyEndDie(){
	animControl.SetTrigger("flyEndDie");
}

function FlyDive(){
	animControl.SetTrigger("flyDive");
}

function FlyGlide(){
	animControl.SetTrigger("flyGlide");
}

function FlyGotHit(){
	animControl.SetTrigger("flyGotHit");
}

function HeadFire1(){
	SetHeadLayerWeight(1.0,0.0);
	animControl.SetTrigger("headFire1");
	InvokeRepeating("CheckEndOfClip", 0.5, 0.1);
}

function HeadFire2(){
	SetHeadLayerWeight(1.0,0.0);
	animControl.SetTrigger("headFire2");
	InvokeRepeating("CheckEndOfClip", 0.5, 0.1);
}

function LookBackLeft(){
	SetHeadLayerWeight(1.0,0.0);
	animControl.SetTrigger("lookBackLeft");
	InvokeRepeating("CheckEndOfClip", 0.5, 0.1);
}

function LookBackRight(){
	SetHeadLayerWeight(1.0,0.0);
	animControl.SetTrigger("lookBackRight");
	InvokeRepeating("CheckEndOfClip", 0.5, 0.1);
}

function EndLookBackLeft(){
	SetHeadLayerWeight(1.0,0.0);
	animControl.SetTrigger("endLookBackLeft");
	InvokeRepeating("CheckEndOfClip", 0.5, 0.1);
}

function EndLookBackRight(){
	SetHeadLayerWeight(1.0,0.0);
	animControl.SetTrigger("endLookBackRight");
	InvokeRepeating("CheckEndOfClip", 0.5, 0.1);
}

// This function, after a delay, will set the weight of headLayerID
function SetHeadLayerWeight(weight : float, delay : float){
	yield WaitForSeconds(delay);
	desHeadWeight = weight;
	//animControl.SetLayerWeight(headLayerID, weight);
}

function CheckEndOfClip(){
	if (animControl.GetCurrentAnimatorStateInfo(headLayerID).IsName("Head Layer.FlyIdle"))
	{
		desHeadWeight = 0.0;
		CancelInvoke("CheckEndOfClip");
	}
}


