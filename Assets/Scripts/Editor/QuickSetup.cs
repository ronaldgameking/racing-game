using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class QuickSetup : EditorWindow
{
    public GameObject CarPrefab;

    [MenuItem("Tools / Quick Setup")]
    public static void SetupScene()
    {
        GetWindow<QuickSetup>();
    }

    private void OnGUI()
    {
        GUILayout.Label("Note: if you already have setup the scene with this then this will destroy any leftovers");
        
        if (GUILayout.Button("Setup"))
        {
            {
                CarController carComponent = FindObjectOfType<CarController>();
                if (carComponent != null)
                {
                    DestroyImmediate(carComponent.gameObject);
                }
                else
                {
                    Logger.Log("oof");
                }
                
            }

            GameObject carObject =  Instantiate(CarPrefab);
            carObject.name = CarPrefab.name;
        }
    }
}
