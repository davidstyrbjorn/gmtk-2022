using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceLandZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerStay(Collider other)
	{
        DieSide dieSide = other.GetComponent<DieSide>();
        if (dieSide != null)
        {
            Die die = other.GetComponentInParent<Die>();
            
            die.value = 7 - dieSide.value;
        }
    }
}
