#include <iostream>
#include <GL/glew.h>
#include <GL/freeglut.h>
#include <glm/glm.hpp>
#include <glm/gtc/type_ptr.hpp>

#include "glsl.h"

using namespace std;


//--------------------------------------------------------------------------------
// Consts
//--------------------------------------------------------------------------------

const int WIDTH = 800, HEIGHT = 600;
const char * fragshader_name = "fragmentshader.fsh";
const char * vertexshader_name = "vertexshader.vsh";
unsigned const int DELTA = 10;


//--------------------------------------------------------------------------------
// Variables
//--------------------------------------------------------------------------------

GLuint program_id;
GLuint vao;

struct VertexFormat
{
	glm::vec4 position;
	glm::vec4 color;

	VertexFormat(glm::vec4 &pos, glm::vec4 &col)
	{
		position = pos;
		color = col;
	}
};

VertexFormat triangle[] = {
   VertexFormat(glm::vec4(0.5, -0.5, 0.0, 1.0), glm::vec4(1.0f, 0.0f, 0.0f, 1.0f)),
   VertexFormat(glm::vec4(-0.5, -0.5, 0.0, 1.0), glm::vec4(0.0f, 1.0f, 0.0f, 1.0f)),
   VertexFormat(glm::vec4(0.0, 0.5, 0.0, 1.0), glm::vec4(0.0f, 0.0f, 1.0f, 1.0f))
};

//--------------------------------------------------------------------------------
// Mesh variables
//--------------------------------------------------------------------------------

const GLfloat vertices[] =
{
	-0.5f, 0.5f, 0.0f, 0.5f,
	0.5f, 0.5f, 0.0f, 0.5f,
	-0.5f, -0.5f, 0.0f, 0.55f
};

const GLfloat colors[] =
{
	1.0f, 0.0f, 0.0f, 0.0f,
	0.0f, 1.0f, 0.0f, 0.0f,
	0.0f, 0.0f, 1.0f, 0.0f,
	0.0f, 0.0f, 0.0f, 1.0f
};


//--------------------------------------------------------------------------------
// Keyboard handling
//--------------------------------------------------------------------------------

void keyboardHandler(unsigned char key, int a, int b)
{
	if (key == 27)
		glutExit();
}


//--------------------------------------------------------------------------------
// Rendering
//--------------------------------------------------------------------------------

void Render()
{
	glClearColor(0.0, 0.0, 0.0, 1.0);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glUseProgram(program_id);

	glBindVertexArray(vao);
	glDrawArrays(GL_TRIANGLES, 0, 3);
	glBindVertexArray(0);

	glutSwapBuffers();
}



//------------------------------------------------------------
// void Render(int n)
// Render method that is called by the timer function
//------------------------------------------------------------

void Render(int n)
{
	Render();
	glutTimerFunc(DELTA, Render, 0);
}


//------------------------------------------------------------
// void InitGlutGlew(int argc, char **argv)
// Initializes Glut and Glew
//------------------------------------------------------------

void InitGlutGlew(int argc, char **argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA | GLUT_DEPTH);
	glutInitWindowSize(WIDTH, HEIGHT);
	glutCreateWindow("Hello OpenGL");
	glutDisplayFunc(Render);
	glutKeyboardFunc(keyboardHandler);
	glutTimerFunc(DELTA, Render, 0);

	glewInit();
}


//------------------------------------------------------------
// void InitShaders()
// Initializes the fragmentshader and vertexshader
//------------------------------------------------------------

void InitShaders()
{
	char * vertexshader = glsl::readFile(vertexshader_name);
	GLuint vsh_id = glsl::makeVertexShader(vertexshader);

	char * fragshader = glsl::readFile(fragshader_name);
	GLuint fsh_id = glsl::makeFragmentShader(fragshader);

	program_id = glsl::makeShaderProgram(vsh_id, fsh_id);
}


//------------------------------------------------------------
// void InitBuffers()
// Allocates and fills buffers
//------------------------------------------------------------

void InitBuffers()
{
	GLuint position_id, color_id;
	GLuint vbo;

	// VBO for VertexFormat
	glGenBuffers(1, &vbo);
	glBindBuffer(GL_ARRAY_BUFFER, vbo);
	glBufferData(GL_ARRAY_BUFFER, sizeof(triangle), triangle, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	position_id = glGetAttribLocation(program_id, "position");
	color_id = glGetAttribLocation(program_id, "color");

	glGenVertexArrays(1, &vao);
	glBindVertexArray(vao);

	// Bind vertices and color to vao
	glBindBuffer(GL_ARRAY_BUFFER, vbo);

	// Bind vertices to vao
	glVertexAttribPointer(position_id, 4, GL_FLOAT, GL_FALSE, sizeof(VertexFormat), 0);
	glEnableVertexAttribArray(position_id);

	// Bind colors to vao
	glVertexAttribPointer(color_id, 4, GL_FLOAT, GL_FALSE, sizeof(VertexFormat), (void*)(sizeof(glm::vec4)));
	glEnableVertexAttribArray(color_id);

	glBindBuffer(GL_ARRAY_BUFFER, 0);
	glBindVertexArray(0);
}


int main(int argc, char ** argv)
{
	InitGlutGlew(argc, argv);
	InitShaders();
	InitBuffers();

	HWND hWnd = GetConsoleWindow();
	ShowWindow(hWnd, SW_HIDE);

	glutMainLoop();

	return 0;
}
