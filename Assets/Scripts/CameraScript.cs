using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CameraScript : MonoBehaviour
{
    Camera cam;
    public float zoomMin;
    public float zoomMax;

    float touchesPrevPosDifference, touchesCurrPosDifference, zoomModifier;
    Vector2 firstTouchPrevPos, secondTouchPrevPos;

    [SerializeField]
    float zoomModifierSpeed = 0.1f;
    [SerializeField]
    TextAlignment text;
   

    private void Awake() {
        cam = GetComponent<Camera> ();
    }
      

    private void Update() {
        cam.fieldOfView -= 5 * Input.GetAxis("Mouse ScrollWheel");
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, zoomMin, zoomMax);

        // if (Input.touchCount == 2){
        //     Touch firstTouch = Input.GetTouch (0);
        //     Touch secondTouch = Input.GetTouch(1);
            
        //     firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
        //     secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

        //     touchesPrevPosDifference = (firstTouchPrevPos -secondTouchPrevPos).magnitude;
        //     touchesCurrPosDifference = (firstTouch.position - secondTouch.position).magnitude;

        //     zoomModifier = (firstTouch.deltaPosition - secondTouch.deltaPosition).magnitude - zoomModifierSpeed;

        //     if(touchesPrevPosDifference > touchesCurrPosDifference)
        //         cam.orthographicSize += zoomModifier;
        //     if(touchesPrevPosDifference < touchesCurrPosDifference)
        //         cam.orthographicSize -= zoomModifier;
        // }

        // cam.orthographicSize = Mathf.Clamp (cam.orthographicSize, 2f, 10f);
        //text.text = "Camera size" + cam.orthographicSize;
        }
    }

