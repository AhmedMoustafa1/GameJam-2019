using UnityEngine;

public class CharacterControllerPoly : MonoBehaviour
{
    private Rigidbody rb;
    public float movingForce = 5;
    public float rotatingTime = 2;
    bool movingH;
    bool movingV;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            this.transform.forward = Vector3.Lerp(this.transform.forward,Vector3.forward, rotatingTime);
            movingH = true;
            rb.AddForce(Vector3.right * movingForce);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            this.transform.forward = Vector3.Lerp(this.transform.forward, -Vector3.forward, rotatingTime);
               

            movingH = true;
            rb.AddForce(-Vector3.right * movingForce);
        }
      

        if (Input.GetAxis("Vertical") > 0 && !movingH)
        {
            this.transform.right = Vector3.Lerp(this.transform.right, Vector3.forward, rotatingTime);


            rb.AddForce(Vector3.forward * movingForce);
            movingV = true;
        }

        if (Input.GetAxis("Vertical") < 0 && !movingH)
        {
            this.transform.right = Vector3.Lerp(this.transform.right, -Vector3.forward, rotatingTime);

            rb.AddForce(-Vector3.forward * movingForce);
            movingV = true;
        }


        if (Input.GetAxis("Vertical") == 0)
        {
            movingV = false;
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            movingH = false;
        }
    }
}
