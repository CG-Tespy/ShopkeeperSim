using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteColorRandomizer : MonoBehaviour 
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] bool randomizeOnAwake =        true;

    const float opaque =                            1.0f;

	// Use this for self-initialization
	void Awake () 
	{
        if (randomizeOnAwake)
            RandomizeColor();
	}

    public void RandomizeColor()
    {
        float red =                                 Random.value;
        float green =                               Random.value;
        float blue =                                Random.value;
        Color newColor =                            new Color(red, green, blue, opaque);
        spriteRenderer.color =                      newColor;
    }
	
}
