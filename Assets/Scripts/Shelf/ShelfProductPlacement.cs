using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShelfProductPlacement : MonoBehaviour
{
    public bool IsShelFull { get=>isShelFull; }
    public GameObject Product { get; set; }
    private List<GameObject> products = new List<GameObject>();
    private Vector3 pos;
    private bool isShelFull;
    private float xProductSize;
    private float zProductSize;
    private Vector3 initialPosition;
    private Vector3 finalPosition;

    public void PlaceProduct(List<GameObject> productList)
    {
        if (!isShelFull)
        {
            GetProductAndShelfSizes();
            var sequence = DOTween.Sequence();
            var productPosition = new Vector3(initialPosition.x - xProductSize / 2, transform.position.y, initialPosition.z - zProductSize / 2);
            if (CheckProductType(productList[0]))
            {
                for (int i = 0; i < productList.Count; i++)
                {
                    productList[i].transform.localPosition = pos;
                    productList[i].transform.parent = transform.parent;
                    products.Add(productList[i]);
                    sequence.Append(productList[i].transform.DOLocalMove(productPosition, 0.5f).SetEase(Ease.OutQuint));
                    productList[i].transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    products.Add(productList[i]);
                    
                    if (productPosition.x > finalPosition.x)
                    {
                        productPosition.z -= zProductSize;
                        if (productPosition.z < finalPosition.z)
                        {
                            productPosition.x -= xProductSize;
                            productPosition.z = initialPosition.z - zProductSize / 2;
                        }
                    }
                    if (productPosition.x <= finalPosition.x && productPosition.z >= finalPosition.z)
                    {
                        isShelFull = true;
                        break;
                    }
                }
                sequence.Play();
                foreach (GameObject product in products)
                {
                    productList.Remove(product);
                }
            }
        }
    }

    private bool CheckProductType(GameObject product)
    {
        return product == Product;
    }
    private void GetProductAndShelfSizes()
    {
        pos = Camera.main.transform.position;
        pos.y -= 0.5f;
        pos = transform.InverseTransformPoint(pos);

        xProductSize = Product.transform.GetChild(0).localScale.x + 0.05f;
        zProductSize = Product.transform.GetChild(0).localScale.z + 0.05f;

        initialPosition = transform.GetChild(0).localPosition;
        finalPosition = transform.GetChild(1).localPosition;
    }
}
