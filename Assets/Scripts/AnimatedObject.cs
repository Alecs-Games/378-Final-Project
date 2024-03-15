using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedObject : MonoBehaviour
{
    public SpriteRenderer spr;
    public int animIndex = -1;
    public SpritesList[] animations;
    bool animating;
    bool looping;
    float animSpeed;
    int currFrameIndex;
    float lastFrameSwitch;
    public bool UIImage;
    public int playOnStartIndex = -1;

    ReturnAnimationValues returnAnimation = null;

    public void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        if (playOnStartIndex != -1)
        {
            Animate(playOnStartIndex, false, animations[playOnStartIndex].animationSpeed, true);
        }
    }

    public void Animate(int index, bool loop, float speed, bool moving)
    {
        if ((index != animIndex || speed != animSpeed) && returnAnimation == null)
        {
            animIndex = index;
            looping = loop;
            animSpeed = speed;
            currFrameIndex = 0;
            lastFrameSwitch = 0;
            animating = moving;
            SetSprite(animations[animIndex].sprites[0]);
        }
    }

    void SetSprite(Sprite sp)
    {
        spr.sprite = sp;
    }

    protected void Update()
    {
        lastFrameSwitch += Time.deltaTime;
        if (animating)
        {
            if (lastFrameSwitch > animSpeed)
            {
                //move next frame
                lastFrameSwitch = 0;
                if (currFrameIndex < animations[animIndex].sprites.Length - 1)
                {
                    currFrameIndex += 1;
                }
                else
                {
                    //On animation finished
                    if (looping)
                    {
                        //loop back around if loop is enabled
                        currFrameIndex = 0;
                    }
                    else
                    {
                        StopAnimating();
                    }
                }
                SetSprite(animations[animIndex].sprites[currFrameIndex]);
                //print(gameObject.name + " moved next frame");
            }
        }
    }

    public void StopAnimating()
    {
        animating = false;
        SetSprite(animations[animIndex].sprites[0]);
    }

    public class ReturnAnimationValues
    {
        int index;
        bool loop;
        float speed;
        bool moving;
        AnimatedObject parent;

        public ReturnAnimationValues(int index, bool loop, float speed, AnimatedObject parent)
        {
            this.index = index;
            this.loop = loop;
            this.speed = speed;
            this.moving = moving;
            this.parent = parent;
        }
    }
}
