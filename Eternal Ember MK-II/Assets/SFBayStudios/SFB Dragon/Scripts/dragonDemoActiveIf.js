#pragma strict

var targetToggles	: UI.Toggle[];					// Toggles that we are watching
var targetObject	: GameObject;					// Object we will turn on/off
var activeIf		: boolean		= true;			// Are we active if any of the targetToggles are?

function Update () {
	var togglesOn		: int	 = 0;
	for (var i : int; i < targetToggles.Length; i++){
		if (targetToggles[i].isOn)
			togglesOn++;
	}
	if (togglesOn != 0 && activeIf)
		targetObject.SetActive(true);
	else if (togglesOn != 0 && !activeIf)
		targetObject.SetActive(false);
	else if (togglesOn == 0 && !activeIf)
		targetObject.SetActive(true);
	else if (togglesOn == 0 && activeIf)
		targetObject.SetActive(false);
}