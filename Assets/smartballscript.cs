using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class smartballscript : MonoBehaviour {

    float power = 0f;
    GameObject[] cubes = new GameObject[5];
    Vector3[] moves = new Vector3[5];
    public Text score;

	// Use this for initialization
	void Start () {
        moves[0] = new Vector3(0f, 1f, 0f);
        moves[1] = new Vector3(-3f, 1f, 5f);
        moves[2] = new Vector3(-3f, 1f, 5f);
        moves[3] = new Vector3(-3f, 1f, -3f);
        moves[4] = new Vector3(3f, 1f, -3f);
        for (int i = 0; i < 5; i++)
        {
            cubes[i] = GameObject.Find("BoardCube" + i);
            Vector3 move = cubes[i].transform.position;
            AnimationClip clip = new AnimationClip();
            clip.legacy = true;
            Keyframe[] keysX = new Keyframe[2];
            keysX[0] = new Keyframe(0f, move.x - 3);
            keysX[1] = new Keyframe(i + 1f, move.x);
            AnimationCurve curveX = new AnimationCurve(keysX);
            clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
            Keyframe[] keysY = new Keyframe[2];
            keysY[0] = new Keyframe(0f, move.y);
            keysY[1] = new Keyframe(i + 1f, move.y);
            AnimationCurve curveY = new AnimationCurve(keysY);
            clip.SetCurve("", typeof(Transform), "localPosition.x", curveX);
            Keyframe[] keysZ = new Keyframe[2];
            keysZ[0] = new Keyframe(0f, move.z);
            AnimationCurve curveZ = new AnimationCurve(keysZ);
            clip.SetCurve("", typeof(Transform), "localPosition.z", curveZ);
            cubes[i].GetComponent<Animation>().AddClip(clip, "clip1");
            cubes[i].GetComponent<Animation>().Play("clip1");
        }
    }
	
	// Update is called once per frame
	void Update () {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        Renderer renderer = GetComponent<Renderer>();

        MoveCube();

        rigidbody.AddForce(0f, 0f, -1f);
        if (Input.GetKey(KeyCode.Space))
        {
            power += 0.01f;
            if(power > 1f)
            {
                power = 1f;
            }
            renderer.material.color = new Color(1f, power, 0f);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rigidbody.AddForce(new Vector3(0f, 0f, power * 2500f));
            power = 0f;
            renderer.material.color = Color.red;
        }
    }

    void MoveCube()
    {
        for(int i = 0; i < 5; i++)
        {
            cubes[i].transform.Rotate(new Vector3(0f, 2.5f, 0f));
        }
    }
}
