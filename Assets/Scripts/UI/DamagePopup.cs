using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rougelee
{
    public class DamagePopup : MonoBehaviour
    {

        public static DamagePopup Create(Vector3 position, int damageAmount)
        {
            Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);
            DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
            damagePopup.Setup(damageAmount);

            return damagePopup;
        }

        private const float DISAPPEAR_TIME_MAX = 1f;
        private TextMeshPro textMesh;
        private float disappearTimer;
        private Color textColor;
        private Vector3 moveVector;

        private void Awake()
        {
            textMesh = transform.GetComponent<TextMeshPro>();
        }
        public void Setup(int damageAmount)
        {
            textMesh.SetText(damageAmount.ToString());
            // TODO: placeholder. should probably make it autoscale instead of using ifs
            // to change color and size of bigger numbers
            if(damageAmount > 40)
            {
                textMesh.color = Color.red;
            }
            else
            {
                textMesh.color = Color.yellow;
            }

            textMesh.fontSize *= damageAmount/10;
            if(textMesh.fontSize < 1)
            {
                textMesh.fontSize = 1;
            }
            textColor = textMesh.color;
            disappearTimer = .75f;

            moveVector = new Vector3(.5f, 1) * .5f;
        }

        private void FixedUpdate()
        {
            //creates a bouncy effect
            transform.position += moveVector * Time.deltaTime;
            moveVector -= moveVector * 5f * Time.deltaTime;

            //increase size during first half, decrease during second
            if(disappearTimer > DISAPPEAR_TIME_MAX * .75f)
            {
                float increaseScaleAmount = 1f;
                transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
            }
            else
            {
                float decreaseScaleAmount = 1f;
                transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
            }

            //after disappearTime seconds, damagePopup will begin to fade away. disappears once completely transparent
            disappearTimer -= Time.deltaTime;
            if (disappearTimer < 0)
            {
                float disappearSpeed = 3f;
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;
                if (textColor.a < 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}