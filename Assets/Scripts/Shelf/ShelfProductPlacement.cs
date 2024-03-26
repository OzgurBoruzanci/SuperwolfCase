using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfProductPlacement : MonoBehaviour
{
    public GameObject productPrefab;
    private List<Vector3> productPositions = new List<Vector3>();
    private Vector3 firstProductSize;
    private float spacing = 0.1f;
    //oyuncunun elinde liste olabilir her týklandýðýnda bir obje eksiltip bu kod ile dizilebilir
    private void OnMouseDown()
    {
        PlaceProduct();
    }

    private void PlaceProduct()
    {
        if (productPositions.Count == 0)
        {
            DetermineProductPositions();
        }
        GameObject newProduct = Instantiate(productPrefab, transform.position, Quaternion.identity);
        newProduct.transform.parent = transform;

        Vector3 newProductPosition = productPositions.Count > 0 ? productPositions[productPositions.Count - 1] + firstProductSize + new Vector3(spacing, 0f, 0f) : transform.position;
        newProduct.transform.position = newProductPosition;

        if (productPositions.Count == 0)
        {
            firstProductSize = newProduct.GetComponent<Renderer>().bounds.size;
        }
        productPositions.Add(newProductPosition);
    }

    private void DetermineProductPositions()
    {
        Bounds shelfBounds = GetComponent<Renderer>().bounds;
        float xPos = shelfBounds.min.x;
        while (xPos <= shelfBounds.max.x)
        {
            productPositions.Add(new Vector3(xPos, shelfBounds.min.y, shelfBounds.min.z));
            xPos += firstProductSize.x + spacing;
        }
    }
}
