using UnityEngine;
using System.Collections;

/*
 * Attatch to camera and call DoShake from another class to start the shake
*/

public class CameraShake : MonoBehaviour
{
    private bool Shaking;
    private float ShakeDecay;
    private float ShakeIntensity;

    private Quaternion OriginalRot;

    void Start()
    {
        Shaking = false;
    }

    void Update()
    {
        if (ShakeIntensity > 0)
        {
            transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                           OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                           OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                           OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f);

            ShakeIntensity -= ShakeDecay * Time.deltaTime;

        }
        else if (Shaking)
        {
            transform.eulerAngles = Vector3.zero;
            Shaking = false;
        }
    }

    public void DoShake(float _intensity = 0.12f, float _shakedecay = 0.0085f)
    {
        OriginalRot = Quaternion.identity;

        ShakeIntensity = _intensity;
        ShakeDecay = _shakedecay;
        Shaking = true;
    }
}