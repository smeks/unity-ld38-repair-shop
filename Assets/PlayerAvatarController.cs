using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatarController : MonoBehaviour {
    public Transform ItemSlot1;
    public Transform ItemSlot2;
    public Transform ItemSlot3;

    private ItemController lastHitObject;

    Rigidbody rb;
    Camera cam;

    public void UpdateAvatarPosition(Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.localRotation = rot;
    }

    public void UpdateAvatarCamera(Quaternion rot)
    {
        cam.transform.localRotation = rot;
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {

        if (ItemSlot1.transform.childCount > 0)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                ItemSlot1.GetChild(0).GetComponent<ItemController>().Drop();
                return;   
            }
        }


        var ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if ((hitInfo.collider.gameObject.layer & GameConstants.CollisionWorkbench) == GameConstants.CollisionWorkbench &&
                hitInfo.distance < 5.0f )
            {
                var wb = hitInfo.collider.GetComponent<WorkbenchController>();
                if(wb.CanRepair)
                {
                    if (Input.GetKeyUp(KeyCode.R))
                    {
                        wb.DoRepair();
                    }
                }
            }

            if ( (hitInfo.collider.gameObject.layer & GameConstants.CollisionItem) == GameConstants.CollisionItem &&
                hitInfo.distance < 5.0f)
            {
                lastHitObject = hitInfo.collider.GetComponent<ItemController>();
                
                if(lastHitObject.IsPickedUp())
                    lastHitObject.ShowText(false);
                else
                    lastHitObject.ShowText(true);

                if (Input.GetKeyUp(KeyCode.E))
                    lastHitObject.Pickup(ItemSlot1.transform);
            }
            else if(lastHitObject)
            {
                lastHitObject.ShowText(false);
                lastHitObject = null;
            }
        }
    }
}
