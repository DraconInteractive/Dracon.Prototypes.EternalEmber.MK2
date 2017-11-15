#pragma strict

var heads			: GameObject[];			// Head Objects
var reins			: GameObject[];			// Reins for each head
var bridles			: GameObject[];			// Bridle for each head

function Update () {
	// If the user wants to see the Reins/Bridle...
	if (GetComponent(UI.Toggle).isOn)
	{
		for (var i : int; i < heads.Length; i++){
			// If a head is active and the reins or bridle are not...
			if (heads[i].activeSelf && (!reins[i].activeSelf || !bridles[i].activeSelf))
			{
				// Activate them.
				reins[i].SetActive(true);
				bridles[i].SetActive(true);
			}
			else if (!heads[i].activeSelf)  // If a head is not active...
			{
				// Make sure these are deactivated...
				reins[i].SetActive(false);
				bridles[i].SetActive(false);
			}
		}
	}
}

function ToggleReins(isOn : boolean){
	if (!isOn)
	{
		for (var i : int; i < heads.Length; i++){
			reins[i].SetActive(false);
			bridles[i].SetActive(false);
		}
	}
}