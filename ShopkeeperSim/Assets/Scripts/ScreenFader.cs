using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class ScreenFader : UIElementController
{

    IEnumerator fadeCoroutine;

    // TODO: Implement the rest of this class
    protected virtual IEnumerator Fade(float targetOpacity, float duration)
    {
        float countdown =                           duration;

        while (countdown > 0)
        {
            countdown -=                            Time.deltaTime;
            opacity =                               Mathf.Lerp(opacity, targetOpacity, countdown / duration);
            yield return null;
        }
    }
	
}
