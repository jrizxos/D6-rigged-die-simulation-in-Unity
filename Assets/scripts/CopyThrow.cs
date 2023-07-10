using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyThrow : MonoBehaviour{

    Rigidbody m_Rigidbody;
    Transform m_transform;
    private Vector3 init_pos;
    private GameObject[] childsG;
    public Material m1, m2, m3, m4, m5, m6;

    void Start(){
        m_Rigidbody = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();
        init_pos = m_transform.position;
        childsG = new GameObject[m_transform.childCount];
        int i = 0;
        foreach (Transform child in m_transform) {
            childsG[i] = child.gameObject;
            i++;
        }
        //Debug.Log(m_transform.childCount);
        //Debug.Log(childsG);
    }

    public void ResetDie() {                               //resets this die
        //Debug.Log("copy die reset");                        //print debug message
        m_Rigidbody.velocity = Vector3.zero;                //clear forces
        m_Rigidbody.angularVelocity = Vector3.zero;
        m_Rigidbody.useGravity = false;                     //freeze position
        m_transform.position = init_pos;                    //move back to initial position
        m_transform.rotation = Quaternion.identity;
    }

    public void ThrowDie(float m_Thrust, float m_Angle, int max_child) {
        //Debug.Log("copy die thrown, speed=" + m_Thrust + ", angle=" + m_Angle);  //print debug message
        rearange(max_child);
        m_transform.eulerAngles = new Vector3(0f, m_Angle, 0f); //set angle
        m_Rigidbody.useGravity = true;                      //unfreeze
        m_Rigidbody.AddForce(transform.forward * m_Thrust); //add frorce to throw
    }

    private void rearange(int max_child) {
        ArrayList facelst = new ArrayList() { 1, 2, 3, 4, 5, 6 };
        if (max_child % 2 == 0) {
            facelst.Remove(max_child);
            facelst.Remove(max_child-1);
            childsG[max_child - 1].GetComponent<Renderer>().material = m6;
            childsG[max_child - 2].GetComponent<Renderer>().material = m1;
        }
        else {
            facelst.Remove(max_child);
            facelst.Remove(max_child + 1);
            childsG[max_child - 1].GetComponent<Renderer>().material = m6;
            childsG[max_child].GetComponent<Renderer>().material = m1;
        }
        //PrintValues(facelst);
        childsG[(int)facelst[0]-1].GetComponent<Renderer>().material = m5;
        childsG[(int)facelst[1]-1].GetComponent<Renderer>().material = m2;
        childsG[(int)facelst[2]-1].GetComponent<Renderer>().material = m4;
        childsG[(int)facelst[3]-1].GetComponent<Renderer>().material = m3;
    }

    public static void PrintValues(ArrayList myList) {
        foreach (int obj in myList)
            Debug.Log(obj);
        Debug.Log("\n");
    }
}
