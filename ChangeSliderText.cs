using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeSliderText : MonoBehaviour {

    Text numOfRoundsText;
	// Use this for initialization
	void Start () {
        numOfRoundsText = GameObject.Find("numOfRndsTxt").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
	if (numOfRoundsText != null)
        {
            numOfRoundsText.text = GameObject.Find("RoundsSlider").GetComponent<Slider>().value.ToString();
        }
	}
}
