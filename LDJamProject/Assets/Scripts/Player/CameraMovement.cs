using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : SingletonBase<CameraMovement>
{
    Vector3 playerLookDir;
    public Vector3 cameraOriPos;
    [SerializeField] float cameraScale;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = 0;
        playerLookDir = worldMousePos - GetComponent<Transform>().position;

        Camera.main.transform.position = cameraOriPos + playerLookDir.normalized * cameraScale * playerLookDir.magnitude;
    }
}
