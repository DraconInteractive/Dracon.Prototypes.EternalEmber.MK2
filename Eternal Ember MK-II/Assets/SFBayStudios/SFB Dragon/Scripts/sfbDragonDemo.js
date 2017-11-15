#pragma strict

var animator	: Animator;

function Start () {
	animator	= GetComponent.<Animator>();
}

function UpdateLocomotion(value : float){
	animator.SetFloat("locomotion", value);
}