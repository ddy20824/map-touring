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
    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
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
    }
}
