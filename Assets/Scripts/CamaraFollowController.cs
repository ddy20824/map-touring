using System.Collections;
using UnityEngine;

public class CamaraFollowController : MonoBehaviour
{
    public GameObject target;

    public float deadZone = 0;
    public bool followVertical = true;
    public bool followHorizontal = true;
    public float maxX = 0;
    public float minX = 0;
    public float maxHeight = 0;
    public float minHeight = 0;
    public bool focusOnPlayer = true;


    [SerializeField]
    private Transform[] routes;

    private int routeToGo;

    private float tParam;

    private Vector2 objectPosition;

    private float speedModifier;

    private bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.8f;
        coroutineAllowed = true;
        EventManager.instance.CameraMoveEvent += FocusOnRuneMap;
        if (target == null)
        {
            target = GameObject.Find("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && focusOnPlayer)
        {

            if (followHorizontal == true)
            {
                if (transform.position.x >= target.transform.position.x + deadZone)
                {
                    transform.position = new Vector3(target.transform.position.x + deadZone, transform.position.y, transform.position.z);
                }
                if (transform.position.x <= target.transform.position.x - deadZone)
                {
                    transform.position = new Vector3(target.transform.position.x - deadZone, transform.position.y, transform.position.z);
                }
            }

            if (followVertical == true)
            {
                if (transform.position.y >= target.transform.position.y + deadZone)
                {
                    transform.position = new Vector3(transform.position.x, target.transform.position.y + deadZone, transform.position.z);
                }
                if (transform.position.y <= target.transform.position.y - deadZone)
                {
                    transform.position = new Vector3(transform.position.x, target.transform.position.y - deadZone, transform.position.z);
                }
            }

            if (target.transform.position.x > maxX)
            {
                transform.position = new Vector3(maxX, target.transform.position.y, transform.position.z);
            }
            if (target.transform.position.x < minX)
            {
                transform.position = new Vector3(minX, target.transform.position.y, transform.position.z);
            }

            if (target.transform.position.y > maxHeight)
            {
                transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
            }

            if (target.transform.position.y < minHeight)
            {
                transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
            }
        }
        if (coroutineAllowed && !focusOnPlayer)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    void FocusOnRuneMap()
    {
        focusOnPlayer = false;
        StartCoroutine(Helper.Delay(UnFocusOnRuneMap, 2f));
    }
    void UnFocusOnRuneMap()
    {
        focusOnPlayer = true;

        tParam = 0f;
        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        coroutineAllowed = true;
    }

    private IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;

        Vector2 p0 = routes[routeNum].GetChild(0).position;
        Vector2 p1 = routes[routeNum].GetChild(1).position;
        Vector2 p2 = routes[routeNum].GetChild(2).position;
        Vector2 p3 = routes[routeNum].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = new Vector3(transform.position.x, objectPosition.y, transform.position.z);
            yield return new WaitForEndOfFrame();
        }

        EventManager.instance.TriggerMap2RuneActive();

    }
}
