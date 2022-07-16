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

        GameObject currGambleParticles = Instantiate(GambleParticles);

        Destroy(currGambleText, 4.0f);
        Destroy(currGambleParticles, 4.0f);
    }

    public void SpawnBattleTransition()
    {
        GameObject currBattleText = Instantiate(BattleText);
        currBattleText.transform.SetParent(gameObject.transform, false);
        GameObject currBattleParticles = Instantiate(BattleParticles);

        Destroy(currBattleText, 4.0f);
        Destroy(currBattleParticles, 4.0f);
    }
}
