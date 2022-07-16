using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    List<Die> dice;

    public int[] diceValuesDebug = new int[5];

    // Start is called before the first frame update
    void Start()
    {
        Die[] foundDice = GetComponentsInChildren<Die>();
        dice = new List<Die>();
        for (int i = 0; i < foundDice.Length; i++)
		{
            dice.Add(foundDice[i]);
		}
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < dice.Count; i++)
        {
            if (!dice[i].IsMoving() && dice[i].HasValue())
                 diceValuesDebug[i] = dice[i].value;
             else
                 diceValuesDebug[i] = -1;                
        }        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Throw();
        }
    }

    public bool AllDiceSettled()
    {
		foreach (Die die in dice)
		{
            if (die.IsMoving())
                return false;
            if (!die.HasValue())
                return false;
		}
        return true;
    }

    public bool GetValues(int[] outValues)
	{
        if (!AllDiceSettled())
            return false;

		for (int i = 0; i < dice.Count; i++)
		{
            outValues[i] = dice[i].value;
		}

        return true;
	}

    public bool Throw()
	{
        if (!AllDiceSettled())
            return false;

        foreach (Die die in dice)
        {
            if (!die.GetLocked())
                die.Throw();
        }

        return true;
	}
}
