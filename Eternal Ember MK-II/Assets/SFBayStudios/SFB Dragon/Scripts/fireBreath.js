#pragma strict

var magicSpell		: GameObject[];

function StartSpell(){
	for (var i : int; i < magicSpell.Length; i++){
		if (magicSpell[i].GetComponent(ParticleSystem))
			magicSpell[i].GetComponent(ParticleSystem).enableEmission 	= true;
		if (magicSpell[i].GetComponent(Light))
			magicSpell[i].GetComponent(Light).enabled					= true;
	}
}

function EndSpell(){
	for (var i : int; i < magicSpell.Length; i++){
		if (magicSpell[i].GetComponent(ParticleSystem))
			magicSpell[i].GetComponent(ParticleSystem).enableEmission 	= false;
		if (magicSpell[i].GetComponent(Light))
			magicSpell[i].GetComponent(Light).enabled					= false;
	}
}