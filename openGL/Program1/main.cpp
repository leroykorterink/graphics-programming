#include <vector>

#include <GL/glew.h>
#include <GL/freeglut.h>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include "glsl.h"
#include "objloader.hpp"

#include "Object3d.h"

//--------------------------------------------------------------------------------
// Consts
//--------------------------------------------------------------------------------

const int WIDTH = 800, HEIGHT = 600;
const char * fragshader_name = "shaders/fragmentshader.glsl";
const char * vertexshader_name = "shaders/vertexshader.glsl";
unsigned const int DELTA = 10;

//--------------------------------------------------------------------------------
// Structs
//--------------------------------------------------------------------------------
struct LightSource {
	glm::vec3 position;
};

struct Material {
	glm::vec3 ambientColor;
	glm::vec3 diffuseColor;
	glm::vec3 specular;
	GLfloat power;
};

//--------------------------------------------------------------------------------
// Variables
//--------------------------------------------------------------------------------
std::vector<Object3d> myObjects;

GLuint program_id;
GLuint vao;
GLuint uniform_mv, uniform_proj, uniform_light_pos, uniform_material_ambient, uniform_material_diffuse, 
uniform_material_specular, uniform_material_power;

glm::mat4 model, view, projection;
glm::mat4 mv;

LightSource lightSource;
Material material;

//--------------------------------------------------------------------------------
// Keyboard handling
//--------------------------------------------------------------------------------

void keyboardHandler(unsigned char key, int a, int b)
{
    if (key == 27)
        glutExit();
}

/**
 * Draws all objects
 */
void DrawObjects(std::vector<Object3d> objects)
{
	glBindVertexArray(vao);

	// Loop trough vector to draw all triangles in vao
	using ConstIterator = std::vector<Object3d>::const_iterator;

	for (ConstIterator it = objects.begin(); it != objects.end(); ++it)
	{
		glDrawArrays(GL_TRIANGLES, 0, it->vertices.size());
	}

	glBindVertexArray(0);
}

void InitObjects(std::vector<Object3d> objects)
{
	// Initialize the objects
	using Iterator = std::vector<Object3d>::iterator;

	for (Iterator it = objects.begin(); it != objects.end(); ++it)
	{
		it->Init(program_id, &vao);
	}
}


//--------------------------------------------------------------------------------
// Rendering
//--------------------------------------------------------------------------------

void Render()
{
    glClearColor(0.0, 0.0, 0.0, 1.0);
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

    // Send vao
    glBindVertexArray(vao);
	DrawObjects(myObjects);
    glBindVertexArray(0);

    // Do transformation
    model = glm::rotate(model, 0.01f, glm::vec3(0.0f, 1.0f, 0.0f));
    mv = view * model;

    // Send mvp
    glUseProgram(program_id);
    glUniformMatrix4fv(uniform_mv, 1, GL_FALSE, glm::value_ptr(mv));

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
	material.specular = glm::vec3(1.0);
	material.power = 123;
}

void CreateLights()
{
	InitMaterial();

	// Make uniform vars
	uniform_mv = glGetUniformLocation(program_id, "mv");
	uniform_proj = glGetUniformLocation(program_id, "projection");
	uniform_light_pos = glGetUniformLocation(program_id, "lightPosition");
	uniform_material_ambient = glGetUniformLocation(program_id, "materiaAmbient");
	uniform_material_diffuse = glGetUniformLocation(program_id,"materialDiffuse");
	uniform_material_specular = glGetUniformLocation(program_id, "materialSpecular");
	uniform_material_power = glGetUniformLocation(program_id, "materialPower");

	// Fill uniform vars
	glUseProgram(program_id);
	glUniformMatrix4fv(uniform_mv, 1, GL_FALSE, glm::value_ptr(mv));
	glUniformMatrix4fv(uniform_proj, 1, GL_FALSE, glm::value_ptr(projection));
	glUniform3fv(uniform_light_pos, 1, glm::value_ptr(lightSource.position));
	glUniform3fv(uniform_material_ambient, 1, glm::value_ptr(material.ambientColor));
	glUniform3fv(uniform_material_diffuse, 1, glm::value_ptr(material.diffuseColor));
	glUniform3fv(uniform_material_specular, 1, glm::value_ptr(material.specular));
	glUniform1f(uniform_material_power, material.power);
}

//------------------------------------------------------------
// void InitBuffers()
// Allocates and fills buffers
//------------------------------------------------------------
int main(int argc, char ** argv)
{
    InitGlutGlew(argc, argv);
	

	InitShaders();
    InitMatrices();
	CreateLights();

	myObjects = std::vector<Object3d>{
		Object3d("assets/box.obj", "assets/yellow_brick.bmp")
	};

	InitObjects(myObjects);

    glEnable(GL_DEPTH_TEST);
    glDisable(GL_CULL_FACE);

    HWND hWnd = GetConsoleWindow();
    ShowWindow(hWnd, SW_SHOW);

    glutMainLoop();

    return 0;
}
