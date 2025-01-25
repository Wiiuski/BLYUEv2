using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public float CameraSpeed = 100f;
    public GameObject FollowObj;
    Vector3 FollowPOS;
    public int mouseSensitivity;
    public GameObject CameraObj;
    public GameObject PlayerObj;
    public float camDistanceXToPlayer;
    public float camDistanceYToPlayer;
    public float camDistanceZToPlayer;
    public float mouseX;
    public float mouseY;
    public float smoothX;
    public float smoothY;
    public float rotY;
    public float rotX;
    public bool cameraLocked = true;
    public Quaternion localRotation;
    public Vector3 localPos;
    public Vector3 initPos;
    public Quaternion initRot;
    public Vector3 cameraPos;
    public Quaternion cameraRot;

    public bool woke;

    //public GameMain main;

    static float ClampAngle(float angle, float min, float max)
    {
        if (min < 0 && max > 0 && (angle > max || angle < min))
        {
            angle -= 360;
            if (angle > max || angle < min)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
                else return max;
            }
        }
        else if (min > 0 && (angle > max || angle < min))
        {
            angle += 360;
            if (angle > max || angle < min)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
                else return max;
            }
        }

        if (angle < min) return min;
        else if (angle > max) return max;
        else return angle;
    }


    public void Start()
    {

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        woke = true;
    }

    

    

    // Update is called once per frame
    void Update()
    {
        if (woke)
        {
                rotX += -Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
                rotY += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                rotX = ClampAngle(rotX, -60, 60);
                rotY = ClampAngle(rotY, -180, 180);
                localRotation = Quaternion.Euler(rotX, rotY, 0f);
                transform.rotation = localRotation;
        }
    }
    void LateUpdate()
    {
        if (woke)
        {
                CameraUpdater();
                
                        cameraPos = transform.position;
                        localPos = cameraPos;
                        cameraRot = transform.rotation;
                    
            
        }
    }
    void CameraUpdater()
    {
        Transform target = FollowObj.transform;
        float step = CameraSpeed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    public void SetCamPos()
    {
            transform.position = cameraPos;
            transform.rotation = cameraRot;
            localRotation = cameraRot;
    }



}
