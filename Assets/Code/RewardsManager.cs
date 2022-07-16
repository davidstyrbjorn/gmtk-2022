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
        // Translate type into reward
        return 1.0f;
    }
}
