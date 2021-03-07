#version 440 core
layout (location = 0) in vec4 aPosition;
layout (location = 1) in vec4 vertexColor;

out vec4 color;
out vec2 TexCoord;

void main()
{
    gl_Position = vec4(aPosition.x, aPosition.y, 0, 1.0);
    double scale = abs(aPosition.y) * 2.5;
    if(scale < 0.15)
    {
        scale = 0.15;
    }
    if(scale > 1)
    {
        scale = 1.0;
    }
    color = vertexColor * vec4(scale, scale, scale, 1.0);
    TexCoord = vec2(aPosition.z, aPosition.w);
}