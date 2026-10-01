using UnityEngine;

public class BrokenPieces : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 moveDirection;
    public float deceleration;
    public float lifeTime=3f;
    public SpriteRenderer theSR;
    public float fadeTime;
    void Start()
    {
        moveDirection.x=Random.Range(-moveSpeed,moveSpeed);
        moveDirection.y=Random.Range(-moveSpeed,moveSpeed);
    }

    
    void Update()
    {
        transform.position+=moveDirection*Time.deltaTime;
        moveDirection=Vector3.Lerp(moveDirection,Vector3.zero,deceleration*Time.deltaTime);
        lifeTime-=Time.deltaTime;
        if (lifeTime <= 0)
        {
            theSR.color=new Color(theSR.color.r,theSR.color.g,theSR.color.b,Mathf.MoveTowards(theSR.color.a,0f,fadeTime*Time.deltaTime));
            if (theSR.color.a == 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
