using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CardController : Singleton<CardController>
{
    [SerializeField] private Text leftText;
    [SerializeField] private Text rightText;

    //PARAMETERS FOR THE PATH FUNCTION
    [SerializeField] private Vector3[] toRightPath;
    [SerializeField] private Vector3[] toLeftPath;
    [SerializeField] private float duration = 10f;
    private int resolution = 10;

    [SerializeField] private Transform cardParent;
    [SerializeField] private GameObject card;
    [SerializeField] private CardStack leftCards;
    [SerializeField] private CardStack rightCards;
    [SerializeField] private Transform leftTop;
    [SerializeField] private Transform rightTop;
    [SerializeField] private float cardDistance = 0.1f;

    [SerializeField] private float multiplier = 0.2f;
    [SerializeField] private float coroutineTime = 0.1f;

    float timer;
    float pressTime;
    [SerializeField] private float waitTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        leftText.text = leftCards.Length().ToString();
        rightText.text = rightCards.Length().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float swipe;
        timer = Time.time - pressTime;
        swipe = TouchInput.Instance.swipeDelta;
        if (swipe > 0  && timer > waitTime && leftCards.lastObj() != null)
        {
            pressTime = Time.time;

            int i = (int)(swipe * multiplier);
            i = Mathf.Clamp(i, 0, leftCards.Length()-1);

            StartCoroutine(ExecuteAfterTime(i, false));
        }
        else if (swipe < 0  && timer > waitTime && rightCards.lastObj() != null)
        {
            pressTime = Time.time;

            int i = (int)(-swipe * multiplier);
            i = Mathf.Clamp(i, 0, rightCards.Length() - 1);

            StartCoroutine(ExecuteAfterTime(i, true));
        }
    }
    IEnumerator ExecuteAfterTime(int index, bool toLeft)
    {
        yield return new WaitForSeconds(coroutineTime);
        StartTransfer(index, toLeft);
    }
    void StartTransfer(int index, bool toLeft)
    {
        if (index < 0)
            return;
        else
        {
            TransferCard(toLeft);
            StartCoroutine(ExecuteAfterTime(--index, toLeft));
        }
    }

    void FollowPath(Transform obj, bool toLeft)
    {
        if (toLeft)
            obj.transform.DOLocalPath(toLeftPath, duration, PathType.CatmullRom, PathMode.Full3D, resolution, Color.green);
        else
            obj.transform.DOLocalPath(toRightPath, duration, PathType.CatmullRom, PathMode.Full3D, resolution, Color.green);
    }

    void TransferCard(bool toLeft)
    {
        GameObject obj;
        if (toLeft)
        {
            if (rightCards.lastObj() == null)
                return;
            obj = rightCards.Pop();
            FollowPath(obj.transform, toLeft);
            rightTop.position -= Vector3.up * cardDistance;
            obj.transform.DORotate(Vector3.forward * 180, duration, RotateMode.Fast);

            leftText.text = leftCards.Length().ToString();
            rightText.text = rightCards.Length().ToString();
            //toLeftPath[0] -= Vector3.up * cardDistance;
            //toRightPath[toRightPath.Length - 1] -= Vector3.up * cardDistance;
        }
        else
        {
            if (leftCards.lastObj() == null)
                return;
            obj = leftCards.Pop();
            FollowPath(obj.transform, toLeft);            
            leftTop.position -= Vector3.up * cardDistance;
            obj.transform.DORotate(Vector3.forward * -180, duration, RotateMode.Fast);

            rightText.text = rightCards.Length().ToString();
            leftText.text = leftCards.Length().ToString();
            //toRightPath[0] -= Vector3.up * cardDistance;
            //toLeftPath[toLeftPath.Length - 1] -= Vector3.up * cardDistance;
        }
        obj.AddComponent<Rigidbody>().useGravity = false;
    }

    public void InstantiateCard(bool isLeft)
    {
        if (isLeft)
        {
            GameObject obj = (GameObject)Instantiate(card, leftTop.position, leftTop.rotation, cardParent);
            leftTop.position += Vector3.up * cardDistance;
            obj.GetComponent<Card>().left = true;
            leftCards.Push(obj);
            leftText.text = leftCards.Length().ToString();
        }
        else
        {
            GameObject obj = (GameObject)Instantiate(card, rightTop.position,rightTop.rotation, cardParent);
            rightTop.position += Vector3.up * cardDistance;
            obj.GetComponent<Card>().left = false;
            rightCards.Push(obj);
            rightText.text = rightCards.Length().ToString();
        }
    }

    public void DestroyCard(bool isLeft)
    {
        if (isLeft)
        {
            if(leftCards.lastObj() != null)
            {
                Destroy(leftCards.Pop());
                leftTop.position -= Vector3.up * cardDistance;
                leftText.text = leftCards.Length().ToString();
            }

        }
        else
        {
            if(rightCards.lastObj() != null)
            {
                Destroy(rightCards.Pop());
                rightTop.position -= Vector3.up * cardDistance;
                rightText.text = rightCards.Length().ToString();
            }
        }
    }
    public int GetCardCount(bool left)
    {
        if (left)
            return leftCards.Length();
        else
            return rightCards.Length();
    }



    public void CatchCard(GameObject obj, bool isLeft)
    {
        if (obj.GetComponent<Rigidbody>() != null)
            Destroy(obj.GetComponent<Rigidbody>());
        if (isLeft)
        {
            leftCards.Push(obj);
            obj.GetComponent<Card>().left = true;
            //leftTop.position += Vector3.up * cardDistance;
            toLeftPath[toLeftPath.Length - 1] += Vector3.up * cardDistance;
            toRightPath[0] += Vector3.up * cardDistance;
        }
        else
        {
            rightCards.Push(obj);
            obj.GetComponent<Card>().left = false;
            //rightTop.position += Vector3.up * cardDistance;
            toRightPath[toRightPath.Length - 1] += Vector3.up * cardDistance;
            toLeftPath[0] += Vector3.up * cardDistance;
        }
    }
}
