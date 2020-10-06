using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using System.Threading;

public class MoveTest2 : MonoBehaviour
{
    Vector3 tempPos;
    int yth = 0;
    List<Vector3> EyeTrackingVecData = new List<Vector3>();
    List<Vector3> EyeTrackingPosData = new List<Vector3>();
    List<
string> EyeTrackingObjData = new List<string>();

    void Start()
    {
        //Debug.Log("test");

        using (var reader = new StreamReader(@"Assets/EYE.csv"))
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
                //Debug.Log("test   A");
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
                EyeTrackingVecData.Add(new Vector3(VecX, VecY, VecZ));
                EyeTrackingPosData.Add(new Vector3(PosX, PosY, PosZ));
                EyeTrackingObjData.Add(vals[7]);
            }
        }
    }

    void Update()
    {
        //...
        //ienumerator will slow down the rate  or mabye a sleep (but may lag)
        //tempPos.y += .001f;
        tempPos = EyeTrackingPosData[yth];
        yth++;
        // Debug.Log("TEST HERE ****** inside update \n");

        transform.position = tempPos;
        Thread.Sleep(250);
    }
}
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