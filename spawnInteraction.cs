using UnityEngine;
using System.Collections;

public class spawnInteraction : MonoBehaviour {
    public bool isSelected = false; //set to private later
    public GameObject highlight;
    public Object highlightClone;
    public Transform location;
    public bool resetSprite;

    // Use this for initialization
    void Start () {
    }

    void Update()
    {
        if (resetSprite == true) 
        {
            Destroy(highlightClone);
            Debug.Log("Destroyed Crate");
            resetSprite = false;
        }
    }

    // On mouse click
    void OnMouseDown()
    {
        isSelected = true; //this object is not selected
        highlightClone = Instantiate(highlight, location.position, location.rotation); // generate the highlight object

    }

    public void resetIsSelected()
    {
        this.isSelected = false;
        Debug.Log("isSelected now false");
    }

    public void resetSpriteCall()
    {
        this.resetSprite = true;
    }
}
