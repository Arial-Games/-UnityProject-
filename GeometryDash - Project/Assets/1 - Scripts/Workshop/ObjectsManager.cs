using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ObjectsManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] SO_WorkshopObjects[] so_WorkshopObjects;

    [Header("Bouttons / Sprite")]
    [SerializeField] GameObject[] buyingButtons;
    [SerializeField] Sprite interoPoint;
    [SerializeField] Image floatingImage;

    int levelData = 1000; // Size of the level
    bool isPlacingObject = false;
    SO_WorkshopObjects selectedObject;
    [SerializeField] List<GameObject> placedObjects = new List<GameObject>();

    private void Update()
    {
        if (floatingImage.enabled)
        {
            floatingImage.transform.position = Input.mousePosition;
        }

        if (isPlacingObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                CancelPlacement();
            }
        }
    }

    private void Start()
    {
        HideFloatingImage();

        System.Array.Sort(so_WorkshopObjects, (a, b) => a.uniqueId.CompareTo(b.uniqueId));

        for (int i = 0; i < buyingButtons.Length; i++)
        {
            if (so_WorkshopObjects.Length > i)
            {
                buyingButtons[i].GetComponent<Image>().sprite = so_WorkshopObjects[i].objSprite;
            }
            else
            {
                buyingButtons[i].GetComponent<Image>().sprite = interoPoint;
                buyingButtons[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    public void workshopButton(int id)
    {
        int realId = id - 1;
        if (id == 0)
        {
            Debug.LogWarning("Boutton non renseigné");
            return;
        }

        if (so_WorkshopObjects[realId].dataSize > levelData)
        {
            Debug.LogWarning("Taille insufisante !");
            return;
        }

        selectedObject = so_WorkshopObjects[realId];
        floatingImage.sprite = selectedObject.objSprite;
        floatingImage.enabled = true;
        floatingImage.SetNativeSize();
        isPlacingObject = true;
    }

    void PlaceObject()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        GameObject newObject = new GameObject(selectedObject.name);
        SpriteRenderer renderer = newObject.AddComponent<SpriteRenderer>();
        renderer.sprite = selectedObject.objSprite;
        newObject.transform.position = worldPosition;

        placedObjects.Add(newObject);
        levelData -= selectedObject.dataSize;

        if (levelData <= 0)
        {
            CancelPlacement();
        }

        if (selectedObject.uniqueId == 1)
        {
            buyingButtons[0].GetComponent<Button>().interactable = false;
            CancelPlacement();
        }
    }

    void CancelPlacement()
    {
        HideFloatingImage();
        isPlacingObject = false;
    }

    public void HideFloatingImage()
    {
        floatingImage.enabled = false;
    }
}