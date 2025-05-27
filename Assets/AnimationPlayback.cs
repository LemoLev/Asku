using System.Collections;
using UnityEngine;

public class AnimationPlayback : MonoBehaviour
{
    Sprite[] sprites;
    private void ResetAndContinue(float fps, string animName, string sName)
    {
        StopAllCoroutines();
        StartCoroutine(ActPlay(fps, animName, sName));
    }
    public void PlayAnim(float fps, string animName, string sName)
    {
        sprites = Resources.LoadAll<Sprite>(sName);
        ResetAndContinue(fps, animName, sName);
    }

    private IEnumerator ActPlay(float fps, string animName, string sName)
    { 
        while (true)
        {
            foreach (Sprite s in sprites)
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