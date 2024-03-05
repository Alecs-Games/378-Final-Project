using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Camera cam;
    public float speed;
    private Vector2 deadzone = new Vector2(0.75f, 0.75f);
    bool zooming;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        transform.position =
            GameManager.instance.player.transform.position
            + new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.player != null)
        {
            Vector2 diff = GameManager.instance.player.transform.position - transform.position;
            if (
                diff.x >= deadzone.x
                || diff.y >= deadzone.y
                || diff.x <= -deadzone.x
                || diff.y <= -deadzone.y
            )
            {
                Vector2 newpos = Vector2.MoveTowards(
                    transform.position,
                    GameManager.instance.player.transform.position,
                    speed * Time.deltaTime
                );
                transform.position = new Vector3(newpos.x, newpos.y, transform.position.z);
            }
        }
    }
}
