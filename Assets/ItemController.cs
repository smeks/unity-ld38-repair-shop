using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemController : MonoBehaviour
{
    // parts list to repair this item
    public float Price = 0.0f;
    public bool isRepaired = false;
    public bool isTool = false;
    public bool isRepairable = false;
    public List<ItemController> PartsList;
    public string Description;

    Rigidbody rb;
    Transform worldRoot;
    TextMesh text;


    public void Pickup(Transform newParent)
    {
        rb.isKinematic = true;
        transform.parent = newParent;
    }

    public void Drop()
    {
        rb.isKinematic = false;
        transform.parent = worldRoot;
    }

    public void ShowText(bool show)
    {
        if (text)
        {
            text.gameObject.SetActive(show);
        }
    }

    public bool IsPickedUp()
    {
        return rb.isKinematic;
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        worldRoot = GameObject.FindGameObjectWithTag("WorldRoot").transform;
        text = GetComponentInChildren<TextMesh>();

        if (text)
        {
            var itemDes = string.Format("{0}\n", Description);
            if(PartsList.Count > 0)
            {
                itemDes += string.Format("{0}\n", "<color=orange>Parts Needed:</color>");
            }
            foreach(var part in PartsList)
            {
                itemDes += string.Format("<color=red>{0}</color>\n", part.name);
            }

            text.text = itemDes;
        }

        ShowText(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (text)
        {
            if (text)
            {
                var itemDes = string.Format("{0}\n", Description);
                if (PartsList.Count > 0)
                {
                    itemDes += string.Format("{0}\n", "<color=orange>Parts Needed:</color>");
                }
                foreach (var part in PartsList)
                {
                    itemDes += string.Format("<color=red>{0}</color>\n", part.name);
                }

                text.text = itemDes;
            }

            text.transform.forward = Camera.main.transform.forward;
        }
	}
}
