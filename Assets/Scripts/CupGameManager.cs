using Assets.Scripts.Helpers;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CupGameManager : MonoBehaviour
{
    private Animator anim;

    public UnityEvent AnimationFinished = new();
    public UnityEvent AnswerSuccess = new();
    public UnityEvent AnswerFailed = new();

    [SerializeField]
    private CupAnim[] cupAnimations;

    [SerializeField]
    private float animSpeed = 1.0f;
    [SerializeField]
    private Transform ball;

    [Header("Read Only Debug")]
    [SerializeField]
    private float nextAnimTime;
    [SerializeField]
    private int ballPostion = 1;

    [SerializeField]
    private bool started = false;
    private int movesToMake = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    [ContextMenu("StartGame")]
    public void TestStart() => StartCoroutine(StartGame(7));

    public IEnumerator StartGame(int moves)
    {
        RevealBall();
        yield return new WaitForSeconds(2.0f);
        movesToMake = moves;
        started = true;
        ball.gameObject.SetActive(false);

    }
    public IEnumerator SubmitAnswer(int answer)
    {
        RevealBall();
        yield return new WaitForSeconds(1.5f);
        if (ballPostion == answer)
        {
            AnswerSuccess?.Invoke();
        }
        else
        {
            AnswerFailed?.Invoke();
        }
    }

    [ContextMenu("Reveal")]
    private void RevealBall()
    {
        anim.speed = 1.0f;
        anim.Play("RevealBall" + ballPostion, 0);
    }

    private CupAnim currentAnim;
    private int lastIndex;
    void Update()
    {
        if (started == false || Time.time <= nextAnimTime)
            return;

        if (movesToMake == 0)
        {
            started = false;
            ball.gameObject.SetActive(true);
            return;
        }

        movesToMake--;
        int newIndex = Random.Range(0, cupAnimations.Length);

        if (newIndex == lastIndex)
            newIndex = MathExtensions.ClampWrapped(newIndex + 1, 0, cupAnimations.Length - 1);

        lastIndex = newIndex;

        currentAnim = cupAnimations[lastIndex];
        anim.speed = animSpeed;
        anim.Play(currentAnim.Name, 0);

        if (ballPostion == currentAnim.Cup1)
            ballPostion = currentAnim.Cup2;
        else if (ballPostion == currentAnim.Cup2)
            ballPostion = currentAnim.Cup1;

        nextAnimTime = Time.time + (0.5f / animSpeed);
        ball.localPosition = new Vector3(0, 0.04f, (ballPostion - 2) * 0.25f);
    }
}
[System.Serializable]
public class CupAnim
{
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public int Cup1 { get; set; }
    [field: SerializeField]
    public int Cup2 { get; set; }
}
