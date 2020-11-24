#version 440 core
layout (location = 0) in vec2 aPosition;
layout (location = 1) in vec4 vertexColor;

out vec4 color;

void main()
{
    gl_Position = vec4(aPosition, 0, 1.0);
    double scale = abs(aPosition.y) * 2.5;
    if(scale > 0.6)
    {
        scale = 0.6;
    }
    if(scale < 0.15)
    {
        scale = 0.15;
    }
    color = vertexColor * vec4(scale, scale, scale, 1.0);
}