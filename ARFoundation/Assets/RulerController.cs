using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RulerController : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Vector3 centerPosition;

    public Transform rulerPoint;
    public GameObject ruler;
    public Transform rulers;

    private Ruler currentRuler;
    private List<Ruler> rulerList = new List<Ruler>();

    private bool isEnableRuler = false;
    private Vector3 savedRulerPosition;

    private void Start()
    {
        centerPosition = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    }

    private void Update()
    {
        if (raycastManager.Raycast(centerPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            savedRulerPosition = hitPose.position;

            isEnableRuler = true;
            rulerPoint.rotation = Quaternion.Lerp(rulerPoint.rotation, hitPose.rotation, 0.2f);

            if (currentRuler != null ) 
            {
                currentRuler.SetPointB(hitPose.position);
            }
        }
        else
        {
            isEnableRuler = true;
            rulerPoint.rotation = Quaternion.Lerp(rulerPoint.rotation, Quaternion.Euler(90f, 0f, 0f), 0.5f)
;        }
    }

    public void MakeRuler()
    {
        if (isEnableRuler)
        {
            if (currentRuler != null)
            {
                currentRuler = null;
            }
            else
            {
                GameObject rulerObj = Instantiate(ruler);
                rulerObj.transform.SetParent(rulers);
                rulerObj.transform.position = Vector3.zero;
                rulerObj.transform.localScale = new Vector3(1, 1, 1);

                currentRuler = rulerObj.GetComponent<Ruler>();
                currentRuler.SetPointA(savedRulerPosition);
                rulerList.Add(currentRuler);
            }
        }
    }
}
