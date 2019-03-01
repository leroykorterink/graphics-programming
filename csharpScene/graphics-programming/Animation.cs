﻿using System;

namespace graphics_programming
{
    class AnimationControl
    {
        private Animation _currentAnimation;

        public AnimationControl()
        {
            _currentAnimation = new ScaleUp();
        }

        public void Update(CubeControlValues cubeControlValues)
        {
            _currentAnimation.Calculate(this, cubeControlValues);
        }

        public void SetAnimation(Animation animation)
        {
            _currentAnimation = animation;
        }
    }

    class ScaleUp : Animation
    {
        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CubeScale >= 1.5F)
            {
                animationControl.SetAnimation(new ScaleDown());
                return;
            }

            values.CubeScale += .01F;
            values.CameraThetaY--;
        }
    }

    class ScaleDown : Animation
    {
        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CubeScale <= 1F)
            {
                animationControl.SetAnimation(new RotateXForward());
                return;
            }

            values.CubeScale -= .01F;
            values.CameraThetaY--;
        }
    }

    class RotateXForward : Animation
    {
        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CubeThetaX >= 45)
            {
                animationControl.SetAnimation(new RotateXBackward());
                return;
            }

            values.CubeThetaX++;
            values.CameraThetaY--;
        }
    }

    class RotateXBackward : Animation
    {
        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CubeThetaX <= 0)
            {
                animationControl.SetAnimation(new RotateYForward());
                return;
            }

            values.CubeThetaX--;
            values.CameraThetaY--;
        }
    }

    class RotateYForward : Animation
    {
        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CubeThetaY >= 45)
            {
                animationControl.SetAnimation(new RotateYBackward());
                return;
            }

            values.CubeThetaY++;
            values.CameraThetaX++;
        }
    }

    class RotateYBackward : Animation
    {
        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CubeThetaY <= 0)
            {
                animationControl.SetAnimation(new ResetCameraPosition());
                return;
            }

            values.CubeThetaY--;
            values.CameraThetaX++;
        }
    }

    class ResetCameraPosition : Animation
    {
        CubeControlValues defaultValues = new CubeControlValues();

        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CameraThetaX == defaultValues.CameraThetaX && values.CameraThetaY == defaultValues.CameraThetaY) {
               animationControl.SetAnimation(new ScaleUp());
            }

            if (values.CameraThetaX != defaultValues.CameraThetaX)
                values.CameraThetaX += values.CameraThetaX < defaultValues.CameraThetaX ? 1F : -1F;

            if (values.CameraThetaY != defaultValues.CameraThetaY)
                values.CameraThetaY += values.CameraThetaY < defaultValues.CameraThetaY ? 1F : -1F;
        }
    }

    interface Animation
    {
        void Calculate(AnimationControl animationControl, CubeControlValues values);
    }
}