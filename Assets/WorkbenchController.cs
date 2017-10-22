using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorkbenchController : MonoBehaviour {

    public List<ItemController> PartsOnWorkbench = new List<ItemController>();
    public List<ItemController> PartsList = new List<ItemController>();
    public bool CanRepair = false;

    TextMesh text;

    // Use this for initialization
    void Start () {
        // disable text at start
        text = GetComponentInChildren<TextMesh>();
        text.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DoRepair()
    {
        PartsOnWorkbench.Find(p => p.isRepairable).isRepaired = true;
        PartsOnWorkbench.Find(p => p.isRepairable).PartsList.Clear();
        PartsOnWorkbench.FindAll(p => !p.isRepairable && !p.isTool).ForEach(part => {
            Destroy(part.gameObject);
        });
        PartsOnWorkbench.RemoveAll(p => !p.isRepairable && !p.isTool);
        PartsList.Clear();
        CheckPartsForRepair();
    }

    public bool CheckPartsForRepair()
    {
        if(PartsList.Count == 0)
        {
            text.gameObject.SetActive(false);
            CanRepair = false;
            return false;
        }

        var validPartsCount = 0;

        foreach (var part in PartsList)
        {
            if (PartsOnWorkbench.Find(p => p.Description == part.Description))
                validPartsCount++;
        }

        if (validPartsCount == PartsList.Count)
        {
            text.gameObject.SetActive(true);
            CanRepair = true;
            return true;
        }

        text.gameObject.SetActive(false);
        CanRepair = false;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if( (other.gameObject.layer & GameConstants.CollisionItem) == GameConstants.CollisionItem)
        {
            var newPart = other.gameObject.GetComponent<ItemController>();

            if(newPart.isRepairable)
            {
                PartsList.Clear();
                foreach(var part in newPart.PartsList)
                {
                    PartsList.Add(part);
                }
            }

            if (PartsOnWorkbench.Contains(newPart))
            {
                return;
            }else
            {
                PartsOnWorkbench.Add(newPart);
                CheckPartsForRepair();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.layer & GameConstants.CollisionItem) == GameConstants.CollisionItem)
        {
            var newPart = other.gameObject.GetComponent<ItemController>();

            if (PartsOnWorkbench.Contains(newPart))
            {
                if (newPart.isRepairable)
                {
                    PartsList.Clear();
                }
                PartsOnWorkbench.Remove(newPart);
                CheckPartsForRepair();
            }
        }
    }
}
