using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrinkFromKitchen : MonoBehaviour
{
    public Transform kitchenTransform;
    public TextMeshProUGUI text;

    private bool canDrink = true;
    private bool closeEnough = false;

    void Update()
    {
        // Check if we're close enough to drink
        if (Vector2.Distance(transform.position, kitchenTransform.position) < 4)
        {
            closeEnough = true;
        }
        else
        {
            closeEnough = false;
        }

        // Actual input
        if (canDrink & closeEnough && Input.GetKeyDown(KeyCode.F))
        {
            // Actual drinking
            if (TryGetComponent<Health>(out Health health))
            {
                health.hp++;
                health.hp = Mathf.Min(10, health.hp);
                canDrink = false;
                StartCoroutine("canDrinkRoutine");
                FindObjectOfType<SfxManager>().PlaySound("drink");
            }
        }

        // UI Effect
        if (closeEnough && canDrink)
        {
            text.SetText("Press F to take a swig <color=green>+1 Health</color>");
        }
        else if (closeEnough && !canDrink)
        {
            text.SetText("Come back soon...");
        }
        else
        {
            text.SetText("");
        }
    }

    private IEnumerator canDrinkRoutine()
    {
        yield return new WaitForSeconds(30);
        canDrink = true;
    }
}
