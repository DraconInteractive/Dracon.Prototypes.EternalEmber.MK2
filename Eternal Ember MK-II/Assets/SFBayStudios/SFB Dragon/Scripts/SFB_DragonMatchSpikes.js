#pragma strict
@script ExecuteInEditMode()

private var thisMaterial	: ProceduralMaterial;
var bodyMaterial			: ProceduralMaterial;
var matchSFX				: boolean				= true;
var matchSpikesToBody		: boolean				= true;
private var rebuildTextures	: boolean				= false;

function Update () {
	if (!thisMaterial)
		thisMaterial	 	= GetComponent.<Renderer>().sharedMaterial as ProceduralMaterial;
	if (bodyMaterial && thisMaterial)
	{
		if (matchSpikesToBody)
			CheckSpikesToBody();
		if (matchSFX)
			CheckSFX();
	}
}

function CheckSpikesToBody(){
	if (thisMaterial.GetProceduralFloat("SpikesHue") != bodyMaterial.GetProceduralFloat("BodyHue"))
	{
		thisMaterial.SetProceduralFloat("SpikesHue", bodyMaterial.GetProceduralFloat("BodyHue"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("SpikesSaturation") != bodyMaterial.GetProceduralFloat("BodySaturation"))
	{
		thisMaterial.SetProceduralFloat("SpikesSaturation", bodyMaterial.GetProceduralFloat("BodySaturation"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("SpikesLightness") != bodyMaterial.GetProceduralFloat("BodyLightness"))
	{
		thisMaterial.SetProceduralFloat("SpikesLightness", bodyMaterial.GetProceduralFloat("BodyLightness"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("SpikesContrast") != bodyMaterial.GetProceduralFloat("BodyContrast"))
	{
		thisMaterial.SetProceduralFloat("SpikesContrast", bodyMaterial.GetProceduralFloat("BodyContrast"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("SpikesOverlayAO") != bodyMaterial.GetProceduralFloat("BodyOverlayAO"))
	{
		thisMaterial.SetProceduralFloat("SpikesOverlayAO", bodyMaterial.GetProceduralFloat("BodyOverlayAO"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("SpikesOverlayBlend") != bodyMaterial.GetProceduralFloat("BodyOverlayBlend"))
	{
		thisMaterial.SetProceduralFloat("SpikesOverlayBlend", bodyMaterial.GetProceduralFloat("BodyOverlayBlend"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("SpikesRoughnessShift") != bodyMaterial.GetProceduralFloat("BodyRoughnessShift"))
	{
		thisMaterial.SetProceduralFloat("SpikesRoughnessShift", bodyMaterial.GetProceduralFloat("BodyRoughnessShift"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralColor("SpikesOverlayColor") != bodyMaterial.GetProceduralColor("BodyOverlayColor"))
	{
		thisMaterial.SetProceduralColor("SpikesOverlayColor", bodyMaterial.GetProceduralColor("BodyOverlayColor"));
		rebuildTextures		= true;
	}
}



function CheckSFX(){
	if (thisMaterial.GetProceduralFloat("GroundDirtHeight") != bodyMaterial.GetProceduralFloat("GroundDirtHeight"))
	{
		thisMaterial.SetProceduralFloat("GroundDirtHeight", bodyMaterial.GetProceduralFloat("GroundDirtHeight"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("GroundDirtLevel") != bodyMaterial.GetProceduralFloat("GroundDirtLevel"))
	{
		thisMaterial.SetProceduralFloat("GroundDirtLevel", bodyMaterial.GetProceduralFloat("GroundDirtLevel"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("GroundDirtContrast") != bodyMaterial.GetProceduralFloat("GroundDirtContrast"))
	{
		thisMaterial.SetProceduralFloat("GroundDirtContrast", bodyMaterial.GetProceduralFloat("GroundDirtContrast"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("GroundDirtRoughness") != bodyMaterial.GetProceduralFloat("GroundDirtRoughness"))
	{
		thisMaterial.SetProceduralFloat("GroundDirtRoughness", bodyMaterial.GetProceduralFloat("GroundDirtRoughness"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralColor("GroundDirtColor") != bodyMaterial.GetProceduralColor("GroundDirtColor"))
	{
		thisMaterial.SetProceduralColor("GroundDirtColor", bodyMaterial.GetProceduralColor("GroundDirtColor"));
		rebuildTextures		= true;
	}
	
	if (thisMaterial.GetProceduralFloat("DirtAmount") != bodyMaterial.GetProceduralFloat("DirtAmount"))
	{
		thisMaterial.SetProceduralFloat("DirtAmount", bodyMaterial.GetProceduralFloat("DirtAmount"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("DirtGrunge") != bodyMaterial.GetProceduralFloat("DirtGrunge"))
	{
		thisMaterial.SetProceduralFloat("DirtGrunge", bodyMaterial.GetProceduralFloat("DirtGrunge"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("DirtContrast") != bodyMaterial.GetProceduralFloat("DirtContrast"))
	{
		thisMaterial.SetProceduralFloat("DirtContrast", bodyMaterial.GetProceduralFloat("DirtContrast"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("DirtRoughness") != bodyMaterial.GetProceduralFloat("DirtRoughness"))
	{
		thisMaterial.SetProceduralFloat("DirtRoughness", bodyMaterial.GetProceduralFloat("DirtRoughness"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralColor("DirtColor") != bodyMaterial.GetProceduralColor("DirtColor"))
	{
		thisMaterial.SetProceduralColor("DirtColor", bodyMaterial.GetProceduralColor("DirtColor"));
		rebuildTextures		= true;
	}
	
	if (thisMaterial.GetProceduralFloat("GrungeAmount") != bodyMaterial.GetProceduralFloat("GrungeAmount"))
	{
		thisMaterial.SetProceduralFloat("GrungeAmount", bodyMaterial.GetProceduralFloat("GrungeAmount"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("GrungeContrast") != bodyMaterial.GetProceduralFloat("GrungeContrast"))
	{
		thisMaterial.SetProceduralFloat("GrungeContrast", bodyMaterial.GetProceduralFloat("GrungeContrast"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("GrungeRoughness") != bodyMaterial.GetProceduralFloat("GrungeRoughness"))
	{
		thisMaterial.SetProceduralFloat("GrungeRoughness", bodyMaterial.GetProceduralFloat("GrungeRoughness"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralColor("GrungeColor") != bodyMaterial.GetProceduralColor("GrungeColor"))
	{
		thisMaterial.SetProceduralColor("GrungeColor", bodyMaterial.GetProceduralColor("GrungeColor"));
		rebuildTextures		= true;
	}
	
	if (thisMaterial.GetProceduralFloat("SFXWaterLevel") != bodyMaterial.GetProceduralFloat("SFXWaterLevel"))
	{
		thisMaterial.SetProceduralFloat("SFXWaterLevel", bodyMaterial.GetProceduralFloat("SFXWaterLevel"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("SFXIceLevel") != bodyMaterial.GetProceduralFloat("SFXIceLevel"))
	{
		thisMaterial.SetProceduralFloat("SFXIceLevel", bodyMaterial.GetProceduralFloat("SFXIceLevel"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("SFXIceDetails") != bodyMaterial.GetProceduralFloat("SFXIceDetails"))
	{
		thisMaterial.SetProceduralFloat("SFXIceDetails", bodyMaterial.GetProceduralFloat("SFXIceDetails"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("SFXSnow") != bodyMaterial.GetProceduralFloat("SFXSnow"))
	{
		thisMaterial.SetProceduralFloat("SFXSnow", bodyMaterial.GetProceduralFloat("SFXSnow"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("SFXMoss") != bodyMaterial.GetProceduralFloat("SFXMoss"))
	{
		thisMaterial.SetProceduralFloat("SFXMoss", bodyMaterial.GetProceduralFloat("SFXMoss"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("SFXMossScale") != bodyMaterial.GetProceduralFloat("SFXMossScale"))
	{
		thisMaterial.SetProceduralFloat("SFXMossScale", bodyMaterial.GetProceduralFloat("SFXMossScale"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralColor("SFXMossColor") != bodyMaterial.GetProceduralColor("SFXMossColor"))
	{
		thisMaterial.SetProceduralColor("SFXMossColor", bodyMaterial.GetProceduralColor("SFXMossColor"));
		rebuildTextures		= true;
	}
	
}

function LateUpdate(){
	if (rebuildTextures)
		RebuildTextures();
}

function RebuildTextures(){
	thisMaterial.RebuildTextures();
	rebuildTextures		= false;
}