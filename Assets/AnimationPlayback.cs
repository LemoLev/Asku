using System.Collections;
using UnityEngine;

public class AnimationPlayback : MonoBehaviour
{
    public string aN, sheetname;
    public float fp;
    private void Start()
    {
        StartCoroutine(PlayAnim(fp, aN, sheetname));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StopAllCoroutines();
            aN = "Left";
            StartCoroutine(PlayAnim(fp, aN, sheetname));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            StopAllCoroutines();
            aN = "Right";
            StartCoroutine(PlayAnim(fp, aN, sheetname));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StopAllCoroutines();
            aN = "Up";
            StartCoroutine(PlayAnim(fp, aN, sheetname));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StopAllCoroutines();
            aN = "Down";
            StartCoroutine(PlayAnim(fp, aN, sheetname));
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            StopAllCoroutines();
            aN = "Idle";
            StartCoroutine(PlayAnim(fp, aN, sheetname));
        }
    }

    IEnumerator PlayAnim(float fps, string animName, string sName)
    {
        while (true)
        {
            foreach (Sprite s in Resources.LoadAll<Sprite>(sName))
            {
                if (s.name.Contains(animName))
                {
                    GetComponent<SpriteRenderer>().sprite = s;
                    yield return new WaitForSeconds(1 / fps);
                }
            }
        }
    }
}