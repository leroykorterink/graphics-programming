using System;
using System.Drawing;
using System.Windows.Forms;

namespace graphics_programming
{
    class CubeControlValues {
        // Camera properties
        public float CameraR = 10;            // R / r
        public float CameraDistance = 800;    // D / d
        public float CameraTheta = -100;      // T / t
        public float CameraPhi = -10;         // P / p

        // Cube properties
        public float CubeScale = 1;           // S / s

        public float CubeX = 0;               // Left / Right
        public float CubeY = 0;               // Up / Down
        public float CubeZ = 0;               // PageUp / PageDown

        public float CubeThetaX = 0;          // X / x
        public float CubeThetaY = 0;          // Y / y
        public float CubeThetaZ = 0;          // Z / z
    }

    /// <summary>
    /// Starting parameters
    /// - d = 800
    /// - r = 10
    /// - theta = -100
    /// - phi = -10
    /// 
    /// Handle keys
    /// - Cursor keys: change x/y [Done]
    /// - PgUp / PgDn : decrease / increase z [Done]
    /// - x/X, y/Y, z/Z : rotate around x-axis, y-axis, z-axis [Done]
    /// - s/S : scale(increase / decrease) [Done]
    /// 
    /// All stepsizes are 0.1
    /// 
    /// When “C” is pressed, all variables are reset to default
    /// 
    /// When “A” is pressed, start animation
    ///     Phases
    ///     - Phase 1: Scale until 1.5x and shrink(stepsize 0.01)
    ///     - Phase 2: Rotate 45° over X-axis and back
    ///     - Phase 3: Rotate 45° over Y-axis and back
    ///     - During phase 1-2, decrease theta
    ///     - During phase 3, increase phi
    ///     - After phase 3, increase theta and decrease phi until starting values. Then start with phase 1
    ///     
    ///     If no stepsize is mentioned, use stepsize 1
    /// 
    /// Hints to get more insight:
    /// - Display all parameters in the window
    /// - Make more events to change all parameters manually (e.g.r/R for r, t/T for theta, etc.)
    /// </summary>
    class CubeControls
    {
        public CubeControlValues Values = new CubeControlValues();

        public delegate void OnChangeHandler();
        public OnChangeHandler OnChange;

        private readonly Brush _brush = new SolidBrush(Color.Black);
        private readonly Font _font = new Font("Arial", 9, FontStyle.Bold);
        private readonly Point _position = new Point(0, 0);

        private readonly Timer _timer;
        private readonly AnimationControl _animationControl = new AnimationControl();

        public CubeControls()
        {
            _timer = new Timer
            {
                Interval = 50
            };

            _timer.Tick += OnTick;
        }

        private void OnTick(object sender, EventArgs e)
        {
            _animationControl.Update(Values);

            OnChange();
        }

        public void KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                #region Camera properties

                case Keys.R:
                    Values.CameraR += e.Shift ? -1F : 1F;
                    break;

                case Keys.D:
                    Values.CameraDistance += e.Shift ? -5F : 5F;
                    break;

                case Keys.P:
                    Values.CameraPhi += e.Shift ? -1F : 1F;
                    break;

                case Keys.T:
                    Values.CameraTheta += e.Shift ? -1F : 1F;
                    break;

                #endregion

                #region Translate cube

                case Keys.Right:
                    Values.CubeX -= .1F;
                    break;

                case Keys.Left:
                    Values.CubeX += .1F;
                    break;

                case Keys.Up:
                    Values.CubeY += .1F;
                    break;

                case Keys.Down:
                    Values.CubeY -= .1F;
                    break;

                case Keys.PageUp:
                    Values.CubeZ += .1F;
                    break;

                case Keys.PageDown:
                    Values.CubeZ -= .1F;
                    break;

                #endregion

                #region Rotate cube

                case Keys.X:
                    Values.CubeThetaX += e.Shift ? -1F : 1F;
                    break;

                case Keys.Y:
                    Values.CubeThetaY += e.Shift ? -1F : 1F;
                    break;

                case Keys.Z:
                    Values.CubeThetaZ += e.Shift ? -1F : 1F;
                    break;

                #endregion

                #region Scale cube

                // Scale cube
                case Keys.S:
                    Values.CubeScale += e.Shift ? -0.01F : 0.01F;
                    break;

                #endregion

                #region Rendering properties

                // Toggle animation
                case Keys.A:
                    _timer.Enabled = !_timer.Enabled;
                    break;

                // Reset values
                case Keys.C:
                    _timer.Enabled = false;
                    Values = new CubeControlValues();
                    break;

                #endregion

                default:
                    Console.WriteLine("Invalid command");
                break;
            }

            OnChange();
        }

        public void Draw(Graphics graphics)
        {
            graphics.DrawString(
                $"CameraR (R/r) \t\t\t\t = {Values.CameraR}\n" +
                $"CameraDistance (D/d) \t\t\t = {Values.CameraDistance}\n" +
                $"CameraTheta (T/t) \t\t\t = {Values.CameraTheta}\n" +
                $"CameraPhi (P/p) \t\t\t = {Values.CameraPhi}\n" +
                $"CubeScale (S/s) \t\t\t = {Values.CubeScale}\n" +
                $"CubeX (X/x) \t\t\t\t = {Values.CubeX}\n" +
                $"CubeY (Y/y) \t\t\t\t = {Values.CubeY}\n" +
                $"CubeZ (Z/z) \t\t\t\t = {Values.CubeZ}\n" +
                $"CubeThetaX (Left/Right) \t\t = {Values.CubeThetaX}\n" +
                $"CubeThetaY (Up/Down) \t\t\t = {Values.CubeThetaY}\n" +
                $"CubeThetaZ (PageUp/PageDown) \t = {Values.CubeThetaZ}\n" +
                $"Animation (a) \t\t\t\t = {(_timer.Enabled ? $"On ({_animationControl.CurrentAnimation.GetType().Name})" : "Off")}",
                _font,
                _brush,
                _position
            );
        }
    }
}
