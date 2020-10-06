using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using System.Threading;

public class VisionTrack2 : MonoBehaviour
{

    Vector3 tempPos;
    Vector3 tempVec;
    Vector3 lookPos;
    RaycastHit hit;

    int yth2 = 0;
    List<Vector3> EyeTrackingVecData2 = new List<Vector3>();
    List<Vector3> EyeTrackingPosData2 = new List<Vector3>();
    List<string> EyeTrackingObjData2 = new List<string>();

    void Start()
    {
        using (var reader = new StreamReader(@"Assets/EYE2.csv"))
        {
            var SkipHeader = false;
            while (!reader.EndOfStream)
            {
                if (!SkipHeader)
                {
                    SkipHeader = true;
                    continue;
                }
                string line2 = reader.ReadLine();
                string[] vals2 = line2.Split(new char[] { ',' });
                Debug.Log("##### vision tracking test   A");
                float VecX2 = 0.0f;
                float VecY2 = 0.0f;
                float VecZ2 = 0.0f;

                float PosX2 = 0.0f;
                float PosY2 = 0.0f;
                float PosZ2 = 0.0f;

                float.TryParse(vals2[1] != null ? vals2[1] : "0.0", out VecX2);
                float.TryParse(vals2[2] != null ? vals2[2] : "0.0", out VecY2);
                float.TryParse(vals2[3] != null ? vals2[3] : "0.0", out VecZ2);

                float.TryParse(vals2[4] != null ? vals2[4] : "0.0", out PosX2);
                float.TryParse(vals2[5] != null ? vals2[5] : "0.0", out PosY2);
                float.TryParse(vals2[6] != null ? vals2[6] : "0.0", out PosZ2);
                EyeTrackingVecData2.Add(new Vector3(VecX2, VecY2, VecZ2));
                EyeTrackingPosData2.Add(new Vector3(PosX2, PosY2, PosZ2));
                EyeTrackingObjData2.Add(vals2[7]);
            }
        }
    }
    void Update()
    {
        tempPos = EyeTrackingPosData2[yth2];
        tempVec = EyeTrackingVecData2[yth2];
        //raycast
        if (Physics.Raycast(tempPos, tempVec, out hit))
        { // We have hit geometry of some sort
            lookPos = tempPos + tempVec * hit.distance;
        }
        yth2++;
        transform.position = lookPos;
        Thread.Sleep(250);
    }

}





/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;

public class VisionTrack2 : MonoBehaviour
{
    RaycastHit hit;

    List<Vector3> EyeTrackingVecData = new List<Vector3>();
    List<Vector3> EyeTrackingPosData = new List<Vector3>();
    List<string> EyeTrackingObjData = new List<string>();

    List<RaycastHit> EyeTrackingHitData = new List<RaycastHit>();

    //GameObject prefab = (GameObject)Instantiate(Resources.Load("V2"));
    //  Instantiate (Resources.Load ("Category1/Hatchet")) as GameObject;
    Vector3 tempPos;
    Vector3 tempVec;
    void Start()
    {
        Debug.Log("test");
        GameObject prefab = (GameObject)Instantiate(Resources.Load("V2"));

        using (var reader = new StreamReader(@"Assets/SHORTlist.csv"))
        {
            var SkipHeader = false;
            while (!reader.EndOfStream)
            {
                if (!SkipHeader)
                {
                    SkipHeader = true;
                    continue;
                }

                string line = reader.ReadLine();
                string[] vals = line.Split(new char[] { ',' });


                float VecX = 0.0f;
                float VecY = 0.0f;
                float VecZ = 0.0f;

                float PosX = 0.0f;
                float PosY = 0.0f;
                float PosZ = 0.0f;


                float.TryParse(vals[1] != null ? vals[1] : "0.0", out VecX);
                float.TryParse(vals[2] != null ? vals[2] : "0.0", out VecY);
                float.TryParse(vals[3] != null ? vals[3] : "0.0", out VecZ);

                float.TryParse(vals[4] != null ? vals[4] : "0.0", out PosX);
                float.TryParse(vals[5] != null ? vals[5] : "0.0", out PosY);
                float.TryParse(vals[6] != null ? vals[6] : "0.0", out PosZ);

                var tVec = new Vector3(VecX, VecY, VecZ);
                var tPos = new Vector3(PosX, PosY, PosZ);

                EyeTrackingVecData.Add(tVec);
                EyeTrackingPosData.Add(tPos);
                EyeTrackingObjData.Add(vals[7]);

                if (Physics.Raycast(tPos, tVec, out hit))
                {
                    EyeTrackingHitData.Add(hit);
                }
            }

            for (var i = 0; i < EyeTrackingPosData.Count; i++)
            {
                tempPos = EyeTrackingPosData[i];
                tempVec = EyeTrackingVecData[i];
                Debug.Log(" Before instantiat ORIGINAL BAD  \n");
                if (Physics.Raycast(tempPos, tempVec, out hit))
                {
                    EyeTrackingHitData.Add(hit);

                    Instantiate(prefab, tempPos + tempVec * hit.distance, Quaternion.identity);
                    //Debug.Log("TEST HERE " + i + " \n" );
                }
            }
    }
}

void Update()
{
    //...
    //ienumerator will slow down the rate  or mabye a sleep (but may lag)
    //tempPos.y += .001f;
}
}
*/
/*
{
Vector3 tempPos;
// Start is called before the first frame update
void Start()
{
    tempPos = transform.position;
    TextAsset myData = Resources.Load<TextAsset>("dataFile");
    //string[] sepData = myData.text.Split(new char[] { '\n' });
    string[] sepData = myData.text.Split(  '\n' );
    //Debug.Log(sepData.Length);
    Debug.Log("TEST HERE inside start BEFORE LOOP ******* \n");
    for (int i=1; i<25; i++)
    {
      //  string[] row = sepData[i].Split(new char[] { ',' });
        //Console.WriteLine("TEST\n");
        Debug.Log("TEST HERE inside start \n");
    }

}

// Update is called once per frame
void Update()
{
    //ienumerator will slow down the rate  or mabye a sleep (but may lag)
    tempPos.y += .001f;
    Debug.Log("TEST HERE ****** inside update \n");

    transform.position =tempPos;
}
}
*/