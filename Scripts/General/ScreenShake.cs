using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

    // Coroutine for screen shake
    public IEnumerator Shake(float duration, float magnitude) {
        Quaternion originalRotation = transform.localRotation;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1.0f, 1.0f) * magnitude;
            float y = Random.Range(-1.0f, 1.0f) * magnitude;

            transform.localRotation = new Quaternion(x, y, originalRotation.z, originalRotation.w);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localRotation = originalRotation;
    }
}
