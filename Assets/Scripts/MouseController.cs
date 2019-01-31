using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpawnerController))]
public class MouseController : MonoBehaviour {

    public LayerMask spawnMask;
    Camera cam;
    SpawnerController spawner;

    // Use this for initialization
    void Start () {
        cam = Camera.main;

        spawner = GetComponent<SpawnerController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, spawnMask))
            {
                spawner.Spawn(hit.point);
                
            }
        }
    }
}
