using System;
using System.Collections;
using UnityEngine;

public class FlipCards : MonoBehaviour
{
    public GameObject number;

    private bool _isFront;

    public void AnimatedFlipCard()
    {
        StartCoroutine(CardFlipping());
    }

    private IEnumerator CardFlipping()
    {
        for (var i = -180f; i <= 0; i += 10f)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            
            if (Math.Abs(i + 90f) < 0.1f)
            {
                FlipCard();
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void FlipCard()
    {
        if (_isFront) SetBack();
        else SetFront();
    }

    private void SetFront()
    {
        _isFront = true;
        number.SetActive(true);
    }

    private void SetBack()
    {
        _isFront = false;
        number.SetActive(false);
    }
}
