﻿using Kopernicus;
using System;
using UnityEngine;


namespace SigmaDimensionsPlugin
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class SpaceCenterFixer : MonoBehaviour
    {
        void Start()
        {
            foreach (SpaceCenterCamera2 camera in Resources.FindObjectsOfTypeAll<SpaceCenterCamera2>())
            {
                float resizeBuildings = (float)FlightGlobals.GetHomeBody().Get<double>("resizeBuildings");

                camera.zoomInitial *= resizeBuildings;
                camera.zoomMax *= resizeBuildings;
                camera.zoomMin *= resizeBuildings;
                camera.zoomSpeed *= resizeBuildings;
            }
        }


        public bool fixLight = true;
        public Light light = null;

        void Update()
        {
            if (fixLight && ScenarioUpgradeableFacilities.GetFacilityLevel(SpaceCenterFacility.LaunchPad) == 1)
            {
                if (light == null)
                {
                    light = Array.Find(GameObject.FindGameObjectsWithTag("KSC_Pad_Water_Tower"), o => o.name == "Spotlight").GetComponent<Light>();
                }
                else
                {
                    light.range *= (float)(FlightGlobals.GetHomeBody().Has("resizeBuildings") ? FlightGlobals.GetHomeBody().Get<double>("resizeBuildings") : 1);
                    fixLight = false;
                }
            }
        }
    }
}
