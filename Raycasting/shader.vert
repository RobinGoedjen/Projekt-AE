#version 440 core
layout (location = 0) in vec2 aPosition;
layout (location = 1) in vec3 vertexColor;

out vec3 color;

void main()
{
    gl_Position = vec4(aPosition, 0, 1.0);
    color = vertexColor;
}