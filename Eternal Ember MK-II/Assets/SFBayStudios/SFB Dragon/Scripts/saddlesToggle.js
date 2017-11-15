#pragma strict

var backParts		: GameObject[];			// Back Part Objects
var backPartsAlt	: GameObject[];			// Back Part Objects
var saddles			: GameObject[];			// Saddle for each head
var normalSaddle	: GameObject;
var carapaceSaddle	: GameObject;

var spikes			: GameObject[];
var fins			: GameObject[];
var carapace		: GameObject[];

//var allOff			: boolean			= true;

function Update () {
	// If the user wants to see the Saddle...
	if (GetComponent(UI.Toggle).isOn)
	{
		if (backParts[0].activeSelf || backPartsAlt[0].activeSelf)
		{
			normalSaddle.SetActive(false);
			carapaceSaddle.SetActive(true);
		}
		else
		{
			normalSaddle.SetActive(true);
			carapaceSaddle.SetActive(false);
		}
		
		// Spikes & Fins
		if (spikes[0].activeSelf)
		{
			spikes[0].SetActive(false);
			spikes[1].SetActive(true);
		}
		if (fins[0].activeSelf)
		{
			fins[0].SetActive(false);
			fins[1].SetActive(true);
		}
		if (carapace[0].activeSelf)
		{
			carapace[0].SetActive(false);
			carapace[1].SetActive(true);
		}
	}
	else
	{
		if (spikes[1].activeSelf)
		{
			spikes[1].SetActive(false);
			spikes[0].SetActive(true);
		}
		if (fins[1].activeSelf)
		{
			fins[1].SetActive(false);
			fins[0].SetActive(true);
		}
		if (carapace[1].activeSelf)
		{
			carapace[1].SetActive(false);
			carapace[0].SetActive(true);
		}
	}
}

function ToggleSaddles(isOn : boolean){
	if (!isOn)
	{
		for (var i : int; i < backParts.Length; i++){
			saddles[i].SetActive(false);
		}
		//normalSaddle.SetActive(false);
	}
}