﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace graphics_programming
{
    public class Cube
    {

        //          7----------4
        //         /|         /|
        //        / |        / |                y
        //       /  6-------/--5                |
        //      3----------0  /                 ----x
        //      | /        | /                 /
        //      |/         |/                  z
        //      2----------1

        private const int size = 1;

        private readonly Color color;
        public Cube(Color c) { color = c; }

        public List<Vector3> vertexbuffer = new List<Vector3>
        {
            new Vector3( 1.0f,  1.0f, 1.0f),     // 0
            new Vector3( 1.0f, -1.0f, 1.0f),     // 1
            new Vector3(-1.0f, -1.0f, 1.0f),     // 2
            new Vector3(-1.0f,  1.0f, 1.0f),     // 3

            new Vector3( 1.0f,  1.0f, -1.0f),    // 4
            new Vector3( 1.0f, -1.0f, -1.0f),    // 5
            new Vector3(-1.0f, -1.0f, -1.0f),    // 6
            new Vector3(-1.0f,  1.0f, -1.0f),    // 7

            new Vector3( 1.2f,  1.2f, 1.2f),     // 0
            new Vector3( 1.2f, -1.2f, 1.2f),     // 1
            new Vector3(-1.2f, -1.2f, 1.2f),     // 2
            new Vector3(-1.2f,  1.2f, 1.2f),     // 3

            new Vector3( 1.2f,  1.2f, -1.2f),    // 4
            new Vector3( 1.2f, -1.2f, -1.2f),    // 5
            new Vector3(-1.2f, -1.2f, -1.2f),    // 6
            new Vector3(-1.2f,  1.2f, -1.2f)     // 7
        };

        public void Draw(Graphics g, List<Vector3> vb)
        {
            Pen pen = new Pen(color, 3f);
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[1].X, vb[1].Y);    //0 -> 1
            g.DrawLine(pen, vb[1].X, vb[1].Y, vb[2].X, vb[2].Y);    //1 -> 2
            g.DrawLine(pen, vb[2].X, vb[2].Y, vb[3].X, vb[3].Y);    //2 -> 3
            g.DrawLine(pen, vb[3].X, vb[3].Y, vb[0].X, vb[0].Y);    //3 -> 0

            g.DrawLine(pen, vb[4].X, vb[4].Y, vb[5].X, vb[5].Y);    //4 -> 5
            g.DrawLine(pen, vb[5].X, vb[5].Y, vb[6].X, vb[6].Y);    //5 -> 6
            g.DrawLine(pen, vb[6].X, vb[6].Y, vb[7].X, vb[7].Y);    //6 -> 7
            g.DrawLine(pen, vb[7].X, vb[7].Y, vb[4].X, vb[4].Y);    //7 -> 4

            //pen.DashStyle = DashStyle.DashDot;
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[4].X, vb[4].Y);    //0 -> 4
            g.DrawLine(pen, vb[1].X, vb[1].Y, vb[5].X, vb[5].Y);    //1 -> 5
            g.DrawLine(pen, vb[2].X, vb[2].Y, vb[6].X, vb[6].Y);    //2 -> 6
            g.DrawLine(pen, vb[3].X, vb[3].Y, vb[7].X, vb[7].Y);    //3 -> 7

            Font font = new Font("Arial", 12, FontStyle.Bold);
            for (int i = 0; i < 8; i++)
            {
                PointF p = new PointF(vb[i + 8].X, vb[i + 8].Y);
                g.DrawString(i.ToString(), font, Brushes.Black, p);
            }
        }
    }   
}
