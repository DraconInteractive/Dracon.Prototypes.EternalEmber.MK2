#pragma strict

var magicSpell		: GameObject[];

function StartMagicSpell(){
	magicSpell[0].GetComponent(ParticleSystem).enableEmission = true;
	magicSpell[1].GetComponent(ParticleSystem).enableEmission = true;
}

function StopMagicSpell(){
	magicSpell[0].GetComponent(ParticleSystem).enableEmission = false;
	magicSpell[1].GetComponent(ParticleSystem).enableEmission = false;
}