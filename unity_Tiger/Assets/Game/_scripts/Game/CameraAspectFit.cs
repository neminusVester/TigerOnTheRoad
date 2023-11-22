using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectFit : MonoBehaviour
{
    public float targetScreenWidth = 5.0f; // Set your target screen width here

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found.");
            return;
        }

        AdjustCamera();
    }

    void AdjustCamera()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        float targetAspect = targetScreenWidth / mainCamera.orthographicSize;

        float orthoSize = targetScreenWidth / (2.0f * currentAspect);

        mainCamera.orthographicSize = orthoSize;
    }
}

