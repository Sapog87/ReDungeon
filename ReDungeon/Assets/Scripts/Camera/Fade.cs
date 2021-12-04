using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public GameObject go;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        StartCoroutine(FadeBlackWhite(go));
    }

    private IEnumerator FadeBlackWhite(GameObject go)
    {
        const float FADE_SPEED = 0.7f;
        Color color = sr.color;

        while (color.a < 1)
        {
            float alpha = color.a + (FADE_SPEED * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, alpha);
            sr.color = color;
            yield return null;
        }

        // Пример
        go.GetComponent<Printerr>().print();

        while (color.a > 0)
        {
            float alpha = color.a - (FADE_SPEED * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, alpha);
            sr.color = color;
            yield return null;
        }
    }
}
