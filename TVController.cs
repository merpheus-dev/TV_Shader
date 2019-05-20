using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVController : MonoBehaviour
{
    [SerializeField]
    private Material tvMaterial;

    [SerializeField]
    private float speed;

    private bool isPowerUp = false;

    private void Start()
    {
        SetInitialStateToClosed();
        StartPowerButtonListening();
    }

    private void SetInitialStateToClosed()
    {
        tvMaterial.SetFloat("_Close", 50f); 
    }

    private void StartPowerButtonListening()
    {
        StartCoroutine(HandlePowerButtonPress());
    }

    private IEnumerator HandlePowerButtonPress()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.P));
        while (CheckCondition())
        {
            tvMaterial.SetFloat("_Close", tvMaterial.GetFloat("_Close") + Time.deltaTime*(isPowerUp ? speed : -speed));
            yield return null;
        }
        isPowerUp ^= true;
        StartPowerButtonListening();
    }

    private bool CheckCondition()
    {
        return IsPowerDownAndValueMoreThanZero() || IsPowerUpAndValueLessThanMax();
    }
    private bool IsPowerDownAndValueMoreThanZero()
    {
        return tvMaterial.GetFloat("_Close") > 0f && !isPowerUp;
    }

    private bool IsPowerUpAndValueLessThanMax()
    {
        return tvMaterial.GetFloat("_Close") <50f && isPowerUp;
    }
}
