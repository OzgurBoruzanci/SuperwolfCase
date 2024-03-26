using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfTransport : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragging = false;
    private float distance;

    private void OnMouseDown()
    {
        distance= Vector3.Distance(transform.position, Camera.main.transform.position);
        Debug.Log(distance);
        if (!isDragging && distance <=3f)
        {
            isDragging = true;
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(Vector3.up, 90f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -90f);
        }
    }
    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}





//{    //�uanda objeyinin clonesini ta��yor b�rakt���nda orjinal obje geliyor.
//     //ama ben orjinalini ta��y�p yere dikd�rtgen ta��yacaz
//    private Vector3 screenPoint;
//private Vector3 offset;
//private bool isDragging = false;
//private GameObject shadowObject; // G�lge objesi

//private void OnMouseDown()
//{
//    float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
//    if (!isDragging && distance <= 3f)
//    {
//        isDragging = true;
//        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
//        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

//        // G�lge objeyi olu�tur
//        shadowObject = Instantiate(gameObject);
//        shadowObject.GetComponent<Renderer>().material.color = new Color(0f, 0f, 0f, 0.5f); // G�lge rengi (siyah ve yar� saydam)
//        shadowObject.transform.localScale *= 1.1f; // G�lge objeyi biraz b�y�t
//        shadowObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z); // Yere yerle�tir
//        Destroy(shadowObject.GetComponent<ShelfTransport>()); // G�lge objede s�r�kleme i�levselli�ini devre d��� b�rak
//    }
//}

//private void OnMouseDrag()
//{
//    if (isDragging)
//    {
//        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
//        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
//        transform.position = curPosition;
//    }
//    if (Input.GetKeyDown(KeyCode.E))
//    {
//        transform.Rotate(Vector3.up, 90f);
//    }
//    if (Input.GetKeyDown(KeyCode.Q))
//    {
//        transform.Rotate(Vector3.up, -90f);
//    }
//}

//private void OnMouseUp()
//{
//    if (isDragging)
//    {
//        isDragging = false;
//        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
//        // G�lge objeyi yok et
//        Destroy(shadowObject);
//    }
//}
//}