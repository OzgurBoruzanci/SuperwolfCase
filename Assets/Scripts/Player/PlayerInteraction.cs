using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask shelfLayerMask;
    public List<GameObject> productPrefab;
    //public List<GameObject> ProductPrefab { get; set; }
    private RaycastHit hit;
    private InputManager inputManager;
    private ShelfProductPlacement shelfProductPlacement;
    [SerializeField]private GameObject previewObject;
    
    void Start()
    {
        inputManager = InputManager.Instance;
    }
    
    private void Update()
    {
        if(productPrefab.Count>0)
        {
            CheckTheShelfs();
            MouseDown();
        }
    }
    private void CheckTheShelfs()
    {
        Ray ray = Camera.main.ScreenPointToRay(inputManager.GetMousePosition());
        if (Physics.Raycast(ray, out hit,3f, shelfLayerMask))
        {
            shelfProductPlacement = hit.collider.gameObject.GetComponentInParent<ShelfProductPlacement>();
            if (shelfProductPlacement != null && shelfProductPlacement.IsShelFull == false)
            {
                if (previewObject == null)
                {
                    previewObject = Instantiate(productPrefab[0], new Vector3(0, -5, 0), shelfProductPlacement.transform.rotation);
                    previewObject.tag = "Clone";
                    var previewRenderer = previewObject.GetComponentInChildren<Renderer>();
                    var previewMaterial = previewRenderer.material;

                    var previewColor = previewMaterial.color;
                    previewColor.a = 0.5f;
                    previewMaterial.color = previewColor;
                }
                previewObject.transform.position = hit.point;
            }
        }
        else if (previewObject != null)
        {
            Destroy(previewObject);
        }
    }
    private void MouseDown()
    {
        if (inputManager.GetMouseLeftClick())
        {
            shelfProductPlacement.Product = productPrefab[0];
            shelfProductPlacement.PlaceProduct(productPrefab);
            Debug.Log(shelfProductPlacement.gameObject.name+"  raf");
            shelfProductPlacement=null;
            Destroy(previewObject);
        }
    }
}
