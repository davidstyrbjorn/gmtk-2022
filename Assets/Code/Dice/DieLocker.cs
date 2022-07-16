using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieLocker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Mouse0))
    // 	{
    //         RaycastHit hit;
    //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //         if (Physics.Raycast(ray, out hit))
    // 		{
    //             Rigidbody rigidBody = hit.rigidbody;
    //             if (rigidBody != null)
    // 			{
    // 			    if (rigidBody.gameObject.TryGetComponent(out Die die))
    //                 {
    //                     if (!die.IsMoving())
    //                         die.SetLocked(!die.GetLocked());
    // 			    }
    //             }
    //         }
    // 	}
    // }
}
