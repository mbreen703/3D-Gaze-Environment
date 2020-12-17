using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Runtime.InteropServices;

public class PointHeatMap : MonoBehaviour
{
	public ParticleSystem system;

	List<Vector3> EyeTrackingVecData = new List<Vector3>();
	List<Vector3> EyeTrackingPosData = new List<Vector3>();
	List<string> EyeTrackingObjData = new List<string>();

	//bool ShowAllData      = false;

	//TESTING
	bool ShowAllData = true;
	bool ShowRealtimeData = true;
	float FrameNumber  = 0;
	int   CurrentFrame = 0;

	//Toggle realtime data
	public void ToggleRealTimeData()
    {
		ShowRealtimeData = !ShowRealtimeData;
    }
	//Toggle conglomerate data
	public void ToggleConglomerateData()
    {
		ShowAllData = !ShowAllData;
    }
	//Reset realtime data
	public void ResetRealTimeData()
    {
		CurrentFrame = 0;
		FrameNumber = 0;
    }

	void Start()
	{
		Material mat = new Material(Shader.Find("Particles/Standard Unlit"));
		//RaycastHit hit;

		RaycastHit hitAll;
		
		system = gameObject.GetComponent<ParticleSystem>();

		TextAsset reader = (TextAsset)Resources.Load("FoveEyes_9-15-2020_9_06_01AM");

		string fs = reader.text;
		//Convert data file into an array of strings consisting of each line
		string[] lineNum = fs.Split(System.Environment.NewLine.ToCharArray());

		for (int i = 1; i < lineNum.Length; i++)
		{
			//Get current line and parse columns
			string line = lineNum[i];
			string[] vals = line.Split(new char[] { ',' });
		
			Vector3 Vec = new Vector3(0, 0, 0);
			Vector3 Pos = new Vector3(0, 0, 0);

			for (int v = 0; v < vals.Length; v++)
			{
				switch (v)
				{
					case 1:
						float.TryParse(vals[1] != null ? vals[1] : "0.0", out Vec.x); break;
					case 2:
						float.TryParse(vals[2] != null ? vals[2] : "0.0", out Vec.y); break;
					case 3:
						float.TryParse(vals[3] != null ? vals[3] : "0.0", out Vec.z); break;
					case 4:
						float.TryParse(vals[4] != null ? vals[4] : "0.0", out Pos.x); break;
					case 5:
						float.TryParse(vals[5] != null ? vals[5] : "0.0", out Pos.y); break;
					case 6:
						float.TryParse(vals[6] != null ? vals[6] : "0.0", out Pos.z); break;
					case 7:
						EyeTrackingObjData.Add(vals[7]); break;
				}
			}
			EyeTrackingVecData.Add(Vec);
			EyeTrackingPosData.Add(Pos);
		}
		
		if (ShowAllData) {
			for (var i = 0; i < EyeTrackingPosData.Count; i++) {
				if (Physics.Raycast(EyeTrackingPosData[i], EyeTrackingVecData[i], out hitAll))
				{ // We have hit geometry of some sort.
					EmitPoint(EyeTrackingPosData[i] + EyeTrackingVecData[i] * hitAll.distance);
				}
			}
		}
		
		
	}

	//float FrameNumber  = 0;
	//int   CurrentFrame = 0;
	float FrameRate = 30; //FPS

	void Update() {
		if (ShowRealtimeData) {
			//RaycastHit hit;

			RaycastHit hitReal;

			FrameNumber += Time.deltaTime;

			while (FrameNumber >= (1/FrameRate)) {
				   FrameNumber -= (1/FrameRate);

				if (CurrentFrame < EyeTrackingPosData.Count) {
					if (Physics.Raycast(EyeTrackingPosData[CurrentFrame], EyeTrackingVecData[CurrentFrame], out hitReal)) { // We have hit geometry of some sort.
						var emitParams = new ParticleSystem.EmitParams();
						emitParams.startColor = Color.green;
						emitParams.startSize  = 0.04f;
						emitParams.position   = EyeTrackingPosData[CurrentFrame] + EyeTrackingVecData[CurrentFrame] * hitReal.distance;
						emitParams.startLifetime   = 2;
						system.Emit(emitParams, 10);
					}
					//Debug.Log(CurrentFrame);
					CurrentFrame++;
				}
			}
		}
		/*
		//TESTING
		if(Input.GetKeyDown(KeyCode.T))
        {
			ToggleConglomerateData();

		}
		if(Input.GetKeyDown(KeyCode.R))
        {
			ToggleRealTimeData();
        }
		*/

	}

	void EmitPoint(Vector3 pos) {
		var emitParams = new ParticleSystem.EmitParams();
		emitParams.startColor = Color.blue;
		emitParams.startSize  = 0.03f;
		emitParams.position   = pos;
		system.Emit(emitParams, 10);
	}	

}
