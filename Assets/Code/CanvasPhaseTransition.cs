using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPhaseTransition : MonoBehaviour
{
    [SerializeField] GameObject GambleText;
    [SerializeField] GameObject GambleParticles;

    [SerializeField] GameObject BattleText;
    [SerializeField] GameObject BattleParticles;
    public void SpawnGambleTransition()
    {
        GameObject currGambleText = Instantiate(GambleText);
        currGambleText.transform.SetParent(gameObject.transform, false);

        Instantiate(GambleParticles);
    }

    public void SpawnBattleTransition()
    {
        GameObject currBattleText = Instantiate(BattleText);
        currBattleText.transform.SetParent(gameObject.transform, false);
        Instantiate(BattleParticles);
    }
}
