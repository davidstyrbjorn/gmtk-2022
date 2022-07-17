using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp;
    public float invinsibilityTimer = 0;
    private bool isInvinsible = false;

    public void TakeDamage(int howMuch)
    {
        if (isInvinsible) return;

        // Is there a timer for how long we are invinsible? Become invinsible for X seconds
        if (invinsibilityTimer != 0)
        {
            isInvinsible = true;
            StartCoroutine("Invinsibility");
        }

        hp -= howMuch;
    }

    private IEnumerator Invinsibility()
    {
        yield return new WaitForSeconds(invinsibilityTimer);
        isInvinsible = false;
    }
}
