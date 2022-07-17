using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun Data", menuName = "Gun Data")]
public class GunData : ScriptableObject
{
    public Sprite gunSprite;
    public float spread;

    [Tooltip("Fire Rate is shots per second.")]
    public float fireRate;
    public int projectilesPerShot;

    public float cameraShakeIntensity = 0.05f;
    public float cameraShakeDecayRate = 0.1f;
    public int piercing = 1;
}
