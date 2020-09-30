using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopup : MonoBehaviour
{
    private TextMeshPro textMesh;


    private Animator _animator;

    public Animator animator
    {
        get
        {
            if (_animator == null)
                _animator = this.GetComponent<Animator>();

            return _animator;
        }
    }



    public static void Create(Vector3 myPosition, string myText, bool directhit = false)
    {
        Transform popUpTransform = Instantiate(SpawnManager.instance.TextEffect, myPosition, Quaternion.identity);
        TextPopup textPopUp = popUpTransform.GetComponent<TextPopup>();
        textPopUp.SetText(myText);

        if(directhit)
            textPopUp.animator.Play("DirectHit");
        else
            textPopUp.animator.Play("GlancingHit");

    }

    public void Awake()
    {
        textMesh = transform.GetComponentInChildren<TextMeshPro>();
    }

    public void SetText(string text)
    {
        textMesh.SetText(text.ToString());
    }

    public void AnimationFinished()
    {
        Destroy(this.transform.gameObject);
    }
}
