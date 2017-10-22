using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForSaleController : MonoBehaviour {

    public Text moneyText;
    public List<ForSaleItemController> ForSaleItems = new List<ForSaleItemController>();
    public float Money = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach(var item in ForSaleItems)
        {
            var price = item.ItemIsSold();
            if(price > 0.0f)
            {
                Money += price;
                moneyText.text = string.Format("Money: ${0}", Money);
                Destroy(item.item.gameObject);
            }
        }

        ForSaleItems.RemoveAll(i => i.isSold == true);
	}

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.layer & GameConstants.CollisionItem) == GameConstants.CollisionItem)
        {
            var newItem = other.gameObject.GetComponent<ItemController>();
            var b = new ForSaleItemController(newItem);
            ForSaleItems.Add(b);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.layer & GameConstants.CollisionItem) == GameConstants.CollisionItem)
        {
            var newItem = other.gameObject.GetComponent<ItemController>();
            ForSaleItems.RemoveAll(i => i.item == newItem);
        }
    }
}
