using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class GridMgr
{
    public static CameraLookaround lookAround;
    public static PointHeatMap heatMap;
    
    //public static HeadPosition head;

    static GridMgr()
    {
        GameObject gm = safeFind("_appManager");

        lookAround = (CameraLookaround)SafeComponent(gm, "CameraLookaround");
        heatMap = (PointHeatMap)SafeComponent(gm, "PointHeatMap");
        //head = (HeadPosition)SafeComponent(gm, "HeadPosition");

    }

    // when Grid wakes up, it checks everything is in place
    // it uses these trivial routines to do so
    private static GameObject safeFind(string s)
    {
        GameObject gm = GameObject.Find(s);
        if (gm == null) Woe("GameObject " + s + "  not on _preload.");
        return gm;
    }
    private static Component SafeComponent(GameObject gm, string s)
    {
        Component component = gm.GetComponent(s);
        if (component == null) Woe("Component " + s + " not on _preload.");
        return component;
    }
    private static void Woe(string error)
    {
        Debug.Log(">>> Cannot proceed... " + error);
        Debug.Log(">>> It is very likely you just forgot to launch");
        Debug.Log(">>> from scene zero, the _preload scene.");
    }
}


