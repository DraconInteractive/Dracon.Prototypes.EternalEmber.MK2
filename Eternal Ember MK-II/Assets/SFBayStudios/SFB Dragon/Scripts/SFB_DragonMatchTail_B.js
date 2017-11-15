#pragma strict
@script ExecuteInEditMode()

private var thisMaterial				: ProceduralMaterial;
var bodyMaterial				: ProceduralMaterial;
var matchBody					: boolean				= true;
var matchUnderside				: boolean				= true;
var matchSFX					: boolean				= true;
var matchHighlightToBody		: boolean				= true;
var matchTailSpikeToBody		: boolean				= true;
private var rebuildTextures		: boolean				= false;

function Update () {
	if (!thisMaterial)
		thisMaterial	 	= GetComponent.<Renderer>().sharedMaterial as ProceduralMaterial;
	if (bodyMaterial && thisMaterial)
	{
		if (matchBody)
			CheckBody();
		if (matchUnderside)
			CheckUnderside();
		if (matchSFX)
			CheckSFX();
		if (matchHighlightToBody)
			CheckHighlightToBody();
		if (matchTailSpikeToBody)
			CheckTailSpikeToBody();
	}
}

function CheckHighlightToBody(){
	if (thisMaterial.GetProceduralFloat("HighlightHue") != bodyMaterial.GetProceduralFloat("BodyHue"))
	{
		thisMaterial.SetProceduralFloat("HighlightHue", bodyMaterial.GetProceduralFloat("BodyHue"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("HighlightSaturation") != bodyMaterial.GetProceduralFloat("BodySaturation"))
	{
		thisMaterial.SetProceduralFloat("HighlightSaturation", bodyMaterial.GetProceduralFloat("BodySaturation"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("HighlightLightness") != bodyMaterial.GetProceduralFloat("BodyLightness"))
	{
		thisMaterial.SetProceduralFloat("HighlightLightness", bodyMaterial.GetProceduralFloat("BodyLightness"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("HighlightContrast") != bodyMaterial.GetProceduralFloat("BodyContrast"))
	{
		thisMaterial.SetProceduralFloat("HighlightContrast", bodyMaterial.GetProceduralFloat("BodyContrast"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("HighlightOverlayAO") != bodyMaterial.GetProceduralFloat("BodyOverlayAO"))
	{
		thisMaterial.SetProceduralFloat("HighlightOverlayAO", bodyMaterial.GetProceduralFloat("BodyOverlayAO"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("HighlightOverlayBlend") != bodyMaterial.GetProceduralFloat("BodyOverlayBlend"))
	{
		thisMaterial.SetProceduralFloat("HighlightOverlayBlend", bodyMaterial.GetProceduralFloat("BodyOverlayBlend"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("HighlightRoughnessShift") != bodyMaterial.GetProceduralFloat("BodyRoughnessShift"))
	{
		thisMaterial.SetProceduralFloat("HighlightRoughnessShift", bodyMaterial.GetProceduralFloat("BodyRoughnessShift"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralColor("HighlightOverlayColor") != bodyMaterial.GetProceduralColor("BodyOverlayColor"))
	{
		thisMaterial.SetProceduralColor("HighlightOverlayColor", bodyMaterial.GetProceduralColor("BodyOverlayColor"));
		rebuildTextures		= true;
	}
}

function CheckTailSpikeToBody(){
	if (thisMaterial.GetProceduralFloat("TailSpikeHue") != bodyMaterial.GetProceduralFloat("BodyHue"))
	{
		thisMaterial.SetProceduralFloat("TailSpikeHue", bodyMaterial.GetProceduralFloat("BodyHue"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("TailSpikeSaturation") != bodyMaterial.GetProceduralFloat("BodySaturation"))
	{
		thisMaterial.SetProceduralFloat("TailSpikeSaturation", bodyMaterial.GetProceduralFloat("BodySaturation"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("TailSpikeLightness") != bodyMaterial.GetProceduralFloat("BodyLightness"))
	{
		thisMaterial.SetProceduralFloat("TailSpikeLightness", bodyMaterial.GetProceduralFloat("BodyLightness"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("TailSpikeContrast") != bodyMaterial.GetProceduralFloat("BodyContrast"))
	{
		thisMaterial.SetProceduralFloat("TailSpikeContrast", bodyMaterial.GetProceduralFloat("BodyContrast"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("TailSpikeOverlayAO") != bodyMaterial.GetProceduralFloat("BodyOverlayAO"))
	{
		thisMaterial.SetProceduralFloat("TailSpikeOverlayAO", bodyMaterial.GetProceduralFloat("BodyOverlayAO"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("TailSpikeOverlayBlend") != bodyMaterial.GetProceduralFloat("BodyOverlayBlend"))
	{
		thisMaterial.SetProceduralFloat("TailSpikeOverlayBlend", bodyMaterial.GetProceduralFloat("BodyOverlayBlend"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("TailSpikeRoughnessShift") != bodyMaterial.GetProceduralFloat("BodyRoughnessShift"))
	{
		thisMaterial.SetProceduralFloat("TailSpikeRoughnessShift", bodyMaterial.GetProceduralFloat("BodyRoughnessShift"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralColor("TailSpikeOverlayColor") != bodyMaterial.GetProceduralColor("BodyOverlayColor"))
	{
		thisMaterial.SetProceduralColor("TailSpikeOverlayColor", bodyMaterial.GetProceduralColor("BodyOverlayColor"));
		rebuildTextures		= true;
	}
}

function CheckBody(){
	if (thisMaterial.GetProceduralFloat("BodyHue") != bodyMaterial.GetProceduralFloat("BodyHue"))
	{
		thisMaterial.SetProceduralFloat("BodyHue", bodyMaterial.GetProceduralFloat("BodyHue"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("BodySaturation") != bodyMaterial.GetProceduralFloat("BodySaturation"))
	{
		thisMaterial.SetProceduralFloat("BodySaturation", bodyMaterial.GetProceduralFloat("BodySaturation"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("BodyLightness") != bodyMaterial.GetProceduralFloat("BodyLightness"))
	{
		thisMaterial.SetProceduralFloat("BodyLightness", bodyMaterial.GetProceduralFloat("BodyLightness"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("BodyContrast") != bodyMaterial.GetProceduralFloat("BodyContrast"))
	{
		thisMaterial.SetProceduralFloat("BodyContrast", bodyMaterial.GetProceduralFloat("BodyContrast"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("BodyOverlayAO") != bodyMaterial.GetProceduralFloat("BodyOverlayAO"))
	{
		thisMaterial.SetProceduralFloat("BodyOverlayAO", bodyMaterial.GetProceduralFloat("BodyOverlayAO"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("BodyOverlayBlend") != bodyMaterial.GetProceduralFloat("BodyOverlayBlend"))
	{
		thisMaterial.SetProceduralFloat("BodyOverlayBlend", bodyMaterial.GetProceduralFloat("BodyOverlayBlend"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("BodyRoughnessShift") != bodyMaterial.GetProceduralFloat("BodyRoughnessShift"))
	{
		thisMaterial.SetProceduralFloat("BodyRoughnessShift", bodyMaterial.GetProceduralFloat("BodyRoughnessShift"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralColor("BodyOverlayColor") != bodyMaterial.GetProceduralColor("BodyOverlayColor"))
	{
		thisMaterial.SetProceduralColor("BodyOverlayColor", bodyMaterial.GetProceduralColor("BodyOverlayColor"));
		rebuildTextures		= true;
	}
}

function CheckUnderside(){
	if (thisMaterial.GetProceduralFloat("UndersideHue") != bodyMaterial.GetProceduralFloat("UndersideHue"))
	{
		thisMaterial.SetProceduralFloat("UndersideHue", bodyMaterial.GetProceduralFloat("UndersideHue"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("UndersideSaturation") != bodyMaterial.GetProceduralFloat("UndersideSaturation"))
	{
		thisMaterial.SetProceduralFloat("UndersideSaturation", bodyMaterial.GetProceduralFloat("UndersideSaturation"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("UndersideLightness") != bodyMaterial.GetProceduralFloat("UndersideLightness"))
	{
		thisMaterial.SetProceduralFloat("UndersideLightness", bodyMaterial.GetProceduralFloat("UndersideLightness"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("UndersideContrast") != bodyMaterial.GetProceduralFloat("UndersideContrast"))
	{
		thisMaterial.SetProceduralFloat("UndersideContrast", bodyMaterial.GetProceduralFloat("UndersideContrast"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("UndersideOverlayAO") != bodyMaterial.GetProceduralFloat("UndersideOverlayAO"))
	{
		thisMaterial.SetProceduralFloat("UndersideOverlayAO", bodyMaterial.GetProceduralFloat("UndersideOverlayAO"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("UndersideOverlayBlend") != bodyMaterial.GetProceduralFloat("UndersideOverlayBlend"))
	{
		thisMaterial.SetProceduralFloat("UndersideOverlayBlend", bodyMaterial.GetProceduralFloat("UndersideOverlayBlend"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralFloat("UndersideRoughnessShift") != bodyMaterial.GetProceduralFloat("UndersideRoughnessShift"))
	{
		thisMaterial.SetProceduralFloat("UndersideRoughnessShift", bodyMaterial.GetProceduralFloat("UndersideRoughnessShift"));
		rebuildTextures		= true;
	}
	if (thisMaterial.GetProceduralColor("UndersideOverlayColor") != bodyMaterial.GetProceduralColor("UndersideOverlayColor"))
	{
		thisMaterial.SetProceduralColor("UndersideOverlayColor", bodyMaterial.GetProceduralColor("UndersideOverlayColor"));
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