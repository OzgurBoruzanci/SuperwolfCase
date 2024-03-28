using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfProductPlacement : MonoBehaviour
{
    public GameObject Product; /*{ get; set; }*/ 
    private List<GameObject> products = new List<GameObject>();
    private Renderer objectRenderer;
    private Bounds objectBounds;
    private float xSize;
    private float zSize;
    private void Start()
    {
        BoundsCalculator();
    }
    public void PlaceProduct(List<GameObject> productList)
    {
        Vector3 pos = Camera.main.transform.position;
        pos.y -= 0.5f;

        Bounds productBounds = Product.GetComponentInChildren<Renderer>().bounds;
        float xProductSize = productBounds.size.x;
        float zProductSize = productBounds.size.z;

        Vector3 initialPosition = new Vector3(transform.position.x - xSize / 2, transform.position.y, transform.position.z - zSize / 2);
        Vector3 finalPosition = new Vector3(transform.position.x + xSize / 2, transform.position.y, transform.position.z + zSize / 2);
        var productPosition=new Vector3(initialPosition.x+xProductSize/2,initialPosition.y,initialPosition.z+zProductSize/2);
        if (CheckProductType(productList[0]))
        {
            products = productList;
            for (int i = 0; i < products.Count; i++)
            {
                products[i].transform.position = pos;
                products[i].transform.parent = transform;
                products[i].transform.DOMove(productPosition, 0.5f).SetEase(Ease.OutQuint);
                if (productPosition.x < finalPosition.x)
                {
                    productPosition.z += zProductSize;
                    if (productPosition.z > finalPosition.z)
                    {
                        productPosition.x += xProductSize;
                        productPosition.z = initialPosition.z + zProductSize / 2;
                    }
                }
            }
        }
    }

    private void BoundsCalculator()
    {
        objectRenderer = GetComponent<Renderer>();
        objectBounds = objectRenderer.bounds;
        xSize = objectBounds.size.x;
        zSize = objectBounds.size.z;
    }
    private bool CheckProductType(GameObject product)
    {
        return product == Product;
    }
}
