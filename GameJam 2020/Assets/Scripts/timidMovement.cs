using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timidMovement : MonoBehaviour
{
    public float speedX, speedY, speedZ;
    public float rotateX, rotateY, rotateZ;
    public Transform Player;
    public float dist = 10f;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] private MeshRenderer timidMat;
    private MaterialPropertyBlock materialBlock;
    private bool flashing;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameManager.Instance.motor.transform;
        materialBlock = new MaterialPropertyBlock();

        rotateX = Random.Range(-10f, 10f) * Time.deltaTime;
        rotateY = Random.Range(-10f, 10f) * Time.deltaTime;
        rotateZ = Random.Range(-10f, 10f) * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player)
            return;

        transform.position -= transform.forward * movementSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, Player.position) <= dist)
        {
            if (!flashing)
                Flash(true);
        }
        else
        {
            transform.eulerAngles += new Vector3(rotateX, rotateY, rotateZ);

            if (flashing)
                Flash(false);
        }
    }

    private void Flash(bool on)
    {
        flashing = on;

        timidMat.GetPropertyBlock(materialBlock);

        if(on)
            materialBlock.SetColor("_EmissionColor", Color.white * 2);
        else
            materialBlock.SetColor("_EmissionColor", Color.white / 2);

        timidMat.SetPropertyBlock(materialBlock);
    }
}
