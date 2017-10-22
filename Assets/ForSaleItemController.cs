using UnityEngine;

public class ForSaleItemController
{
    public ItemController item;
    public bool isSold = false;
    float listedSaleTime; // time item was listed for sale
    float saleTime; // time in seconds before item sells
    float price; // price of item

    public ForSaleItemController(ItemController item)
    {
        this.item = item;
        listedSaleTime = Time.fixedTime;
        saleTime = Random.Range(30.0f, 120.0f);
        price = item.Price;
    }

    public float ItemIsSold()
    {
        var delta = Time.fixedTime - listedSaleTime;
        if ( delta > saleTime)
        {
            isSold = true;
            return price;
        }else
        {
            return 0.0f;
        }
    }
}
