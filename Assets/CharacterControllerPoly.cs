using UnityEngine;

public class CharacterControllerPoly : MonoBehaviour
{
    private Rigidbody rb;
    public float movingForce = 5;
    public float rotatingTime = 2;
    bool movingH;
    bool movingV;
    public int characterNum;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude>0)
        {
            anim.SetBool("moving", true);

        }
        else
        {
            anim.SetBool("moving", false);

        }

        if (Input.GetAxis("JoystickH"+characterNum) > 0&&!movingV)
        {

            this.transform.forward = Vector3.Lerp(this.transform.forward,Vector3.forward, rotatingTime);
            movingH = true;
            rb.AddForce(Vector3.right * movingForce);
        }

        if (Input.GetAxis("JoystickH" + characterNum) < 0&&!movingV)
        {
            this.transform.forward = Vector3.Lerp(this.transform.forward, -Vector3.forward, rotatingTime);

            movingH = true;
            rb.AddForce(-Vector3.right * movingForce);
        }
      

        if (Input.GetAxis("JoystickV" + characterNum) < 0 && !movingH)
        {
            this.transform.right = Vector3.Lerp(this.transform.right, Vector3.forward, rotatingTime);


            rb.AddForce(Vector3.forward * movingForce);
            movingV = true;
        }

        if (Input.GetAxis("JoystickV" + characterNum) > 0 && !movingH )
        {

            this.transform.right = Vector3.Lerp(this.transform.right, -Vector3.forward, rotatingTime);

            rb.AddForce(-Vector3.forward * movingForce);
            movingV = true;
        }


        if (Input.GetAxis("JoystickV" + characterNum) == 0)
        {

            movingV = false;
        }
        if (Input.GetAxis("JoystickH" + characterNum) == 0)
        {

            movingH = false;
        }
    }
}
