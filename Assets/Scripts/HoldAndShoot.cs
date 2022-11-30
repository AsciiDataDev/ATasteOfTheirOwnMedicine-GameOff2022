using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HoldAndShoot : MonoBehaviour
{
    public Animator handAnimator;
    public AudioSource fingerFlickSound;
    private AudioSource grabSound;
    public LayerMask holdableLayer, shootableLayer;
    public Transform holdingPosition, rayTransformOrigin;

    RaycastHit hit;
    GameObject heldObject;
    Rigidbody heldObjectRigidBody;
    TrailRenderer heldObjectTrailRenderer;
    bool isHolding;
    [HideInInspector] public bool isPaused;
    private int NoCollisionLayer, ShotLayer, GrabLayer;
    private GameObject grabIndicator;

    void Awake()
    {
        NoCollisionLayer = LayerMask.NameToLayer("NoCollision");
        ShotLayer = LayerMask.NameToLayer("Shot");
        GrabLayer = LayerMask.NameToLayer("Grab");
        grabIndicator = GameObject.Find("GrabIndicator");

        AudioSource[] unscaledSource = GameObject.Find("UnscaledAudioSources").GetComponents<AudioSource>();
        foreach(AudioSource x in unscaledSource){
            if(x.clip.name == "grab"){
                grabSound = x;
            }
        }
    }
    void Update()
    {

        if (Physics.Raycast(rayTransformOrigin.position, rayTransformOrigin.forward, 3f, holdableLayer) && !isHolding)
        {
            grabIndicator.SetActive(true);
        }
        else if(grabIndicator.activeSelf)
        {
            grabIndicator.SetActive(false);
        }


        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !isHolding && Physics.Raycast(rayTransformOrigin.position, rayTransformOrigin.forward, out hit, 3f, holdableLayer))
            {
                Grab();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && isHolding)
            {
                Release();
            }
        }
    }

    void Grab()
    {
        SetHoldVariables();

        heldObjectRigidBody.interpolation = RigidbodyInterpolation.None;
        heldObject.transform.position = holdingPosition.position;
        heldObject.transform.rotation = holdingPosition.rotation;
        heldObject.transform.SetParent(holdingPosition);
        heldObjectRigidBody.isKinematic = true;
        heldObjectTrailRenderer.emitting = false;
        handAnimator.Play("Hold");
        grabSound.Play();
        heldObject.layer = NoCollisionLayer;
    }

    void SetHoldVariables()
    {
        isHolding = true;
        heldObject = hit.transform.gameObject;

        heldObjectRigidBody = heldObject.GetComponent<Rigidbody>();
        heldObjectTrailRenderer = heldObject.GetComponentInChildren<TrailRenderer>();
    }

    void Release()
    {
        heldObjectRigidBody.interpolation = RigidbodyInterpolation.Interpolate;
        heldObjectTrailRenderer.emitting = true;
        heldObjectRigidBody.isKinematic = false;
        heldObject.transform.SetParent(null);
        handAnimator.Play("Flick");

        Shoot();

        ChangeObjectLayers();

        ResetHoldVariables();
    }

    void ResetHoldVariables()
    {
        heldObjectTrailRenderer = null;
        heldObjectRigidBody = null;
        heldObject = null;
        isHolding = false;
    }

    void Shoot()
    {
        RaycastHit directionHit;
        Vector3 locationToPoint;
        if (Physics.Raycast(rayTransformOrigin.position, rayTransformOrigin.forward, out directionHit, Mathf.Infinity, shootableLayer))
        {
            locationToPoint = directionHit.point;
        }
        else
        {
            locationToPoint = rayTransformOrigin.position + (rayTransformOrigin.forward * 500);
        }

        heldObject.transform.LookAt(locationToPoint);

        heldObjectRigidBody.AddForce(heldObject.transform.forward * 2700);
        fingerFlickSound.Play();
    }


    //for avoiding player from holding bullet again, and becomes damageable to enemies
    void ChangeObjectLayers()
    {
        heldObject.layer = ShotLayer;
        foreach (Transform x in heldObject.GetComponentInChildren<Transform>())
        {
            if (x.gameObject.layer == GrabLayer)
            {
                x.gameObject.layer = NoCollisionLayer;
                break;
            }
        }
    }

}
