using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public IEnumerator CameraShake(float shakeForce, float shakeDuration) {
        //camera shake
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while(elapsed < shakeDuration) {
            float x = Random.Range(-1, 1) * shakeForce;
            float y = Random.Range(-1, 1) * shakeDuration;

            transform.localPosition = new Vector3(x, y, transform.localPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }

    public Transform CameraPosition(Vector2 pos) {
        this.transform.position = pos;
        return this.gameObject.transform;
    }
}
