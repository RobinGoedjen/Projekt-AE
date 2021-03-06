using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Raycasting
{
    class ProgressBar
    {
        public List<Vector4> vertices;
        private readonly Vector2 topLeft, bottomRight;
        public readonly Color barColor;
        public readonly int VBO, VAO;
        private readonly Vector2 topLeftScale, bottomRightScale;
        
        public ProgressBar(Vector2 topLeft, Vector2 bottomRight, Color barColor)
        {
            this.topLeft = topLeft;
            this.bottomRight = bottomRight;
            this.barColor = barColor;
            vertices = new List<Vector4>();
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
            //topLeftScale = new Vector2(1.10f, 0.975f);
            //bottomRightScale = new Vector2(0.985f, 1.025f);
            topLeftScale = new Vector2((bottomRight.X - topLeft.X) * 0.005f, (topLeft.Y - bottomRight.Y) * -0.09f);
            bottomRightScale = new Vector2((bottomRight.X - topLeft.X) * -0.005f, (topLeft.Y - bottomRight.Y) * 0.09f);

            initializeBar();
        }

        
        private void initializeBar()
        {
            fillVertices(vertices, topLeft, bottomRight);
            fillVertices(vertices, topLeft + topLeftScale, bottomRight + bottomRightScale);
            fillVertices(vertices, topLeft + topLeftScale, bottomRight + bottomRightScale);
            updateProgress(0f);
        }

        public void updateProgress(float progressInPercent)
        {
            vertices.RemoveRange(vertices.Count - 4, 4);
            var newX = bottomRight.X - Math.Abs(bottomRight.X - topLeft.X) * (1 - progressInPercent);
            var newBottomRight = new Vector2(newX, bottomRight.Y);
            fillVertices(vertices, topLeft + topLeftScale, newBottomRight + bottomRightScale);
        }

        private void fillVertices(List<Vector4> vertices, Vector2 topLeftCorner, Vector2 bottomRightCorner)
        {
            vertices.Add(new Vector4(topLeftCorner.X, topLeftCorner.Y, 0f, 0f));
            vertices.Add(new Vector4(topLeftCorner.X, bottomRightCorner.Y, 0f, 0f));
            vertices.Add(new Vector4(bottomRightCorner.X, bottomRightCorner.Y, 0f, 0f));
            vertices.Add(new Vector4(bottomRightCorner.X, topLeftCorner.Y, 0f, 0f));
        }
    }
}
