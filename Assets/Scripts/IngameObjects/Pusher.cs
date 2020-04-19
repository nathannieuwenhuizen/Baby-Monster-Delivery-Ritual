using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pusher : MonoBehaviour
{

    //time
    [SerializeField]
    private float pushTime = 1f;
    [SerializeField]
    private float betweenPushAndPullTime = .5f;
    [SerializeField]
    private float pullTime = 3f;
    [SerializeField]
    private float waitTime = 5f;
    [SerializeField]
    private float rumbleTime = 1f;


    //distances
    [SerializeField]
    private float reachDistance;
    private float startDistance;


    //curves for animation
    [SerializeField]
    private AnimationCurve pushCurve;
    [SerializeField]
    private AnimationCurve pullCurve;


    //sounds
    private AudioSource audioS;
    [SerializeField]
    private AudioClip rumbleClip;
    [SerializeField]
    private AudioClip pushClip;

    private float outTime = 1f;
    [SerializeField]
    private float rumbleDistance = 0.5f;

    private bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        startDistance = transform.localPosition.x;
        StartCoroutine(Loop());

        audioS = GetComponent<AudioSource>();
    }

    IEnumerator Loop()
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(Rumble());
            yield return StartCoroutine(Push());
            yield return new WaitForSeconds(betweenPushAndPullTime);
            yield return StartCoroutine(Pull());
            yield return new WaitUntil(delegate { return GameManager.instance.state == gameState.running; });
        }

    }

    public void PlaySound(AudioClip clip, float volume, bool forcePlay = false)
    {
        if (!audioS.isPlaying || forcePlay)
        {
            audioS.clip = clip;
            audioS.volume = volume;
            audioS.Play();
        }
    }

    IEnumerator Rumble()
    {
        PlaySound(rumbleClip, .5f, true);
        float currentTime = 0;
        while(currentTime < rumbleTime)
        {
            currentTime += Time.deltaTime;
            SetPosition(Random.Range(-rumbleDistance, rumbleDistance));
            yield return new WaitForFixedUpdate();
        }
    }

    public void SetPosition(float val)
    {
        transform.localPosition = new Vector3(startDistance + val, transform.localPosition.y, transform.localPosition.z);
    }

    IEnumerator Push()
    {
        PlaySound(pushClip, .5f, true);

        float index = 0;
        float current = 0;
        while (index < 1)
        {
            index += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            current = Mathf.Lerp(startDistance, reachDistance, pushCurve.Evaluate(index));
            current =  reachDistance * pushCurve.Evaluate(index);
            SetPosition(current);
        }
    }

    IEnumerator Pull()
    {
        float index = 0;
        float current = 0;
        while (index < 1)
        {
            index += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            current = Mathf.Lerp(reachDistance, 0, pullCurve.Evaluate(index));
            SetPosition(current);
        }
    }

    void OnDrawGizmosSelected()
    {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(reachDistance,0,0));
    }

}
