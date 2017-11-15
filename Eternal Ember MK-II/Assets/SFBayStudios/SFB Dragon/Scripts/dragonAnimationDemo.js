#pragma strict

var charAnimator		: Animator;								// Animator component.
var isFlying			: boolean			= false;			// We start out in fly idle -- wings must flap to start flying.
var glidePatternLoops	: int				= 10;				// How many seconds in the entire loop
var glidePatternStep	: int				= 0;				// What step are we on.  There's likely a better way to do this using time since start or something.
var stepsForFlapping	: int				= 3;				// Number of seconds for flapping.
function Start () {
	charAnimator		= GetComponent(Animator);				// Cache the value
}

function Update () {
	KeyCommands();												// Check for key presses
}

// This funciton will control things via keyboard to make it easy.
function KeyCommands(){
	if (Input.GetKeyDown("b"))
		HeadBreak();
	if (Input.GetKeyDown("f") && !isFlying)
		StartFlying();
	else if (Input.GetKeyDown("f") && isFlying)					// Needs else or both will be called
		StopFlying();
	if (Input.GetKeyDown("l"))
		LookBack();
	if (Input.GetKeyDown("space"))
		StartFlyingFast();
	if (Input.GetKeyUp("space"))
		StopFlyingFast();
	if (Input.GetKeyDown("d"))
		StartDiving();
	if (Input.GetKeyUp("d"))
		StopDiving();
}

function GlidePattern(){
	glidePatternStep += 1;
	if (glidePatternStep 	> glidePatternLoops)
	{
		glidePatternStep	= 0;
		StartGliding();
	}
	if (glidePatternStep	== glidePatternLoops - stepsForFlapping)
		StopGliding();
}

function StartFlyingFast(){
	StopGliding();		
	charAnimator.SetTrigger("startFlyingFast");
}

function StopFlyingFast(){
	charAnimator.SetTrigger("stopFlyingFast");
	ResetGlidePattern();
}

function StartDiving(){
	StopGliding();		
	charAnimator.SetTrigger("startDiving");
}

function StopDiving(){
	charAnimator.SetTrigger("stopDiving");
	ResetGlidePattern();
}

function StartGliding(){
	charAnimator.SetBool("gliding", true);
}

function StopGliding(){
	charAnimator.SetBool("gliding", false);
}

// Starts the IdleBreak animation for the head
function HeadBreak(){
	charAnimator.SetTrigger("headBreak");
}

// Call this when any other flight pattern ends, will start flapping, and resume gliding.
function ResetGlidePattern(){
	glidePatternStep		= glidePatternLoops - stepsForFlapping;				// Make sure we flap @ the start
	InvokeRepeating("GlidePattern", 1.0, 1.0);									// Repeat this in one second every second
}

// Starts the dragon flying
function StartFlying(){
	isFlying		= true;
	charAnimator.SetTrigger("startFlying");
	ResetGlidePattern();
}

// Stops flying, into fly idle
function StopFlying(){
	if (IsInvoking("GlidePattern"))
		CancelInvoke("GlidePattern");
	isFlying		= false;
	charAnimator.SetTrigger("stopFlying");
	StopGliding();
}

// Gets the Dragon to look back random direction.  Will turn off automatically after set time.
function LookBack(){
	var lookBackDir			= Random.Range(0,2);		// the end number is never chosen, so this will be 1 or 0
	if (lookBackDir			== 0)						// 0 = left
		charAnimator.SetTrigger("lookBackLeft");
	else if (lookBackDir	== 1)						// 1 = right
		charAnimator.SetTrigger("lookBackRight");
	StopLookBack(lookBackDir, 5.0);						// Call this, delay is 5.0 seconds
}

// Stops the look back, after a delay
function StopLookBack(lookBackDir : int, delay : float){
	yield WaitForSeconds(delay);						// Delay
	if (lookBackDir			== 0)						// 0 = left
		charAnimator.SetTrigger("stopBackLeft");
	if (lookBackDir			== 1)						// 1 = right
		charAnimator.SetTrigger("stopBackRight");
}