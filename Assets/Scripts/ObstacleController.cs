using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    //이동 속도 지정
    public float speed = 3f;

    
    float dir = 0;

    private void Update()
    {
        float x_max = -speed * Time.deltaTime;
        float x = speed * Time.deltaTime;

        if(transform.localPosition.x < -4f)
        {
            dir = 1;
        }
        if (transform.localPosition.x > 4f)
        {
            dir = 0;
        }

        switch (dir)
        {
            case 0:
                transform.Translate(new Vector2(x_max,0));
                break;

            case 1:
                transform.Translate(new Vector2(x,0));
                break;
        }
    }
}
