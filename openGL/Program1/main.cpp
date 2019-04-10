#include <vector>

#include <GL/glew.h>
#include <GL/freeglut.h>

#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include "glsl.h"
#include "objloader.hpp"

#include "Model.h"

//--------------------------------------------------------------------------------
// Consts
//--------------------------------------------------------------------------------

const int WIDTH = 800, HEIGHT = 600;
const char * fragshader_name = "src/shaders/fragmentshader.glsl";
const char * vertexshader_name = "src/shaders/vertexshader.glsl";
unsigned const int DELTA = 10;

//--------------------------------------------------------------------------------
// Structs
//--------------------------------------------------------------------------------
struct LightSource {
	glm::vec3 position;
};

//--------------------------------------------------------------------------------
// Variables
//--------------------------------------------------------------------------------
std::vector<Model> myObjects;

GLuint program_id;
GLuint vao;
GLuint uniform_mv, uniform_proj, uniform_light_pos, uniform_material_ambient, uniform_material_diffuse, 
uniform_material_specular, uniform_material_power;

glm::mat4 model, view, projection;
glm::mat4 mv;

LightSource lightSource;

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
void InitObjects(std::vector<Model> objects)
{
	using Iterator = std::vector<Model>::iterator;

	for (Iterator it = objects.begin(); it != objects.end(); ++it)
	{
		it->Init();
	}
}

//--------------------------------------------------------------------------------
// Rendering
//--------------------------------------------------------------------------------

void Render()
{
    glClearColor(0.0, 0.0, 0.0, 1.0);
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	using Iterator = std::vector<Model>::iterator;

	for (Iterator it = myObjects.begin(); it != myObjects.end(); ++it)
	{
		it->Draw();
	}

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

void CreateLights()
{
	lightSource.position = glm::vec3(4, 4, 4);

	// Make uniform vars
	uniform_mv = glGetUniformLocation(program_id, "mv");
	uniform_proj = glGetUniformLocation(program_id, "projection");
	uniform_light_pos = glGetUniformLocation(program_id, "lightPosition");

	// Fill uniform vars
	glUseProgram(program_id);
	glUniformMatrix4fv(uniform_mv, 1, GL_FALSE, glm::value_ptr(mv));
	glUniformMatrix4fv(uniform_proj, 1, GL_FALSE, glm::value_ptr(projection));
	glUniform3fv(uniform_light_pos, 1, glm::value_ptr(lightSource.position));
}

//------------------------------------------------------------
// void InitBuffers()
// Allocates and fills buffers
//------------------------------------------------------------
int main(int argc, char ** argv)
{
    InitGlutGlew(argc, argv);

	// Configure OpenGL
	glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);

	glEnable(GL_DEPTH_TEST | GL_BLEND);
	glDisable(GL_CULL_FACE);

	// Setup renderer
	InitShaders();
    InitMatrices();
	CreateLights();

	myObjects = std::vector<Model>{
		Model(program_id, "assets/box.obj", "assets/yellow_brick.bmp")
	};

	InitObjects(myObjects);

    HWND hWnd = GetConsoleWindow();
    ShowWindow(hWnd, SW_SHOW);

    glutMainLoop();

    return 0;
}
