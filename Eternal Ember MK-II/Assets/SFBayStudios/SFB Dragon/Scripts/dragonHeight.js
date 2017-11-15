#pragma strict

var isFlying			: boolean			= false;
var flyingHeight		: float				= 50.0;
var groundHeight		: float				= 0.0;
var minFlyHeight		: float				= 10.0;
var animator			: Animator;
var animatorHash		: int[];
var isDying				: boolean			= false;
var currentHash			: int;
var normalizedTime		: float;
var deathGroundTime		: float				= 0.8;

function Start(){
	animator		= GetComponent.<Animator>();
	animatorHash[0]	= animator.StringToHash("Base Layer.Air.FlyDeath01");
	animatorHash[1]	= animator.StringToHash("Base Layer.Air.FlyDeath02");
	animatorHash[2]	= animator.StringToHash("Base Layer.Air.FlyDeath03");
}

function Update(){
	var animatorStateInfo 	= animator.GetCurrentAnimatorStateInfo(0);
	normalizedTime			= animatorStateInfo.normalizedTime;
	currentHash	= animatorStateInfo.fullPathHash;
	if (currentHash != animatorHash[0] && currentHash != animatorHash[1] && currentHash != animatorHash[2] && currentHash != animatorHash[3])
	{
		isDying	= false;
		if (isFlying)
		{
			if (transform.position.y < minFlyHeight)
				transform.position.y = flyingHeight;
		}
	}
	else
	{
		isDying = true;
		if (currentHash == animatorHash[0] || currentHash == animatorHash[1])
		{
			if (transform.position.y < minFlyHeight)
				animator.SetTrigger("flyDeathEnd");
		}
	}

	if (isDying && currentHash == animatorHash[2] && normalizedTime >= deathGroundTime)
	{
		transform.position.y = 0;
		isDying	= false;
	}
}

function StartFlying(){
	isFlying				= true;
	transform.position.y	= flyingHeight;
	transform.position.x	= 0;
	transform.position.z	= 0;
}

function StartGround(){
	isFlying				= false;
	transform.position.y	= groundHeight;
	transform.position.x	= 0;
	transform.position.z	= 0;
}