using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesDisabler : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Obstacle") && !other.CompareTag("Poop"))
            other.transform.parent.gameObject.SetActive(false);
        else
            other.gameObject.SetActive(false);
    }
}
