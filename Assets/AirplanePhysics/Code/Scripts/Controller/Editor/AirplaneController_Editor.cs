using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace WeLoveAero
{
    [CustomEditor(typeof(Airplane_Controller))]
    public class AirplaneController_Editor : Editor 
    {
        #region Variables
        private Airplane_Controller targetController;
        #endregion


        #region Builtin Metods
        void OnEnable()
        {
            targetController = (Airplane_Controller)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();


            GUILayout.Space(15);
            if(GUILayout.Button("Get Airplane Components", GUILayout.Height(35)))
            {
                //Find All Engines
                targetController.engines.Clear();
                targetController.engines = FindAllEngines().ToList<Airplane_Engine>();

                //Find All Wheels
                targetController.wheels.Clear();
                targetController.wheels = FindAllWheels().ToList<Airplane_Wheel>();

                //Find All Control Surfaces
                targetController.controlSurfaces.Clear();
                targetController.controlSurfaces = FindAllControlSurfaces().ToList<Airplane_ControlSurface>();
            }

            if(GUILayout.Button("Create Airplane Preset", GUILayout.Height(35)))
            {
                string filePath = EditorUtility.SaveFilePanel("Save Airplane Preset", "Assets", "New_Airplane_Preset", "asset");
                SaveAirplanePreset(filePath);
            }
            GUILayout.Space(15);

        }
        #endregion



        #region Custom Methods
        Airplane_Engine[] FindAllEngines()
        {
            Airplane_Engine[] engines = new Airplane_Engine[0];
            if(targetController)
            {
                engines = targetController.transform.GetComponentsInChildren<Airplane_Engine>(true);
            }

            return engines;
        }

        Airplane_Wheel[] FindAllWheels()
        {
            Airplane_Wheel[] wheels = new Airplane_Wheel[0];
            if(targetController)
            {
                wheels = targetController.transform.GetComponentsInChildren<Airplane_Wheel>(true);
            }

            return wheels;
        }

        Airplane_ControlSurface[] FindAllControlSurfaces()
        {
            Airplane_ControlSurface[] controlSurfaces = new Airplane_ControlSurface[0];
            if(targetController)
            {
                controlSurfaces = targetController.transform.GetComponentsInChildren<Airplane_ControlSurface>(true);
            }

            return controlSurfaces;
        }

        void SaveAirplanePreset(string aPath)
        {
            if(targetController && !string.IsNullOrEmpty(aPath))
            {
                string appPath = Application.dataPath;

                string finalPath = "Assets" + aPath.Substring(appPath.Length);
//                Debug.Log(finalPath);

                //Create new Preset
                Airplane_Preset newPreset = ScriptableObject.CreateInstance<Airplane_Preset>();
                newPreset.airplaneWeight = targetController.airplaneWeight;
                if(targetController.centerOfGravity)
                {
                    newPreset.cogPosition = targetController.centerOfGravity.localPosition;
                }

                if(targetController.charactistics)
                {
                    newPreset.dragFactor = targetController.charactistics.dragFactor;
                    newPreset.maxMPH = targetController.charactistics.maxMPH;
                    newPreset.rbLerpSpeed = targetController.charactistics.rbLerpSpeed;
                    newPreset.liftCurve = targetController.charactistics.liftCurve;
                    newPreset.maxLiftPower = targetController.charactistics.maxLiftPower;
                    newPreset.pitchSpeed = targetController.charactistics.pitchSpeed;
                    newPreset.rollSpeed = targetController.charactistics.rollSpeed;
                    newPreset.yawSpeed = targetController.charactistics.yawSpeed;
                }

                //Create Final Preset
                AssetDatabase.CreateAsset(newPreset, finalPath);
            }
        }
        #endregion
    }
}
