using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WeLoveAero
{
    public class AirplaneSetup_Window : EditorWindow 
    {
        #region Variables
        private string wantedName;
        #endregion


        #region BuiltIn Methods
        public static void LaunchSetupWindow()
        {
            AirplaneSetup_Window.GetWindow(typeof(AirplaneSetup_Window), true, "Airplane Setup").Show();
        }

        void OnGUI()
        {
            wantedName = EditorGUILayout.TextField("Airplane Name:", wantedName);
            if(GUILayout.Button("Create new Airplane"))
            {
                Airplane_SetupTools.BuildDefaultAirplane(wantedName);

                AirplaneSetup_Window.GetWindow<AirplaneSetup_Window>().Close();
            }
        }
        #endregion
    }
}
