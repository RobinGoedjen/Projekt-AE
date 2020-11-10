using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Raycasting
{
    class Game : GameWindow
    {
        float time = 0f;
        float oldTime = 0f;
        Player player;
        Map map;

        Vector2[] vertices =
        {
            new Vector2(-0.2f, 0.2f),
            new Vector2(0.2f, 0.2f),
            new Vector2(0.2f, -0.2f),
            new Vector2(-0.2f, -0.2f)
        };

        private Shader shader;

        int VertexBufferObject;
        int VertexArrayObject;


        public Game(Player player, Map map) : base(500, 300, GraphicsMode.Default, "Raycasting")
        {
            this.map = map;
            this.player = player;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();
            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            for (int x = 0; x < this.Width; x++)
            {
                float cameraX = 2 * x / this.Width - 1;
                Vector2 rayDir = player.direction + player.plane * cameraX;

                Point currentMapPosition = new Point((int)player.position.X, (int)player.position.Y);
                Vector2 sideDistance;

                float deltaDistX = Math.Abs(1 / rayDir.X);
                float deltaDistY = Math.Abs(1 / rayDir.Y);
                float perpWallDist;

                //what direction to step in x or y-direction (either +1 or -1)
                int stepX;
                int stepY;

                bool hit = false;
                int side; 



            }



            base.OnUpdateFrame(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.8f, 0.3f, 0.3f, 1f);

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Vector2.SizeInBytes, vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);
            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);


            shader = new Shader("shader.vert", "shader.frag");
            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            shader.Dispose();
            base.OnUnload(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Quads, 0, vertices.Length);
            

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }

    }
}
