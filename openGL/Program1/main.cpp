#include <iostream>
#include <GL/glew.h>
#include <GL/freeglut.h>
#include <glm/glm.hpp>
#include <glm/gtc/type_ptr.hpp>
#include <glm/gtc/matrix_transform.hpp>

#include "glsl.h"

using namespace std;


//--------------------------------------------------------------------------------
// Consts
//--------------------------------------------------------------------------------

const int WIDTH = 800, HEIGHT = 600;
const char * fragshader_name = "fragmentshader.fsh";
const char * vertexshader_name = "vertexshader.vsh";
unsigned const int DELTA = 10;

glm::vec3 cameraPosition = glm::vec3(0.0, 0.0, 2);
glm::vec3 cameraTarget = glm::vec3(0.0, 0.0, 0.0);
glm::vec3 cameraUpPosition = glm::vec3(0.0, 1.0, 0.0);

//--------------------------------------------------------------------------------
// Variables
//--------------------------------------------------------------------------------

GLuint program_id;
GLuint vao;
GLuint uniform_mvp;

glm::mat4 projection, model, view;
glm::mat4 mvp;

//------------------------------------------------------------
// Variables for object
//
//           7----------6
//          /|         /|
//         / |        / |
//        /  4-------/--5               y
//       /  /       /  /                |
//      3----------2  /                 ----x
//      | /        | /                 /
//      |/         |/                  z
//      0----------1
//------------------------------------------------------------

GLfloat vertices[] = {
	// front
	-1.0, -1.0, 1.0,
	1.0, -1.0, 1.0,
	1.0, 1.0, 1.0,
	-1.0, 1.0, 1.0,
	// back
	-1.0, -1.0, -1.0,
	1.0, -1.0, -1.0,
	1.0, 1.0, -1.0,
	-1.0, 1.0, -1.0
};

GLfloat colors[] = {
	// front colors
	1.0, 1.0, 0.0,
	0.0, 1.0, 0.0,
	0.0, 0.0, 1.0,
	1.0, 1.0, 1.0,
	// back colors
	0.0, 1.0, 1.0,
	1.0, 0.0, 1.0,
	1.0, 0.0, 0.0,
	1.0, 1.0, 0.0
};

GLushort cube_elements[] = {
	0,1,1,2,2,3,3,0,  // front
	0,4,1,5,3,7,2,6,  // front to back
	4,5,5,6,6,7,7,4   //back
};

//--------------------------------------------------------------------------------
// Keyboard handling
//--------------------------------------------------------------------------------

void keyboardHandler(unsigned char key, int a, int b)
{
	if (key == 27)
		glutExit();
}

//------------------------------------------------------------
// void UpdateMVP()
//------------------------------------------------------------
void UpdateMVP(float rotation)
{
	model = glm::mat4();

	//Rotate Model (10 degrees)
	model = glm::rotate(model, glm::radians(10.0f), glm::vec3(0.0f, 0.0f, 1.0f));

	view = glm::lookAt(
		cameraPosition,
		cameraTarget,
		cameraUpPosition
	);

	projection = glm::perspective(
		glm::radians(rotation),
		(1.0f * WIDTH) / HEIGHT,
		0.1f,
		20.0f
	);

	mvp = projection * view * model;
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
	
	glDrawElements(
		GL_LINES, 
		sizeof(cube_elements) / sizeof(GLushort),
		GL_UNSIGNED_SHORT, 
		0
	);

	glBindVertexArray(0);

	glutSwapBuffers();
}

//------------------------------------------------------------
// void Render(int n)
// Render method that is called by the timer function
//------------------------------------------------------------

void Render(int n)
{
	UpdateMVP(n % 360);

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
	GLuint vbo_vertices, vbo_colors, ibo_elements;

	// vbo for vertices
	glGenBuffers(1, &vbo_vertices);
	glBindBuffer(GL_ARRAY_BUFFER, vbo_vertices);
	glBufferData(GL_ARRAY_BUFFER, sizeof(vertices), vertices, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	// vbo for colors
	glGenBuffers(1, &vbo_colors);
	glBindBuffer(GL_ARRAY_BUFFER, vbo_colors);
	glBufferData(GL_ARRAY_BUFFER, sizeof(colors), colors, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	// vbo for elements
	glGenBuffers(1, &ibo_elements);
	glBindBuffer(GL_ARRAY_BUFFER, ibo_elements);
	glBufferData(GL_ARRAY_BUFFER, sizeof(cube_elements), cube_elements, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	position_id = glGetAttribLocation(program_id, "position");
	color_id = glGetAttribLocation(program_id, "color");

	glGenVertexArrays(1, &vao);

	glBindVertexArray(vao);

	// Bind vertices to vao
	glBindBuffer(GL_ARRAY_BUFFER, vbo_vertices);
	glVertexAttribPointer(position_id, 3, GL_FLOAT, GL_FALSE, 0, 0);
	glEnableVertexAttribArray(position_id);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	// Bind colors to vao
	glBindBuffer(GL_ARRAY_BUFFER, vbo_colors);
	glVertexAttribPointer(color_id, 3, GL_FLOAT, GL_FALSE, 0, 0);
	glEnableVertexAttribArray(color_id);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	// Bind elements to vao
	glBindBuffer(GL_ARRAY_BUFFER, ibo_elements);

	glBindVertexArray(0);


	//Make uniform vars
	uniform_mvp = glGetUniformLocation(program_id, "mvp");

	//Fill uniform variables
	glUseProgram(program_id);
	glUniformMatrix4fv(uniform_mvp, 1, GL_FALSE, glm::value_ptr(mvp));
}

int main(int argc, char ** argv)
{
	InitGlutGlew(argc, argv);
	UpdateMVP(45.0f);
	InitShaders();
	InitBuffers();

	glEnable(GL_DEPTH_TEST);
	glEnable(GL_CULL_FACE);

	HWND hWnd = GetConsoleWindow();
	ShowWindow(hWnd, SW_HIDE);

	glutMainLoop();

	return 0;
}