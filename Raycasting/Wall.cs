using System.Collections.Generic;
using OpenTK;
using MapLibrary;

namespace Raycasting
{
    class Wall
    {
        public readonly int VAO;
        public readonly int VBO;
        public readonly List<Vector4> vertices;
        public readonly Vector4 color;
        public readonly WallKind texture;

        public Wall(WallKind texture, Vector4 color, int VAO, int VBO)
        {
            this.texture = texture;
            this.color = color;
            this.VAO = VAO;
            this.VBO = VBO;
            vertices = new List<Vector4>();
        }

        public void addOpenVertice(float drawXScaled, float drawYScaled, float textX)
        {
            vertices.Add(new Vector4(drawXScaled, drawYScaled, textX, 1f));
            vertices.Add(new Vector4(drawXScaled, -drawYScaled, textX, 0f));
        }

        public void addCloseVertice(float drawXScaled, float drawYScaled, float textX)
        {
            vertices.Add(new Vector4(drawXScaled, -drawYScaled, textX, 0f));
            vertices.Add(new Vector4(drawXScaled, drawYScaled, textX, 1f));
        }
    }
}
