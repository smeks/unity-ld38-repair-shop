using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private float XSensitivity = 2f;
    private float YSensitivity = 2f;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public float MovementForwardForce = 20f;
    public float MovementStrafeForce = 20f;
    public float MaximumVelocity = 10f;
    public float MaximumStrafeVelocity = 5f;
    private Quaternion playerRotateTarget = Quaternion.identity;
    private Quaternion cameraRotateTarget = Quaternion.identity;
    private bool m_cursorIsLocked = true;
    public PlayerAvatarController playerAvatar;
    Rigidbody rb;
    Camera cam;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
    }

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal") * MovementForwardForce;
        var vertical = Input.GetAxis("Vertical") * MovementForwardForce;
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;


        var forwardForce = rb.transform.forward * vertical;
        var sidewaysForce = rb.transform.right * horizontal;
        var force = forwardForce + sidewaysForce;

        cameraRotateTarget *= Quaternion.Euler(-xRot, 0f, 0f);
        playerRotateTarget *= Quaternion.Euler(0f, yRot, 0f);

        // clamp camera up/down rot
        cameraRotateTarget = ClampRotationAroundXAxis(cameraRotateTarget);


        // apply forces & orientations
        if (rb.velocity.magnitude < MaximumVelocity)
            rb.AddForce(force);

        transform.localRotation = playerRotateTarget;
        cam.transform.localRotation = cameraRotateTarget;


        playerAvatar.UpdateAvatarPosition(transform.position, playerRotateTarget);
        playerAvatar.UpdateAvatarCamera(cam.transform.localRotation);

        InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
