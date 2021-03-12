using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycasting
{
    abstract class Texture
    {
        public readonly int VAO;
        public readonly int VBO;
        public List<Vector4> vertices;

        public Texture()
        {
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();
        }
    }
}
