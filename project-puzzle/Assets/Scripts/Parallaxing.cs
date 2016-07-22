using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds; // Array of all back/foregrounds to be parallaxed.
	private float[] parallaxScales; //The proportion of the camera's movement to move the backgrounds by.
	public float smoothing = 1f;			// How smooth the parallax is going to be.


	private Transform cam; 	// Reference to the main cameras transform
	private Vector3 previousCamPos; //The position of the camera in the previous frame.

	// Is called before Start(). Great for assigning variables or references.
	void Awake() {
		//Assigning the main camera automatically without having to drag it in to the script in unity.
		cam = Camera.main.transform;

	}
	// Use this for initialization
	void Start () {
		//The previous frame had the current frame's camera position.
		previousCamPos = cam.position;
		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; ++i) {
			// Invert the position of the background?
			parallaxScales[i] = backgrounds[i].position.z * -1;

		}
	}
	
	// Update is called once per frame
	void Update () {
		// for each background
		for(int i = 0; i < backgrounds.Length; ++i){
			//the parallax is the opposite of the camera movement because the previous
			//frame multiplied by the scale.
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

			//Set a target x position which is the current position plus the parallax.
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			// Create a target position which is the background's current position with it's target x pos.
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			//fade between current pos and the target pos using lerp
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

		previousCamPos = cam.position;
	}
}
