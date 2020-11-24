using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using System.Threading;

//This class contains code to render out a particle system which shows all data points available in the given .csv file provided,
// as well as real-time visualization of gaze angle and user fixation.
public class PointHeatMap : MonoBehaviour
{
	public ParticleSystem system;

	List<Vector3> EyeTrackingVecData = new List<Vector3>();
	List<Vector3> EyeTrackingPosData = new List<Vector3>();
	List<string > EyeTrackingObjData = new List<string >();

	bool  ShowAllData      = true; //ShowAllData      - A boolean which when true, will render all data points in the .csv file at once - in blue.
	bool  ShowRealtimeData = true; //ShowRealtimeData - A boolean which when true, will render the data in-order at a specified framerate - in green.
	int   CurrentFrame     = 0;
	float FrameNumber      = 0;

	//ToggleRealTimeData - Toggles the ShowRealtimeData boolean for real-time data visualization - meant for external access through JavaScript.
	void ToggleRealTimeData() {
		ShowRealtimeData = !ShowRealtimeData;
	}

	//ToggleStaticData - Toggles the ShowAllData boolean for static data visualization - meant for external access through JavaScript.
	void ToggleStaticData() {
		ShowAllData = !ShowAllData;
	}

	//ResetRealTimeDataDisplay - Resets the simulation frame to allow for real-time data to be rendered from the beginning frame again.
	void ResetRealTimeDataDisplay() {
		CurrentFrame = 0;
		FrameNumber  = 0;
	}

	void Start() {
		Material mat = new Material(Shader.Find("Particles/Standard Unlit")); 
		RaycastHit hit;

		system = gameObject.GetComponent<ParticleSystem>();
		//gameObject.GetComponent<ParticleSystemRenderer>().material = mat;

		//Read in the provided data file and parse it.
		using (var reader = new StreamReader(@"Assets/Eye Tracking Data/FoveEyes_9-15-2020 9_06_01 AM.csv"))
		{
			var SkipHeader = false;
			while (!reader.EndOfStream)
			{
				if (!SkipHeader) // The first line of the .csv is always the header, so it must be skipped.
				{
					SkipHeader = true;
					continue;
				}

				string   line = reader.ReadLine();
				string[] vals = line.Split(new char[] { ',' });

				Vector3 Vec = new Vector3(0,0,0); // Setup for two known vectors to be read.
				Vector3 Pos = new Vector3(0,0,0);

				float.TryParse(vals[1] != null ? vals[1] : "0.0", out Vec.x); // Parse the X, Y, and Z components of the first vector.
				float.TryParse(vals[2] != null ? vals[2] : "0.0", out Vec.y);
				float.TryParse(vals[3] != null ? vals[3] : "0.0", out Vec.z);

				float.TryParse(vals[4] != null ? vals[4] : "0.0", out Pos.x); // Parse the X, Y, and Z components of the second vector.
				float.TryParse(vals[5] != null ? vals[5] : "0.0", out Pos.y);
				float.TryParse(vals[6] != null ? vals[6] : "0.0", out Pos.z);

				EyeTrackingVecData.Add(Vec); //Append these temp vectors to the list of vectors for later reading.
				EyeTrackingPosData.Add(Pos);
				EyeTrackingObjData.Add(vals[7]);
			}
		}

		if (ShowAllData) { // Upon initialization, if ShowAllData is true, generate the static data visualization.
			for (var i = 0; i < EyeTrackingPosData.Count; i++) {
				if (Physics.Raycast(EyeTrackingPosData[i], EyeTrackingVecData[i], out hit))
					{ // We have hit geometry of some sort.
						EmitPoint(EyeTrackingPosData[i] + EyeTrackingVecData[i] * hit.distance);
					}
			}
		}
	}

	float FrameRate    = 30; // FPS / Hz
	void Update() {
		if (ShowRealtimeData) { // If we wish to see real-time data, render it in green.
			RaycastHit hit;

			FrameNumber += Time.deltaTime; // Increase the current frame by the frame time.

			while (FrameNumber >= (1/FrameRate)) { // If the total frame time is greater than that of the given fps (1/FrameRate), we to render the next point.
				   FrameNumber -= (1/FrameRate);

				if (CurrentFrame < EyeTrackingPosData.Count) {
					if (Physics.Raycast(EyeTrackingPosData[CurrentFrame], EyeTrackingVecData[CurrentFrame], out hit)) { // We have hit geometry of some sort.
						var emitParams = new ParticleSystem.EmitParams();
						emitParams.startColor = Color.green;
						emitParams.startSize  = 0.04f;
						emitParams.position   = EyeTrackingPosData[CurrentFrame] + EyeTrackingVecData[CurrentFrame] * hit.distance;
						emitParams.startLifetime   = 2;
						system.Emit(emitParams, 10);
					}

					CurrentFrame++;
				}
			}
		}
	}

	//EmitPoint - Helper function for emitting a particle from ParticleSystem.
	void EmitPoint(Vector3 pos) {
		var emitParams = new ParticleSystem.EmitParams();
		emitParams.startColor = Color.blue;
		emitParams.startSize  = 0.03f;
		emitParams.position   = pos;
		system.Emit(emitParams, 10);
	}

}