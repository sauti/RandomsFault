using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
public class AnimUtils : MonoBehaviour
{
    public static IEnumerator MoveToTarget(GameObject go, Vector3 targetPosition, Quaternion targetRotation, float targetScale, float duration)
    {
        float time = 0;
        float startValue = 1;
        float scaleModifier = 1;
        Vector3 startPosition = go.transform.position;
        Quaternion startRotation = go.transform.rotation;
        Vector3 startScale = new Vector3(1, 1, 1);

        while (time < duration)
        {
            go.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            go.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);
            scaleModifier = Mathf.Lerp(startValue, targetScale, time / duration);
            go.transform.localScale = startScale * scaleModifier;
            time += Time.deltaTime;
            yield return null;
        }

        go.transform.position = targetPosition;
        go.transform.rotation = targetRotation;
        // go.transform.localScale = targetScale;
    }
}
}
