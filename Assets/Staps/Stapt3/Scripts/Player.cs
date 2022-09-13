using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;


public class Player : MonoBehaviour
{
    public List<Transform> Tails;
    [Range(0, 3)]
    public float BonesDistance;
    public GameObject BonePrefab;
    [Range(0, 4)]
    public float Speed;
    private Transform _transform;
    public float hp = 1;
    public Scrollbar ScrollbarHp;
    public GameObject endText;
    public GameObject Camera2;

    private void Start()
    {
        _transform = GetComponent<Transform>();

    }

    private void Update()
    {

        MoveSnake(_transform.position + _transform.forward * Speed);

        float angel = Input.GetAxis("Horizontal") * 1;
        _transform.Rotate(0, angel, 0);
    }

    private void MoveSnake(Vector3 newPosition)
    {
        float sqrDistance = BonesDistance * BonesDistance;
        Vector3 previosPosition = _transform.position;

        foreach (var bone in Tails)
        {
            if ((bone.position - previosPosition).sqrMagnitude > sqrDistance)
            {
                var temp = bone.position;
                bone.position = previosPosition;
                previosPosition = temp;
            }


            else
            {
                break;
            }
        }

        _transform.position = newPosition;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sector")
        {
            Destroy(collision.gameObject);

            var bone = Instantiate(BonePrefab);
            Tails.Add(bone.transform);
            hp += 0.2f;
            ScrollbarHp.size = hp;

        }
        if (collision.gameObject.tag == "BadSector")
        {
            
            hp -= 0.2f;
            ScrollbarHp.size = hp;
            
            if (hp < 0.1f)
            {
                Death();
            }

        }
        if (collision.gameObject.tag == "VeryBadSector")
        {

            hp -= 0.5f;
            ScrollbarHp.size = hp;

            if (hp < 0.1f)
            {
                Death();
            }

        }




    }
    private void Death()
    {
        endText.SetActive(true);
        Camera2.SetActive(true);
    }

    
}
