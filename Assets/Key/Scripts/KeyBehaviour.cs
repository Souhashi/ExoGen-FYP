using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour {

    public BoxCollider2D keyCollider;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            DeactivateGameObject();
        }
    }

    void DeactivateGameObject()
    {
        keyCollider.enabled = false;
        gameObject.SetActive(false);
    }
	void Start () {
        keyCollider = GetComponent<BoxCollider2D>();
	}
	
	
}
