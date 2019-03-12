#include <iostream>
#include <GL/glew.h>
#include <GL/freeglut.h>
#include <glm/glm/glm.hpp>
#include <glm/glm/gtc/type_ptr.hpp>
#include "glsl.h"

using namespace std;


//--------------------------------------------------------------------------------
// Consts
//--------------------------------------------------------------------------------

const int WIDTH = 800, HEIGHT = 600;

GLuint programID;

const char* fragmentshader_name = "fragmentShader.fsh";
const char* vertexshader_name = "vertexShader.vsh";


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
    static const GLfloat blue[] = { 0.0, 0.0, 0.4, 1.0 };

	glClearBufferfv(GL_COLOR, 0, blue);

	glUseProgram(programID);

	GLuint position_id = glGetUniformLocation(programID, "position");
	GLuint color_id = glGetUniformLocation(programID, "color");

	glm::vec4 position = glm::vec4(0.3, -0.4, 0.0, 1.0);
	glm::vec4 color = glm::vec4(1, 0, 1, 1);

	glUniform4fv(position_id, 1, glm::value_ptr(position));
	glUniform4fv(color_id, 1, glm::value_ptr(color));

	glPointSize(40.0f);
	glDrawArrays(GL_POINTS, 0, 1);

    glutSwapBuffers();
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

    glewInit();
}

void initShaders() 
{
	char *fragshader = glsl::readFile(fragmentshader_name);
	GLuint fshID = glsl::makeFragmentShader(fragshader);

	char *vertexshader = glsl::readFile(vertexshader_name);
	GLuint vshID = glsl::makeVertexShader(vertexshader);

	programID = glsl::makeShaderProgram(vshID, fshID);
}

int main(int argc, char ** argv)
{
    InitGlutGlew(argc, argv);
	initShaders();

    HWND hWnd = GetConsoleWindow();
    ShowWindow(hWnd, SW_HIDE);

    glutMainLoop();

    return 0;
}