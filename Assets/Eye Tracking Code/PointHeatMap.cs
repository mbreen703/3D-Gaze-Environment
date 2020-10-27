using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using System.Threading;

public class PointHeatMap : MonoBehaviour
{
	public ParticleSystem system;

	void Start()
	{
		Material mat = new Material(Shader.Find("Unlit/PointcloudShader")); 
		RaycastHit hit;

		system = gameObject.GetComponent<ParticleSystem>();
		gameObject.GetComponent<ParticleSystemRenderer>().material = mat;

		List<Vector3> EyeTrackingVecData = new List<Vector3>();
		List<Vector3> EyeTrackingPosData = new List<Vector3>();
		List<string > EyeTrackingObjData = new List<string >();

		using (var reader = new StreamReader(@"Assets/Eye Tracking Data/FoveEyes_9-15-2020 9_06_01 AM.csv"))
		{
			var SkipHeader = false;
			while (!reader.EndOfStream)
			{
				if (!SkipHeader)
				{
					SkipHeader = true;
					continue;
				}
				string   line = reader.ReadLine();
				string[] vals = line.Split(new char[] { ',' });

				Vector3 Vec = new Vector3(0,0,0);
				Vector3 Pos = new Vector3(0,0,0);

				float.TryParse(vals[1] != null ? vals[1] : "0.0", out Vec.x);
				float.TryParse(vals[2] != null ? vals[2] : "0.0", out Vec.y);
				float.TryParse(vals[3] != null ? vals[3] : "0.0", out Vec.z);

				float.TryParse(vals[4] != null ? vals[4] : "0.0", out Pos.x);
				float.TryParse(vals[5] != null ? vals[5] : "0.0", out Pos.y);
				float.TryParse(vals[6] != null ? vals[6] : "0.0", out Pos.z);
				EyeTrackingVecData.Add(Vec);
				EyeTrackingPosData.Add(Pos);
				EyeTrackingObjData.Add(vals[7]);
			}
		}

		for (var i = 0; i < EyeTrackingPosData.Count; i++) {
			if (Physics.Raycast(EyeTrackingPosData[i], EyeTrackingVecData[i], out hit))
	            { // We have hit geometry of some sort.
	                EmitPoint(EyeTrackingPosData[i] + EyeTrackingVecData[i] * hit.distance);
	            }
		}
	}

	void EmitPoint(Vector3 pos)
	{
		var emitParams = new ParticleSystem.EmitParams();
		emitParams.startColor = Color.red;
		emitParams.startSize  = 0.025f;
		emitParams.position   = pos;
		system.Emit(emitParams, 10);
	}

}