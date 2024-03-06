using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class PlacementScript : MonoBehaviour
{

    public GameObject placementIndicator;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private TrackableId placedPlaneID = TrackableId.invalidId;
    private Transform placementTransform;
    private ARRaycastManager arOrigin;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    void Start()
    {
        arOrigin = GetComponent<ARRaycastManager>();
    }


    void Update()
    {
        UpdatePlacementIndicator();
        UpdatePlacementPose();
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);


            placementIndicator.transform.position = placementPose.position;

            Vector3 surfaceNormal = placementPose.rotation * Vector3.up;
            Quaternion rotation = Quaternion.LookRotation(surfaceNormal);

            placementIndicator.transform.rotation = rotation;
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }


    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        if (arOrigin.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            placementPoseIsValid = hits.Count > 0;

            if (placementPoseIsValid)
            {
                placementPose = hits[0].pose;
                placedPlaneID = hits[0].trackableId;

                var planeManager = GetComponent<ARPlaneManager>();
                ARPlane arPlane = planeManager.GetPlane(placedPlaneID);
                placementTransform = arPlane.transform;
            }
        }
    }
}
