using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour{

    //this cube's properties
    private Rigidbody m_Rigidbody;
    private Transform m_transform;
    private const float min_Trust = 500f;
    private const float max_Trust = 2000f;
    private const float max_Angle = 40f;
    private float m_Thrust, m_Angle;
    private Vector3 init_pos;
    private GameObject[] childsG;

    //copycat cube's properties
    public GameObject copy_cat;
    private CopyThrow copyScript;

    void Start(){
        m_Rigidbody = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();
        childsG = new GameObject[m_transform.childCount];
        int i = 0;
        foreach (Transform child in m_transform) {
            childsG[i] = child.gameObject;
            i++;
        }
        init_pos = m_transform.position;
        copyScript = copy_cat.GetComponent<CopyThrow>();
        Physics.IgnoreCollision(copy_cat.GetComponent<Collider>(),GetComponent<Collider>());
    }

    void Update(){
        if (Input.GetKeyDown("r")) {                        //reset input
            ResetDie();                                     //reset me
            copyScript.ResetDie();                          //reset copycat
        }
        else if (Input.GetKeyDown("w")) {                   //throw input
            ResetDie();                                     //reset me
            copyScript.ResetDie();                          //reset copycat
            ThrowDie();                                     //throw me
            Time.timeScale = 100.0f;
            StartCoroutine("ThrowCopyCat");
        }
        if (m_transform.position.x > 100  ||
            m_transform.position.x < -100 ||
            m_transform.position.y > 100  ||
            m_transform.position.y < -100 ||
            m_transform.position.z > 100  ||
            m_transform.position.z < -100 ) {
            ResetDie();                                     //reset me
            copyScript.ResetDie();                          //reset copycat
            Time.timeScale = 1.0f;
            StopCoroutine("ThrowCopyCat");
        }
    }

    IEnumerator ThrowCopyCat() {
        yield return new WaitForSeconds(.01f);
        while (m_Rigidbody.velocity != Vector3.zero) {
            yield return null;
        }
        int max_child = 0;
        float max_y = 0f; 
        for(int i=0; i<m_transform.childCount; i++ ) {
            if(childsG[i].transform.position.y> max_y) {
                max_y = childsG[i].transform.position.y;
                max_child = i;
            }
        }
        Time.timeScale = 1.0f;
        copyScript.ThrowDie(m_Thrust, m_Angle, max_child+1);
    }

    private void ResetDie() {                               //resets this die
        //Debug.Log("die reset");                             //print debug message
        m_Rigidbody.velocity = Vector3.zero;                //clear forces
        m_Rigidbody.angularVelocity = Vector3.zero;
        m_Rigidbody.useGravity = false;                     //freeze position
        m_transform.position = init_pos;                    //move back to initial position
        m_transform.rotation = Quaternion.identity;         
    }

    private void ThrowDie() {
        m_Thrust = Random.Range(min_Trust, max_Trust);      //get random thrust force
        m_Angle = Random.Range(-max_Angle, max_Angle);      //get random angle
        //Debug.Log("die thrown, speed=" + m_Thrust + ", angle=" + m_Angle);  //print debug message
        m_transform.eulerAngles = new Vector3(0f, m_Angle, 0f); //set angle
        m_Rigidbody.useGravity = true;                      //unfreeze
        m_Rigidbody.AddForce(transform.forward * m_Thrust); //add frorce to throw
    }
}
