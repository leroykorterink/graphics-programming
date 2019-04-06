
#include <iostream>
#include <vector>

#include <GL/glew.h>
#include <GL/freeglut.h>

#include <glm/glm/glm.hpp>
#include <glm/glm/gtc/matrix_transform.hpp>
#include <glm/glm/gtc/type_ptr.hpp>

#include "glsl.h"
#include "objloader.hpp"
#include "texture.hpp"

#include "Object3d.h"

//--------------------------------------------------------------------------------
// Consts
//--------------------------------------------------------------------------------

const int WIDTH = 800, HEIGHT = 600;
unsigned const int DELTA = 10;

//--------------------------------------------------------------------------------
// Structs
//--------------------------------------------------------------------------------
struct LightSource
{
	glm::vec3 position;
};

struct Material
{
	glm::vec3 ambientColor;
	glm::vec3 diffuseColor;
	glm::vec3 specular;
	GLfloat power;
};

//--------------------------------------------------------------------------------
// Variables
//--------------------------------------------------------------------------------
GLuint programId;
GLuint vao;

const char *fragmentshader_name = "shaders/fragmentShader.glsl";
const char *vertexshader_name = "shaders/vertexShader.glsl";

GLuint 
	uniform_mv, 
	uniform_proj,
	uniform_light_pos,
	uniform_material_ambient,
	uniform_material_diffuse,
	uniform_material_specular,
	uniform_material_power;

glm::mat4 
	model,
	view,
	projection,
	mv;

LightSource lightSource;
Material material;

//--------------------------------------------------------------------------------
// Mesh variables
//--------------------------------------------------------------------------------
vector<Object3d> myObjects;

vector<glm::vec3> vertices;
vector<glm::vec3> normals;
vector<glm::vec2> uvs;

void DrawObjects(std::vector<Object3d> objects)
{
	glBindVertexArray(vao);

	// Loop trough vector until every object is rendered
	using ConstIterator = std::vector<Object3d>::const_iterator;

	for (ConstIterator it = objects.begin(); it != objects.end(); ++it)
	{
		glDrawArrays(GL_TRIANGLES, 0, it->vertices.size());
	}

	glBindVertexArray(0);
}

void InitObjects(std::vector<Object3d> objects)
{
	// Loop trough vector until every object is rendered
	using Iterator = std::vector<Object3d>::iterator;

	for (Iterator it = objects.begin(); it != objects.end(); ++it)
	{
		it->Init(programId, vao);
	}
}

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
	GLfloat blue[] = {0.0, 0.0, 0.4, 1.0};

	// Send vao
	DrawObjects(myObjects);

	glUseProgram(programId);

	GLuint position_id = glGetUniformLocation(programId, "position");
	GLuint color_id = glGetUniformLocation(programId, "color");

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

void InitShaders()
{
	char *fragshader = glsl::readFile(fragmentshader_name);
	GLuint fshID = glsl::makeFragmentShader(fragshader);

	char *vertexshader = glsl::readFile(vertexshader_name);
	GLuint vshID = glsl::makeVertexShader(vertexshader);

	// Fill uniform vars
	glUseProgram(programId);
	glUniformMatrix4fv(uniform_mv, 1, GL_FALSE, glm::value_ptr(mv));
	glUniformMatrix4fv(uniform_proj, 1, GL_FALSE, glm::value_ptr(projection));
	glUniform3fv(uniform_light_pos, 1, glm::value_ptr(lightSource.position));
	glUniform3fv(uniform_material_ambient, 1, glm::value_ptr(material.ambientColor));
	glUniform3fv(uniform_material_diffuse, 1, glm::value_ptr(material.diffuseColor));
	glUniform3fv(uniform_material_specular, 1, glm::value_ptr(material.specular));
	glUniform1f(uniform_material_power, material.power);
}

//------------------------------------------------------------
// void InitMatrices()
//------------------------------------------------------------
void InitMatrices()
{
	model = glm::mat4();

	view = glm::lookAt(
		glm::vec3(0.0, 2.0, 4.0),
		glm::vec3(0.0, 0.5, 0.0),
		glm::vec3(0.0, 1.0, 0.0));

	mv = model * view;

	projection = glm::perspective(
		glm::radians(45.0f),
		1.0f * WIDTH / HEIGHT, 0.1f,
		20.0f
	);
}

void InitMaterial()
{
	lightSource.position = glm::vec3(4, 4, 4);
	material.ambientColor = glm::vec3(0.2, 0.2, 0.2);
	material.diffuseColor = glm::vec3(0.5, 0.5, 0.5);
	material.specular = glm::vec3(0.75f);
	material.power = 123;
}

void CreateLights()
{
	InitMaterial();

	// Make uniform vars
	uniform_mv = glGetUniformLocation(programId, "mv");
	uniform_proj = glGetUniformLocation(programId, "projection");
	uniform_light_pos = glGetUniformLocation(programId, "lightPosition");
	uniform_material_ambient = glGetUniformLocation(programId, "materialAmbient");
	uniform_material_diffuse = glGetUniformLocation(programId, "materialDiffuse");
	uniform_material_specular = glGetUniformLocation(programId, "materialSpecular");
	uniform_material_power = glGetUniformLocation(programId, "materialPower");

	// Fill uniform vars
	glUseProgram(programId);
	glUniformMatrix4fv(uniform_mv, 1, GL_FALSE, glm::value_ptr(mv));
	glUniformMatrix4fv(uniform_proj, 1, GL_FALSE, glm::value_ptr(projection));
	glUniform3fv(uniform_light_pos, 1, glm::value_ptr(lightSource.position));
	glUniform3fv(uniform_material_ambient, 1, glm::value_ptr(material.ambientColor));
	glUniform3fv(uniform_material_diffuse, 1, glm::value_ptr(material.diffuseColor));
	glUniform3fv(uniform_material_specular, 1, glm::value_ptr(material.specular));
	glUniform1f(uniform_material_power, material.power);
}

int main(int argc, char **argv)
{
	InitGlutGlew(argc, argv);

	// Creates/compiles shader program
	InitShaders();

	// Create view matrix
	InitMatrices();

	CreateLights();

	myObjects = std::vector<Object3d>{
		Object3d("assets/teapot.obj", "assets/yellow_brick.bmp")};

	InitObjects(myObjects);

	glEnable(GL_DEPTH_TEST);
	glDisable(GL_CULL_FACE);

	HWND hWnd = GetConsoleWindow();
	ShowWindow(hWnd, SW_SHOW);

	glutMainLoop();

	return 0;
}