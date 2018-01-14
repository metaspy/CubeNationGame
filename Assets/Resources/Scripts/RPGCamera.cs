using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCamera : MonoBehaviour {

    private const string mTargetTag = "RigidBodyFPSController";
    private const float mSmoothFactor = 5.0f;
    private const int mZoomFactor = 1;
    private const int mZoomCloseThreshold = 15;
    private const int mZoomFarThreshold = 100;

    private int mCurrentZoom = mZoomFarThreshold;

    public GameObject mTarget;
    public Camera mCamera;

	void Start () {
        mCamera = Camera.main;
	}

    void Update() {
        FindPlayer();
    }
	
	void LateUpdate () {
        LookAtTarget(); 
        AdjustCameraZoom();
	}

    private void FindPlayer() {
        if (mTarget == null) {
            mTarget = GameObject.FindGameObjectWithTag(mTargetTag);
        }        
    }

    private void LookAtTarget() {
        Vector3 relativePosition = mTarget.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(relativePosition);
    }

    private void AdjustCameraZoom() {
        float mouseScroll = getMouseScroll();

        if (mouseScroll < 0f && mCurrentZoom > mZoomCloseThreshold) {
            mCurrentZoom -= mZoomFactor;
        }

        if (mouseScroll > 0f && mCurrentZoom < mZoomFarThreshold) {
            mCurrentZoom += mZoomFactor;
        }

        mCamera.fieldOfView = Mathf.Lerp(
            mCamera.fieldOfView, mCurrentZoom, Time.deltaTime * mSmoothFactor
        );
    }

    private float getMouseScroll() {
        return Input.GetAxis("Mouse ScrollWheel");
    }

}
