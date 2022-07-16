using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PassiveRewardTypes
{
    FIRE_RATE,
    MOVE_SPEED,
}

public class RewardsManager : MonoBehaviour
{
    public float GetReward(PassiveRewardTypes type)
    {
        switch (type)
        {
            default:
                return 1.0f;
            case PassiveRewardTypes.FIRE_RATE:
                return 1f + (FindObjectOfType<ScoreOnePair>().score / 20f);
            case PassiveRewardTypes.MOVE_SPEED:
                return 1f + (FindObjectOfType<ScoreTwoPair>().score / 20f);
        }

        // Translate type into reward
        return 1.0f;
    }
}
