#version 430 core

uniform mat4 mvp;

in vec4 position;
in vec4 color;

out vec4 vColor;

void main()
{
	gl_Position = mvp * vec4(position, 1.0);

	vColor = color;
}
