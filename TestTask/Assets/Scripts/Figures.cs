using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Figures : MonoBehaviour
{
    private Rigidbody rb;
    private Collider cl;

    private Plane dragPlane;
    private Camera mainCamera;

    private Vector3 offset;
    private Vector3 startPosition;

    private float startZ = -0.3f;
    private float inGameZ = 0f;
    private float transition = -0.1f;

    private bool collision = false;
    private bool falled = false;
    private bool falled1 = false;
    private bool move = true;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cl = GetComponent<Collider>();
        mainCamera = Camera.main;
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (falled && move)
        {
            if(rb.velocity.x == 0 && rb.velocity.y == 0 && rb.velocity.z == 0 && !falled1)
            {
                UIController.BarOn?.Invoke();
            }
            else
            {
                UIController.BarOff?.Invoke();
            }
        }
    }

    private void OnMouseDown()
    {
        if(!falled)
        UIController.SetNewGO(gameObject);
        dragPlane = new Plane(mainCamera.transform.forward, transform.position);
        Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        offset = transform.position - camRay.GetPoint(planeDist);
    }

    private void OnMouseDrag()
    {
        if (!falled)
        {
            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            float planeDist;
            dragPlane.Raycast(camRay, out planeDist);
            Vector3 newPos = camRay.GetPoint(planeDist) + offset;
            if (transform.position.y > transition)
            {
                transform.position = new Vector3(newPos.x, newPos.y, inGameZ);
            }
            else
            {
                transform.position = new Vector3(newPos.x, newPos.y, startZ);
            }
        }
    }

    private void OnMouseUp()
    {
        if(!falled)
        if (!collision)
        {
            rb.isKinematic = false;
            cl.isTrigger = false;
            StartCoroutine(FallWaiter());
        }
        else
        {
            transform.DOMove(startPosition,0.4f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collision = true;
    }
    private void OnTriggerExit(Collider other)
    {
        collision = false;
    }
    IEnumerator FallWaiter()
    {
        UIController.BarOn?.Invoke();
        yield return new WaitForSeconds(0.1f);
        falled = true;
        gameObject.tag = "Falled";
    }

    public void MoveAbilityOff()
    {
        move = false;
    }
}
