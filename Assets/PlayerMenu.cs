using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMenu : MonoBehaviour {
    public float increment;
    public Image indicator;
    float amount;
    string currentColliding = "";
    Vector3 initialScaleIndicator;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.forward * 500.0f+ transform.position);
    }
    private void Start()
    {
        initialScaleIndicator = indicator.transform.localScale;
    }
    void Update () {
        Ray ray = new Ray(transform.position, transform.forward * 500f);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit)){
            Debug.Log(hit.collider.name);
            if (hit.collider.name != currentColliding || hit.collider.tag!="UI")
            {
                amount = 0;
                indicator.transform.localScale = initialScaleIndicator;
            }
            else
            {
                amount += increment;
                indicator.transform.localScale = new Vector3(indicator.transform.localScale.x + increment, indicator.transform.localScale.y + increment, indicator.transform.localScale.z + increment);
            }
            currentColliding = hit.collider.name;
        }
        else
        {
            amount = 0;
            indicator.transform.localScale = initialScaleIndicator;
        }
        if(amount >= 1)
        {
            switch (currentColliding)
            {
                case "Start":
                    SceneManager.LoadScene("game");
                    break;
                case "Credits":
                    SceneManager.LoadScene("credits");
                    break;
                case "Exit":
                    Application.Quit();
                    break;
                case "Return":
                    SceneManager.LoadScene("MainMenu");
                    break;
                
            }
            indicator.transform.localScale = initialScaleIndicator;
            amount = 0;
        }
	}
}
