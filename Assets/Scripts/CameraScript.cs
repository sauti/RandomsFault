using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CameraScript : MonoBehaviour
{
    public Camera cam;
    public float zoomMin;
    public float zoomMax;
    public float sensitivity;

    private Camera mainCamera;

    private Touch touchA;
    private Touch touchB;
    private Vector2 touchADir;
    private Vector2 touchBDir;
    private float distBtwnTouchesPosition;
    private float distBtwnTouchesDirection;
    private float zoom;

    private void Awake() {
        mainCamera = Camera.main;
    }
      

    private void Update() {
    //     cam.fieldOfView -= 5 * Input.GetAxis("Mouse ScrollWheel");
    //     cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, zoomMin, zoomMax);

        if(Input.touchCount == 2){
            touchA = Input.GetTouch(0);
            touchA = Input.GetTouch(1);
            touchADir = touchA.position - touchA.deltaPosition;
            touchBDir = touchB.position - touchB.deltaPosition;

            distBtwnTouchesPosition = Vector2.Distance(touchA.position, touchB.position);
            distBtwnTouchesDirection = Vector2.Distance(touchADir, touchBDir);

            zoom = distBtwnTouchesPosition - distBtwnTouchesDirection;

            var currentZoom = mainCamera.orthographicSize - zoom * sensitivity;

            mainCamera.orthographicSize = Mathf.Clamp(currentZoom, zoomMin, zoomMax);
        }
    }
}
