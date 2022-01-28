using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class QuickSetup : EditorWindow
{
    public GameObject CarPrefab;
    public GameObject CarInScene;
    public GameObject CameraInScene;

    [MenuItem("Tools / Quick Setup")]
    public static void SetupScene()
    {
        GetWindow<QuickSetup>();
    }

    private void OnGUI()
    {
        GUILayout.Label("Note: if you already have setup the scene with this then this will destroy any leftovers");

        CarInScene = (GameObject)EditorGUILayout.ObjectField("Car", CarInScene, typeof(GameObject), true);
        CameraInScene = (GameObject)EditorGUILayout.ObjectField("Camera", CameraInScene, typeof(GameObject), true);
        

        if (GUILayout.Button("Setup New"))
        {
            if (CarInScene != null)
            {
                DestroyImmediate(CarInScene);
            }

            GameObject carObject = Instantiate(CarPrefab);
            carObject.name = CarPrefab.name;
            CarInScene = carObject;
        }

        if (GUILayout.Button("Setup with input"))
        {
            if (CarInScene == null)
            {

            }
            CarController carController = CarInScene.GetComponent<CarController>();
            if (carController == null)
            {
                CarInScene.AddComponent<CarController>();
            }
            ButterCameraController butterCam = CameraInScene.GetComponent<ButterCameraController>();
            if (butterCam == null)
            {
                CameraInScene.AddComponent<ButterCameraController>();
            }
        }
    }
}
