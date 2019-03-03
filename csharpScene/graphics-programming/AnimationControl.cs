using System;

namespace graphics_programming
{
    class AnimationControl
    {
        public IAnimation CurrentAnimation { get; private set; }

        public AnimationControl()
        {
            CurrentAnimation = new ScaleUp();
        }

        public void Update(CubeControlValues cubeControlValues)
        {
            CurrentAnimation.Calculate(this, cubeControlValues);
        }

        public void SetAnimation(IAnimation animation)
        {
            CurrentAnimation = animation;
        }
    }

    interface IAnimation
    {
        void Calculate(AnimationControl animationControl, CubeControlValues values);
    }

    class ScaleUp : IAnimation
    {
        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CubeScale >= 1.5F)
            {
                animationControl.SetAnimation(new ScaleDown());
                return;
            }

            values.CubeScale += .01F;
            values.CameraTheta--;
        }
    }

    class ScaleDown : IAnimation
    {
        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CubeScale <= 1F)
            {
                animationControl.SetAnimation(new RotateXForward());
                return;
            }

            values.CubeScale -= .01F;
            values.CameraTheta--;
        }
    }

    class RotateXForward : IAnimation
    {
        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CubeThetaX >= 45)
            {
                animationControl.SetAnimation(new RotateXBackward());
                return;
            }

            values.CubeThetaX++;
            values.CameraTheta--;
        }
    }

    class RotateXBackward : IAnimation
    {
        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CubeThetaX <= 0)
            {
                animationControl.SetAnimation(new RotateYForward());
                return;
            }

            values.CubeThetaX--;
            values.CameraTheta--;
        }
    }

    class RotateYForward : IAnimation
    {
        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CubeThetaY >= 45)
            {
                animationControl.SetAnimation(new RotateYBackward());
                return;
            }

            values.CubeThetaY++;
            values.CameraPhi++;
        }
    }

    class RotateYBackward : IAnimation
    {
        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CubeThetaY <= 0)
            {
                animationControl.SetAnimation(new ResetCameraPosition());
                return;
            }

            values.CubeThetaY--;
            values.CameraPhi++;
        }
    }

    class ResetCameraPosition : IAnimation
    {
        CubeControlValues defaultValues = new CubeControlValues();

        public void Calculate(AnimationControl animationControl, CubeControlValues values)
        {
            if (values.CameraTheta == defaultValues.CameraTheta && values.CameraPhi == defaultValues.CameraPhi) {
               animationControl.SetAnimation(new ScaleUp());
            }

            if (values.CameraTheta != defaultValues.CameraTheta)
                values.CameraTheta += values.CameraTheta < defaultValues.CameraTheta ? 1F : -1F;

            if (values.CameraPhi != defaultValues.CameraPhi)
                values.CameraPhi += values.CameraPhi < defaultValues.CameraPhi ? 1F : -1F;
        }
    }
}
