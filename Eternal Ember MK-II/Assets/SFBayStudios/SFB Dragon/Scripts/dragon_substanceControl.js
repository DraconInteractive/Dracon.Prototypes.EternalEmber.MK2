#pragma strict

var subPanels				: GameObject[];
var objectSaddle			: GameObject[];
var objectBody				: GameObject;
var objectHead				: GameObject[];
var objectTail				: GameObject[];
var objectSpikes			: GameObject;
var objectFins				: GameObject;
var objectCarapace			: GameObject;
var rendSaddle				: Renderer[];
var rendBody				: Renderer;
var rendHead				: Renderer[];
var rendTail				: Renderer[];
var rendSpikes				: Renderer;
var rendFins				: Renderer;
var rendCarapace			: Renderer;
var substanceSaddle			: ProceduralMaterial[];
var substanceBody			: ProceduralMaterial;
var substanceHead			: ProceduralMaterial[];
var substanceTail			: ProceduralMaterial[];
var substanceSpikes			: ProceduralMaterial;
var substanceFins			: ProceduralMaterial;
var substanceCarapace		: ProceduralMaterial;
var loadingPanel			: GameObject;

var blanketColor			: GameObject[];
var bodyColor				: GameObject[];
var undersideColor			: GameObject[];
var saddleColor				: GameObject[];
var spikesColor				: GameObject[];
var ridgesColor				: GameObject[];
var mouthColor				: GameObject[];
var clawsColor				: GameObject[];
var finsColor				: GameObject[];
var carapaceColor			: GameObject[];
var boneColor				: GameObject[];
var wingsColor				: GameObject[];
var groundDirtColor			: GameObject[];
var surfaceDirtColor		: GameObject[];

var colorDisplayBlanket		: GameObject;
var colorDisplayBody		: GameObject;
var colorDisplayUnderside	: GameObject;
var colorDisplaySaddle		: GameObject;
var colorDisplaySpikes		: GameObject;
var colorDisplayRidges		: GameObject;
var colorDisplayMouth		: GameObject;
var colorDisplayClaws		: GameObject;
var colorDisplayFins		: GameObject;
var colorDisplayCarapace	: GameObject;
var colorDisplayBone		: GameObject;
var colorDisplayWings		: GameObject;
var colorDisplayGroundDirt	: GameObject;
var colorDisplaySurfaceDirt	: GameObject;

var metalHead				: GameObject;
var metalCarapace			: GameObject;
var metalSpikes				: GameObject;
var metalUnderside			: GameObject;

var heads					: GameObject[];
var tails					: GameObject[];
var reins					: GameObject[];
var bridle					: GameObject[];
var saddle					: GameObject[];
var spikes					: GameObject[];
var fins					: GameObject[];
var carapace				: GameObject[];

var headsToggle				: GameObject[];
var tailsToggle				: GameObject[];
var backToggle				: GameObject[];

function Start() {
	for (var s : int; s < rendSaddle.Length; s++){
		rendSaddle[s] 		= objectSaddle[s].GetComponent.<Renderer>();
		substanceSaddle[s]	= rendSaddle[s].sharedMaterial as ProceduralMaterial;
	}
	for (var i : int; i < rendHead.Length; i++){
		rendHead[i] 		= objectHead[i].GetComponent.<Renderer>();
		substanceHead[i]	= rendHead[i].sharedMaterial as ProceduralMaterial;
	}
	for (var a : int; a < rendTail.Length; a++){
		rendTail[a] 		= objectTail[a].GetComponent.<Renderer>();
		substanceTail[a] 	= rendTail[a].sharedMaterial as ProceduralMaterial;
	}
	rendBody 			= objectBody.GetComponent.<Renderer>();
	rendSpikes 			= objectSpikes.GetComponent.<Renderer>();
	rendFins 			= objectFins.GetComponent.<Renderer>();
	rendCarapace 		= objectCarapace.GetComponent.<Renderer>();
	substanceBody 		= rendBody.sharedMaterial as ProceduralMaterial;
	substanceSpikes 	= rendSpikes.sharedMaterial as ProceduralMaterial;
	substanceFins 		= rendFins.sharedMaterial as ProceduralMaterial;
	substanceCarapace 	= rendCarapace.sharedMaterial as ProceduralMaterial;
	loadingPanel.SetActive(true);
	if (!IsInvoking("CheckLoading"))
		InvokeRepeating("CheckLoading", 0, 0.2);
}

function LoadSubPanel(panelNumber : int){
	for (var i : int; i < subPanels.Length; i++){
		subPanels[i].SetActive(false);
	}
	subPanels[panelNumber].SetActive(true);
}

function CloseSubPanels(){
	for (var i : int; i < subPanels.Length; i++){
		subPanels[i].SetActive(false);
	}
}

function CheckLoading(){
	//print ("CheckLoading()");
	if (!substanceSaddle[0].isProcessing && !substanceTail[0].isProcessing && !substanceTail[1].isProcessing && !substanceBody.isProcessing && !substanceHead[0].isProcessing && !substanceHead[1].isProcessing && !substanceHead[2].isProcessing && !substanceSpikes.isProcessing && !substanceFins.isProcessing && !substanceCarapace.isProcessing)
	{
		loadingPanel.SetActive(false);
		CancelInvoke("CheckLoading");
	}
}

function RebuildTexturesSaddle(saddleNumber : int){
	substanceSaddle[saddleNumber].RebuildTextures();
	loadingPanel.SetActive(true);
	InvokeRepeating("CheckLoading", 0, 0.1);
}

function RebuildTexturesBody(){
	substanceBody.RebuildTextures();
	loadingPanel.SetActive(true);
	InvokeRepeating("CheckLoading", 0, 0.1);
}

function RebuildTexturesHead(headNumber : int){
	substanceHead[headNumber].RebuildTextures();
	loadingPanel.SetActive(true);
	InvokeRepeating("CheckLoading", 0, 0.1);
}

function RebuildTexturesTail(tailNumber : int){
	substanceTail[tailNumber].RebuildTextures();
	loadingPanel.SetActive(true);
	InvokeRepeating("CheckLoading", 0, 0.1);
}

function RebuildTexturesSpikes(){
	substanceSpikes.RebuildTextures();
	loadingPanel.SetActive(true);
	InvokeRepeating("CheckLoading", 0, 0.1);
}

function RebuildTexturesFins(){
	substanceFins.RebuildTextures();
	loadingPanel.SetActive(true);
	InvokeRepeating("CheckLoading", 0, 0.1);
}

function RebuildTexturesCarapace(){
	substanceCarapace.RebuildTextures();
	loadingPanel.SetActive(true);
	InvokeRepeating("CheckLoading", 0, 0.1);
}


// BASE MATERIALS
/*
function MetalHead(newValue : boolean){
	substanceBody.SetProceduralBoolean("MetalHead", newValue);
	RebuildTextures();
}

function MetalCarapace(newValue : boolean){
	substanceBody.SetProceduralBoolean("MetalCarapace", newValue);
	RebuildTextures();
}

function MetalUnderside(newValue : boolean){
	substanceBody.SetProceduralBoolean("MetalUnderside", newValue);
	RebuildTextures();
}

function MetalSpikes(newValue : boolean){
	substanceBody.SetProceduralBoolean("MetalSpikes", newValue);
	RebuildTextures();
}
*/

function Saddle(newValue : boolean){
	saddle[0].SetActive(false);
	saddle[1].SetActive(false);
	if ((carapace[0].activeSelf || carapace[1].activeSelf) && newValue)
		saddle[1].SetActive(newValue);
	if ((!carapace[0].activeSelf && !carapace[1].activeSelf) && newValue)
		saddle[0].SetActive(newValue);
	if (newValue && spikes[0].activeSelf)
	{
		spikes[0].SetActive(false);
		spikes[1].SetActive(true);
	}
	if (!newValue && spikes[1].activeSelf)
	{
		spikes[0].SetActive(true);
		spikes[1].SetActive(false);
	}
	if (newValue && fins[0].activeSelf)
	{
		fins[0].SetActive(false);
		fins[1].SetActive(true);
	}
	if (!newValue && fins[1].activeSelf)
	{
		fins[0].SetActive(true);
		fins[1].SetActive(false);
	}
	if (newValue && carapace[0].activeSelf)
	{
		carapace[0].SetActive(false);
		carapace[1].SetActive(true);
	}
	if (!newValue && carapace[1].activeSelf)
	{
		carapace[0].SetActive(true);
		carapace[1].SetActive(false);
	}
}

function Reins(newValue : boolean){
	for (var i : int; i < reins.Length; i++){
		reins[i].SetActive(newValue);
		bridle[i].SetActive(newValue);
	}
}

function Head(headNumber : int){
	if (headsToggle[headNumber].GetComponent(UI.Toggle).isOn)
		heads[headNumber].SetActive(true);
	else
		heads[headNumber].SetActive(false);
}

function Tail(tailNumber : int){
	if (tailsToggle[tailNumber].GetComponent(UI.Toggle).isOn)
		tails[tailNumber].SetActive(true);
	else
		tails[tailNumber].SetActive(false);
}

function Spikes(newValue : boolean){
	print ("Spikes()");
	spikes[0].SetActive(false);
	spikes[1].SetActive(false);
	if (newValue && (saddle[0].activeSelf || saddle[1].activeSelf))
	{
		saddle[0].SetActive(true);
		saddle[1].SetActive(false);
		spikes[1].SetActive(newValue);
	}
	else
		spikes[0].SetActive(newValue);
}

function Fins(newValue : boolean){
	print ("Fins()");
	fins[0].SetActive(false);
	fins[1].SetActive(false);
	if (newValue && (saddle[0].activeSelf || saddle[1].activeSelf))
	{
		saddle[0].SetActive(true);
		saddle[1].SetActive(false);
		fins[1].SetActive(newValue);
	}
	else
		fins[0].SetActive(newValue);
}

function Carapace(newValue : boolean){
	print ("Carapace()");
	carapace[0].SetActive(false);
	carapace[1].SetActive(false);
	if (newValue && (saddle[0].activeSelf || saddle[1].activeSelf))
	{
		saddle[0].SetActive(false);
		saddle[1].SetActive(true);
		carapace[1].SetActive(newValue);
	}
	else
		carapace[0].SetActive(newValue);
	if (!newValue && saddle[1].activeSelf)
	{
		saddle[1].SetActive(false);
		saddle[0].SetActive(true);
	}
}

// BLANKET
function BlanketColor(){
	var newColor	= Color(parseFloat(blanketColor[0].GetComponent(UI.Slider).value),parseFloat(blanketColor[1].GetComponent(UI.Slider).value),parseFloat(blanketColor[2].GetComponent(UI.Slider).value), 1);
	colorDisplayBlanket.GetComponent(UI.Image).color	= newColor;
	for (var i : int; i < substanceSaddle.Length; i++){
		substanceSaddle[i].SetProceduralColor("BlanketOverlayColor", newColor);
		substanceSaddle[i].SetProceduralFloat("BlanketOverlayBlend", saddleColor[3].GetComponent(UI.Slider).value);
		RebuildTexturesSaddle(i);
	}
}

function BlanketHue(newValue : float){
	for (var i : int; i < substanceSaddle.Length; i++){
		substanceSaddle[i].SetProceduralFloat("BlanketHue", newValue);
		RebuildTexturesSaddle(i);
	}
}

function BlanketSaturation(newValue : float){
	for (var i : int; i < substanceSaddle.Length; i++){
		substanceSaddle[i].SetProceduralFloat("BlanketSaturation", newValue);
		RebuildTexturesSaddle(i);
	}
}

function BlanketLightness(newValue : float){
	for (var i : int; i < substanceSaddle.Length; i++){
		substanceSaddle[i].SetProceduralFloat("BlanketLightness", newValue);
		RebuildTexturesSaddle(i);
	}
}

function BlanketContrast(newValue : float){
	for (var i : int; i < substanceSaddle.Length; i++){
		substanceSaddle[i].SetProceduralFloat("BlanketContrast", newValue);
		RebuildTexturesSaddle(i);
	}
}

function BlanketRoughnessShift(newValue : float){
	for (var i : int; i < substanceSaddle.Length; i++){
		substanceSaddle[i].SetProceduralFloat("BlanketRoughnessShift", newValue);
		RebuildTexturesSaddle(i);
	}
}


// BODY
function BodyColor(){
	var newColor	= Color(parseFloat(bodyColor[0].GetComponent(UI.Slider).value),parseFloat(bodyColor[1].GetComponent(UI.Slider).value),parseFloat(bodyColor[2].GetComponent(UI.Slider).value), 1);
	substanceBody.SetProceduralColor("BodyOverlayColor", newColor);
	colorDisplayBody.GetComponent(UI.Image).color		= newColor;
	substanceBody.SetProceduralFloat("BodyOverlayBlend", bodyColor[3].GetComponent(UI.Slider).value);
	RebuildTexturesBody();
}

function BodyHue(newValue : float){
	substanceBody.SetProceduralFloat("BodyHue", newValue);
	RebuildTexturesBody();
}

function BodySaturation(newValue : float){
	substanceBody.SetProceduralFloat("BodySaturation", newValue);
	RebuildTexturesBody();
}

function BodyLightness(newValue : float){
	substanceBody.SetProceduralFloat("BodyLightness", newValue);
	RebuildTexturesBody();
}

function BodyContrast(newValue : float){
	substanceBody.SetProceduralFloat("BodyContrast", newValue);
	RebuildTexturesBody();
}

function BodyRoughnessShift(newValue : float){
	substanceBody.SetProceduralFloat("BodyRoughnessShift", newValue);
	RebuildTexturesBody();
}

// UNDERSIDE
function UndersideColor(){
	var newColor	= Color(parseFloat(undersideColor[0].GetComponent(UI.Slider).value),parseFloat(undersideColor[1].GetComponent(UI.Slider).value),parseFloat(undersideColor[2].GetComponent(UI.Slider).value), 1);
	substanceBody.SetProceduralColor("UndersideOverlayColor", newColor);
	colorDisplayUnderside.GetComponent(UI.Image).color		= newColor;
	substanceBody.SetProceduralFloat("UndersideOverlayBlend", undersideColor[3].GetComponent(UI.Slider).value);
	RebuildTexturesBody();
}

function UndersideHue(newValue : float){
	substanceBody.SetProceduralFloat("UndersideHue", newValue);
	RebuildTexturesBody();
}

function UndersideSaturation(newValue : float){
	substanceBody.SetProceduralFloat("UndersideSaturation", newValue);
	RebuildTexturesBody();
}

function UndersideLightness(newValue : float){
	substanceBody.SetProceduralFloat("UndersideLightness", newValue);
	RebuildTexturesBody();
}

function UndersideContrast(newValue : float){
	substanceBody.SetProceduralFloat("UndersideContrast", newValue);
	RebuildTexturesBody();
}

function UndersideRoughnessShift(newValue : float){
	substanceBody.SetProceduralFloat("UndersideRoughnessShift", newValue);
	RebuildTexturesBody();
}

// SADDLE
function SaddleColor(){
	var newColor	= Color(parseFloat(saddleColor[0].GetComponent(UI.Slider).value),parseFloat(saddleColor[1].GetComponent(UI.Slider).value),parseFloat(saddleColor[2].GetComponent(UI.Slider).value), 1);
	colorDisplaySaddle.GetComponent(UI.Image).color		= newColor;
	for (var i : int; i < substanceSaddle.Length; i++){
		substanceSaddle[i].SetProceduralColor("SaddleOverlayColor", newColor);
		substanceSaddle[i].SetProceduralFloat("SaddleOverlayBlend", saddleColor[3].GetComponent(UI.Slider).value);
		RebuildTexturesSaddle(i);
	}
}

function SaddleHue(newValue : float){
	for (var i : int; i < substanceSaddle.Length; i++){
		substanceSaddle[i].SetProceduralFloat("SaddleHue", newValue);
		RebuildTexturesSaddle(i);
	}
}

function SaddleSaturation(newValue : float){
	for (var i : int; i < substanceSaddle.Length; i++){
		substanceSaddle[i].SetProceduralFloat("SaddleSaturation", newValue);
		RebuildTexturesSaddle(i);
	}
}

function SaddleLightness(newValue : float){
	for (var i : int; i < substanceSaddle.Length; i++){
		substanceSaddle[i].SetProceduralFloat("SaddleLightness", newValue);
		RebuildTexturesSaddle(i);
	}
}

function SaddleContrast(newValue : float){
	for (var i : int; i < substanceSaddle.Length; i++){
		substanceSaddle[i].SetProceduralFloat("SaddleContrast", newValue);
		RebuildTexturesSaddle(i);
	}
}

function SaddleRoughnessShift(newValue : float){
	for (var i : int; i < substanceSaddle.Length; i++){
		substanceSaddle[i].SetProceduralFloat("SaddleRoughnessShift", newValue);
		RebuildTexturesSaddle(i);
	}
}

// SPIKES
function SpikesColor(){
	var newColor	= Color(parseFloat(spikesColor[0].GetComponent(UI.Slider).value),parseFloat(spikesColor[1].GetComponent(UI.Slider).value),parseFloat(spikesColor[2].GetComponent(UI.Slider).value), 1);
	substanceSpikes.SetProceduralColor("SpikesOverlayColor", newColor);
	colorDisplaySpikes.GetComponent(UI.Image).color		= newColor;
	substanceBody.SetProceduralFloat("SpikesOverlayBlend", spikesColor[3].GetComponent(UI.Slider).value);
	RebuildTexturesSpikes();
}

function SpikesHue(newValue : float){
	substanceSpikes.SetProceduralFloat("SpikesHue", newValue);
	RebuildTexturesSpikes();
}

function SpikesSaturation(newValue : float){
	substanceSpikes.SetProceduralFloat("SpikesSaturation", newValue);
	RebuildTexturesSpikes();
}

function SpikesLightness(newValue : float){
	substanceSpikes.SetProceduralFloat("SpikesLightness", newValue);
	RebuildTexturesSpikes();
}

function SpikesContrast(newValue : float){
	substanceSpikes.SetProceduralFloat("SpikesContrast", newValue);
	RebuildTexturesSpikes();
}

function SpikesRoughnessShift(newValue : float){
	substanceSpikes.SetProceduralFloat("SpikesRoughnessShift", newValue);
	RebuildTexturesSpikes();
}

// FINS
function FinsColor(){
	var newColor	= Color(parseFloat(finsColor[0].GetComponent(UI.Slider).value),parseFloat(finsColor[1].GetComponent(UI.Slider).value),parseFloat(finsColor[2].GetComponent(UI.Slider).value), 1);
	substanceFins.SetProceduralColor("FinsOverlayColor", newColor);
	colorDisplayFins.GetComponent(UI.Image).color		= newColor;
	substanceBody.SetProceduralFloat("FinsOverlayBlend", finsColor[3].GetComponent(UI.Slider).value);
	RebuildTexturesFins();
}

function FinsHue(newValue : float){
	substanceFins.SetProceduralFloat("FinsHue", newValue);
	RebuildTexturesFins();
}

function FinsSaturation(newValue : float){
	substanceFins.SetProceduralFloat("FinsSaturation", newValue);
	RebuildTexturesFins();
}

function FinsLightness(newValue : float){
	substanceFins.SetProceduralFloat("FinsLightness", newValue);
	RebuildTexturesFins();
}

function FinsContrast(newValue : float){
	substanceFins.SetProceduralFloat("FinsContrast", newValue);
	RebuildTexturesFins();
}

function FinsRoughnessShift(newValue : float){
	substanceFins.SetProceduralFloat("FinsRoughnessShift", newValue);
	RebuildTexturesFins();
}

// CARAPACE
function CarapaceColor(){
	var newColor	= Color(parseFloat(carapaceColor[0].GetComponent(UI.Slider).value),parseFloat(carapaceColor[1].GetComponent(UI.Slider).value),parseFloat(carapaceColor[2].GetComponent(UI.Slider).value), 1);
	substanceCarapace.SetProceduralColor("CarapaceOverlayColor", newColor);
	colorDisplayCarapace.GetComponent(UI.Image).color		= newColor;
	substanceBody.SetProceduralFloat("CarapaceOverlayBlend", carapaceColor[3].GetComponent(UI.Slider).value);
	RebuildTexturesCarapace();
}

function CarapaceHue(newValue : float){
	substanceCarapace.SetProceduralFloat("CarapaceHue", newValue);
	RebuildTexturesCarapace();
}

function CarapaceSaturation(newValue : float){
	substanceCarapace.SetProceduralFloat("CarapaceSaturation", newValue);
	RebuildTexturesCarapace();
}

function CarapaceLightness(newValue : float){
	substanceCarapace.SetProceduralFloat("CarapaceLightness", newValue);
	RebuildTexturesCarapace();
}

function CarapaceContrast(newValue : float){
	substanceCarapace.SetProceduralFloat("CarapaceContrast", newValue);
	RebuildTexturesCarapace();
}

function CarapaceRoughnessShift(newValue : float){
	substanceCarapace.SetProceduralFloat("CarapaceRoughnessShift", newValue);
	RebuildTexturesCarapace();
}

// WINGS
function WingsColor(){
	var newColor	= Color(parseFloat(wingsColor[0].GetComponent(UI.Slider).value),parseFloat(wingsColor[1].GetComponent(UI.Slider).value),parseFloat(wingsColor[2].GetComponent(UI.Slider).value), 1);
	substanceBody.SetProceduralColor("WingOverlayColor", newColor);
	colorDisplayWings.GetComponent(UI.Image).color		= newColor;
	substanceBody.SetProceduralFloat("WingOverlayBlend", wingsColor[3].GetComponent(UI.Slider).value);
	RebuildTexturesBody();
}

function WingsHue(newValue : float){
	substanceBody.SetProceduralFloat("WingHue", newValue);
	RebuildTexturesBody();
}

function WingsSaturation(newValue : float){
	substanceBody.SetProceduralFloat("WingSaturation", newValue);
	RebuildTexturesBody();
}

function WingsLightness(newValue : float){
	substanceBody.SetProceduralFloat("WingLightness", newValue);
	RebuildTexturesBody();
}

function WingsContrast(newValue : float){
	substanceBody.SetProceduralFloat("WingContrast", newValue);
	RebuildTexturesBody();
}

function WingsRoughnessShift(newValue : float){
	substanceBody.SetProceduralFloat("WingRoughnessShift", newValue);
	RebuildTexturesBody();
}

// RIDGES
function RidgesColor(){
	var newColor	= Color(parseFloat(ridgesColor[0].GetComponent(UI.Slider).value),parseFloat(ridgesColor[1].GetComponent(UI.Slider).value),parseFloat(ridgesColor[2].GetComponent(UI.Slider).value), 1);
	substanceHead[0].SetProceduralColor("RidgesOverlayColor", newColor);
	colorDisplayRidges.GetComponent(UI.Image).color		= newColor;
	substanceHead[0].SetProceduralFloat("RidgesOverlayBlend", ridgesColor[3].GetComponent(UI.Slider).value);
	RebuildTexturesHead(0);
}

function RidgesHue(newValue : float){
	substanceHead[0].SetProceduralFloat("RidgesHue", newValue);
	RebuildTexturesHead(0);
}

function RidgesSaturation(newValue : float){
	substanceHead[0].SetProceduralFloat("RidgesSaturation", newValue);
	RebuildTexturesHead(0);
}

function RidgesLightness(newValue : float){
	substanceHead[0].SetProceduralFloat("RidgesLightness", newValue);
	RebuildTexturesHead(0);
}

function RidgesContrast(newValue : float){
	substanceHead[0].SetProceduralFloat("RidgesContrast", newValue);
	RebuildTexturesHead(0);
}

function RidgesRoughnessShift(newValue : float){
	substanceHead[0].SetProceduralFloat("RidgesRoughnessShift", newValue);
	RebuildTexturesHead(0);
}

// CLAWS
function ClawsColor(){
	var newColor	= Color(parseFloat(clawsColor[0].GetComponent(UI.Slider).value),parseFloat(clawsColor[1].GetComponent(UI.Slider).value),parseFloat(clawsColor[2].GetComponent(UI.Slider).value), 1);
	substanceBody.SetProceduralColor("WingClawOverlayColor", newColor);
	colorDisplayClaws.GetComponent(UI.Image).color		= newColor;
	substanceBody.SetProceduralFloat("WingClawOverlayBlend", clawsColor[3].GetComponent(UI.Slider).value);
	RebuildTexturesBody();
}

function ClawsHue(newValue : float){
	substanceBody.SetProceduralFloat("WingClawHue", newValue);
	RebuildTexturesBody();
}

function ClawsSaturation(newValue : float){
	substanceBody.SetProceduralFloat("WingClawSaturation", newValue);
	RebuildTexturesBody();
}

function ClawsLightness(newValue : float){
	substanceBody.SetProceduralFloat("WingClawLightness", newValue);
	RebuildTexturesBody();
}

function ClawsContrast(newValue : float){
	substanceBody.SetProceduralFloat("WingClawContrast", newValue);
	RebuildTexturesBody();
}

function ClawsRoughnessShift(newValue : float){
	substanceBody.SetProceduralFloat("WingClawRoughnessShift", newValue);
	RebuildTexturesBody();
}

// MOUTH
function MouthColor(){
	var newColor	= Color(parseFloat(mouthColor[0].GetComponent(UI.Slider).value),parseFloat(mouthColor[1].GetComponent(UI.Slider).value),parseFloat(mouthColor[2].GetComponent(UI.Slider).value), 1);
	colorDisplayMouth.GetComponent(UI.Image).color		= newColor;
	for (var i : int; i < substanceHead.Length; i++){
		substanceHead[i].SetProceduralColor("MouthOverlayColor", newColor);
		substanceHead[i].SetProceduralFloat("MouthOverlayBlend", mouthColor[3].GetComponent(UI.Slider).value);
		RebuildTexturesHead(i);
	}
}

function MouthHue(newValue : float){
	for (var i : int; i < substanceHead.Length; i++){
		substanceHead[i].SetProceduralFloat("MouthHue", newValue);
		RebuildTexturesHead(i);
	}
}

function MouthSaturation(newValue : float){
	for (var i : int; i < substanceHead.Length; i++){
		substanceHead[i].SetProceduralFloat("MouthSaturation", newValue);
		RebuildTexturesHead(i);
	}
}

function MouthLightness(newValue : float){
	for (var i : int; i < substanceHead.Length; i++){
		substanceHead[i].SetProceduralFloat("MouthLightness", newValue);
		RebuildTexturesHead(i);
	}
}

function MouthContrast(newValue : float){
	for (var i : int; i < substanceHead.Length; i++){
		substanceHead[i].SetProceduralFloat("MouthContrast", newValue);
		RebuildTexturesHead(i);
	}
}

function MouthRoughnessShift(newValue : float){
	for (var i : int; i < substanceHead.Length; i++){
		substanceHead[i].SetProceduralFloat("MouthRoughnessShift", newValue);
		RebuildTexturesHead(i);
	}
}

// GROUND DIRT
function GroundDirtLevel(newValue : float){
	substanceBody.SetProceduralFloat("GroundDirtLevel", newValue);
	RebuildTexturesBody();
}

function GroundDirtContrast(newValue : float){
	substanceBody.SetProceduralFloat("GroundDirtContrast", newValue);
	RebuildTexturesBody();
}

function GroundDirtHeight(newValue : float){
	substanceBody.SetProceduralFloat("GroundDirtHeight", newValue);
	RebuildTexturesBody();
}

function GroundDirtRoughness(newValue : float){
	substanceBody.SetProceduralFloat("GroundDirtRoughness", newValue);
	RebuildTexturesBody();
}

function GroundDirtColor(){
	var newColor	= Color(parseFloat(groundDirtColor[0].GetComponent(UI.Slider).value),parseFloat(groundDirtColor[1].GetComponent(UI.Slider).value),parseFloat(groundDirtColor[2].GetComponent(UI.Slider).value),parseFloat(groundDirtColor[3].GetComponent(UI.Slider).value));
	substanceBody.SetProceduralColor("GroundDirtColor", newColor);
	colorDisplayGroundDirt.GetComponent(UI.Image).color	= newColor;
	RebuildTexturesBody();
}

// SURFACE DIRT
function DirtLevel(newValue : float){
	substanceBody.SetProceduralFloat("DirtAmount", newValue);
	RebuildTexturesBody();
}

function DirtContrast(newValue : float){
	substanceBody.SetProceduralFloat("DirtContrast", newValue);
	RebuildTexturesBody();
}

function DirtGrungeAmount(newValue : float){
	substanceBody.SetProceduralFloat("DirtGrunge", newValue);
	RebuildTexturesBody();
}

function DirtRoughness(newValue : float){
	substanceBody.SetProceduralFloat("DirtRoughness", newValue);
	RebuildTexturesBody();
}

function DirtColor(){
	var newColor	= Color(parseFloat(surfaceDirtColor[0].GetComponent(UI.Slider).value),parseFloat(surfaceDirtColor[1].GetComponent(UI.Slider).value),parseFloat(surfaceDirtColor[2].GetComponent(UI.Slider).value),parseFloat(surfaceDirtColor[3].GetComponent(UI.Slider).value));
	substanceBody.SetProceduralColor("DirtColor", newColor);
	colorDisplaySurfaceDirt.GetComponent(UI.Image).color	= newColor;
	RebuildTexturesBody();
}


/*
// HEAD
function HeadColor(){
	var newColor	= Color(parseFloat(headColor[0].GetComponent(UI.Slider).value),parseFloat(headColor[1].GetComponent(UI.Slider).value),parseFloat(headColor[2].GetComponent(UI.Slider).value),parseFloat(headColor[3].GetComponent(UI.Slider).value));
	substanceBody.SetProceduralColor("HeadColor", newColor);
	colorDisplayHead.GetComponent(UI.Image).color	= newColor;
	RebuildTextures();
}

function HeadContrast(newValue : float){
	substanceBody.SetProceduralFloat("HeadContrast", newValue);
	RebuildTextures();
}

function HeadRoughness(newValue : float){
	substanceBody.SetProceduralFloat("HeadRoughness", newValue);
	RebuildTextures();
}

// CARAPACE
function CarapaceColor(){
	var newColor	= Color(parseFloat(carapaceColor[0].GetComponent(UI.Slider).value),parseFloat(carapaceColor[1].GetComponent(UI.Slider).value),parseFloat(carapaceColor[2].GetComponent(UI.Slider).value),parseFloat(carapaceColor[3].GetComponent(UI.Slider).value));
	substanceBody.SetProceduralColor("CarapaceColor", newColor);
	colorDisplayCarapace.GetComponent(UI.Image).color	= newColor;
	RebuildTextures();
}

function CarapaceContrast(newValue : float){
	substanceBody.SetProceduralFloat("CarapaceContrast", newValue);
	RebuildTextures();
}

function CarapaceRoughness(newValue : float){
	substanceBody.SetProceduralFloat("CarapaceRoughness", newValue);
	RebuildTextures();
}

// UNDERSIDE
function UndersideColor(){
	var newColor	= Color(parseFloat(undersideColor[0].GetComponent(UI.Slider).value),parseFloat(undersideColor[1].GetComponent(UI.Slider).value),parseFloat(undersideColor[2].GetComponent(UI.Slider).value),parseFloat(undersideColor[3].GetComponent(UI.Slider).value));
	substanceBody.SetProceduralColor("UndersideColor", newColor);
	colorDisplayUnderside.GetComponent(UI.Image).color	= newColor;
	RebuildTextures();
}

function UndersideContrast(newValue : float){
	substanceBody.SetProceduralFloat("UndersideContrast", newValue);
	RebuildTextures();
}

function UndersideRoughness(newValue : float){
	substanceBody.SetProceduralFloat("UndersideRoughness", newValue);
	RebuildTextures();
}

// SPIKES
function SpikesColor(){
	var newColor	= Color(parseFloat(spikesColor[0].GetComponent(UI.Slider).value),parseFloat(spikesColor[1].GetComponent(UI.Slider).value),parseFloat(spikesColor[2].GetComponent(UI.Slider).value),parseFloat(spikesColor[3].GetComponent(UI.Slider).value));
	substanceBody.SetProceduralColor("SpikesColor", newColor);
	colorDisplaySpikes.GetComponent(UI.Image).color	= newColor;
	RebuildTextures();
}

function SpikesContrast(newValue : float){
	substanceBody.SetProceduralFloat("SpikesContrast", newValue);
	RebuildTextures();
}

function SpikesRoughness(newValue : float){
	substanceBody.SetProceduralFloat("SpikesRoughness", newValue);
	RebuildTextures();
}

// SKIN
function SkinHue(newValue : float){
	substanceBody.SetProceduralFloat("SkinHue", newValue);
	RebuildTextures();
}

function SkinSaturation(newValue : float){
	substanceBody.SetProceduralFloat("SkinSaturation", newValue);
	RebuildTextures();
}

function SkinLightness(newValue : float){
	substanceBody.SetProceduralFloat("SkinLightness", newValue);
	RebuildTextures();
}

function SkinRoughness(newValue : float){
	substanceBody.SetProceduralFloat("SkinRoughness", newValue);
	RebuildTextures();
}

function SkinContrast(newValue : float){
	substanceBody.SetProceduralFloat("SkinContrast", newValue);
	RebuildTextures();
}

// MOUTH
function MouthHue(newValue : float){
	substanceBody.SetProceduralFloat("MouthHue", newValue);
	RebuildTextures();
}

function MouthSaturation(newValue : float){
	substanceBody.SetProceduralFloat("MouthSaturation", newValue);
	RebuildTextures();
}

function MouthLightness(newValue : float){
	substanceBody.SetProceduralFloat("MouthLightness", newValue);
	RebuildTextures();
}

function MouthRoughness(newValue : float){
	substanceBody.SetProceduralFloat("MouthRoughness", newValue);
	RebuildTextures();
}

function MouthContrast(newValue : float){
	substanceBody.SetProceduralFloat("MouthContrast", newValue);
	RebuildTextures();
}


// TENTACLES
function TentaclesHue(newValue : float){
	substanceBody.SetProceduralFloat("TentaclesHue", newValue);
	RebuildTextures();
}

function TentaclesSaturation(newValue : float){
	substanceBody.SetProceduralFloat("TentaclesSaturation", newValue);
	RebuildTextures();
}

function TentaclesLightness(newValue : float){
	substanceBody.SetProceduralFloat("TentaclesLightness", newValue);
	RebuildTextures();
}

function TentaclesRoughness(newValue : float){
	substanceBody.SetProceduralFloat("TentaclesRoughness", newValue);
	RebuildTextures();
}

function TentaclesContrast(newValue : float){
	substanceBody.SetProceduralFloat("TentaclesContrast", newValue);
	RebuildTextures();
}

// GROUND DIRT
function GroundDirtLevel(newValue : float){
	substanceBody.SetProceduralFloat("GroundDirtLevel", newValue);
	RebuildTextures();
}

function GroundDirtContrast(newValue : float){
	substanceBody.SetProceduralFloat("GroundDirtContrast", newValue);
	RebuildTextures();
}

function GroundDirtHeight(newValue : float){
	substanceBody.SetProceduralFloat("GroundDirtHeight", newValue);
	RebuildTextures();
}

function GroundDirtRoughness(newValue : float){
	substanceBody.SetProceduralFloat("GroundDirtRoughness", newValue);
	RebuildTextures();
}

function GroundDirtColor(){
	var newColor	= Color(parseFloat(groundDirtColor[0].GetComponent(UI.Slider).value),parseFloat(groundDirtColor[1].GetComponent(UI.Slider).value),parseFloat(groundDirtColor[2].GetComponent(UI.Slider).value),parseFloat(groundDirtColor[3].GetComponent(UI.Slider).value));
	substanceBody.SetProceduralColor("GroundDirtColor", newColor);
	colorDisplayGroundDirt.GetComponent(UI.Image).color	= newColor;
	RebuildTextures();
}

// SURFACE DIRT
function DirtLevel(newValue : float){
	substanceBody.SetProceduralFloat("DirtLevel", newValue);
	RebuildTextures();
}

function DirtContrast(newValue : float){
	substanceBody.SetProceduralFloat("DirtContrast", newValue);
	RebuildTextures();
}

function DirtGrungeAmount(newValue : float){
	substanceBody.SetProceduralFloat("DirtGrungeAmount", newValue);
	RebuildTextures();
}

function DirtRoughness(newValue : float){
	substanceBody.SetProceduralFloat("DirtRoughness", newValue);
	RebuildTextures();
}

function DirtColor(){
	var newColor	= Color(parseFloat(surfaceDirtColor[0].GetComponent(UI.Slider).value),parseFloat(surfaceDirtColor[1].GetComponent(UI.Slider).value),parseFloat(surfaceDirtColor[2].GetComponent(UI.Slider).value),parseFloat(surfaceDirtColor[3].GetComponent(UI.Slider).value));
	substanceBody.SetProceduralColor("DirtColor", newColor);
	colorDisplaySurfaceDirt.GetComponent(UI.Image).color	= newColor;
	RebuildTextures();
}























*/











































