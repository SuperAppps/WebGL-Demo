using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;



public class GameControl : MonoBehaviour
{
	float deltaTime = 0.0f;

	public static GameObject dirLight = null,
							pointLight = null,
							spotLight = null,
							sphereObj = null;

	public static float sphereSpeedNormal = 10.0f,
						sphereSpeedHigh = 100.0f;

	private static bool dirLightDefault = true,
						pointLightDefault = false,
						spotLightDefault = false,
						fogDefault = false,
						depthOfFieldDefault = false;

	PostProcessingProfile m_Profile;


	private Material skyBox = null;

	public static bool showFPS = true,
						showSkybox = true;

	public void SwitchFPS (bool selected)
	{
		showFPS = selected;
	}
		
	public void SwitchSkybox (bool selected)
	{
		if (!selected) {
			RenderSettings.skybox = null;
		} else {
			RenderSettings.skybox = skyBox;
			RenderSettings.ambientLight = Color.black;
		}

		DynamicGI.UpdateEnvironment ();
	}

	public void SwitchDirLight (bool selected)
	{
		dirLight.SetActive (selected);
	}

	public void SwitchPointLight (bool selected)
	{
		pointLight.SetActive (selected);
	}

	public void SwitchSpotLight (bool selected)
	{
		spotLight.SetActive (selected);
	}

	public void SwitchHighSpeed (bool selected)
	{
		if (selected) {
			(sphereObj.GetComponent<SphereShow>() as SphereShow).sphereSpeed = sphereSpeedHigh;
		} else {
			(sphereObj.GetComponent<SphereShow>() as SphereShow).sphereSpeed = sphereSpeedNormal;
		}
	}

	public void SwitchFog (bool selected)
	{
		RenderSettings.fog = selected;
		RenderSettings.fogDensity = 0.18f;
		RenderSettings.fogMode = FogMode.Exponential;
		RenderSettings.fogColor = Color.gray;	// 0x858585FF;
		DynamicGI.UpdateEnvironment ();
	}

	public void SwitchDepthOfField (bool selected)
	{
		m_Profile.depthOfField.enabled = selected;
	}


	void Awake ()
	{
		dirLight = GameObject.Find ("Directional Light");
		pointLight = GameObject.Find ("Point light");
		spotLight = GameObject.Find ("Spotlight");
		sphereObj = GameObject.Find ("Sphere");

		var behaviour = Camera.main.GetComponent<PostProcessingBehaviour>();

/*		if (behaviour.profile == null)
		{
			enabled = false;
			return;
		}	*/

		m_Profile = Instantiate(behaviour.profile);
		behaviour.profile = m_Profile;

	}

	void Start ()
	{
		Debug.Log ("Test!");

		skyBox = new Material (RenderSettings.skybox);	// RenderSettings.skybox;
		
		GameObject.Find ("ToggleDirectionalLight").GetComponent<Toggle> ().isOn = dirLightDefault;
		GameObject.Find ("TogglePointLight").GetComponent<Toggle> ().isOn = pointLightDefault;
		GameObject.Find ("ToggleSpotLight").GetComponent<Toggle> ().isOn = spotLightDefault;
		GameObject.Find ("ToggleFog").GetComponent<Toggle> ().isOn = fogDefault;
		SwitchFog (fogDefault);
		GameObject.Find ("ToggleDepthOfField").GetComponent<Toggle> ().isOn = depthOfFieldDefault;
	}

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}

	void OnDisable()
	{
		RenderSettings.skybox = skyBox;
		RenderSettings.ambientLight = Color.black;
		DynamicGI.UpdateEnvironment();
	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		if (showFPS) {

			GUIStyle style = new GUIStyle();

			Rect rect = new Rect(w * 0.01f, h * 0.01f, w, h * 10 / 100);
			style.alignment = TextAnchor.UpperLeft;
			style.fontSize = h * 10 / 100;
			style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
			float msec = deltaTime * 1000.0f;
			float fps = 1.0f / deltaTime;
			string text = string.Format(" {0:0.} fps", fps);
			GUI.Label(rect, text, style);

			style.fontSize = h * 6 / 100;
			rect = new Rect(w * 0.01f, h * 90 / 100, w, h * 0.99f);
			text = string.Format(" x:{0:000.}\ty:{1:000.}", Input.mousePosition.x, Input.mousePosition.y);
			GUI.Label(rect, text, style);

			// Input.mousePosition.y
		}

	}
}
