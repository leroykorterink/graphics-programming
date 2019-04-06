#include <iostream>
#include <vector>

#include <GL/glew.h>
#include <GL/freeglut.h>

#include <glm/glm.hpp>
#include <glm/glm.hpp>
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtc/type_ptr.hpp>

#include "glsl.h"
#include "objloader.hpp"

using namespace std;


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

GLuint program_id;
GLuint vao;
GLuint uniform_mv, uniform_proj, uniform_light_pos, uniform_material_ambient, uniform_material_diffuse, 
uniform_material_specular, uniform_material_power;

glm::mat4 model, view, projection;
glm::mat4 mv;

LightSource lightSource;
Material material;

//--------------------------------------------------------------------------------
// Mesh variables
//--------------------------------------------------------------------------------
vector<glm::vec3> vertices;
vector<glm::vec3> normals;
vector<glm::vec2> uvs;

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

    // Send vao
    glBindVertexArray(vao);
	glDrawArrays(GL_TRIANGLES, 0, vertices.size());
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

void InitBuffers()
{
    GLuint position_id, normal_id;
    GLuint vbo_vertices, vbo_normals;

	CreateLights();

    // vbo for vertices
    glGenBuffers(1, &vbo_vertices);
    glBindBuffer(GL_ARRAY_BUFFER, vbo_vertices);
	glBufferData(GL_ARRAY_BUFFER, vertices.size() * sizeof(glm::vec3), &vertices[0], GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	// vbo for normals
	glGenBuffers(1, &vbo_normals);
	glBindBuffer(GL_ARRAY_BUFFER, vbo_normals);
	glBufferData(GL_ARRAY_BUFFER, normals.size() * sizeof(glm::vec3), &normals[0], GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

    // Get vertex attributes
    position_id = glGetAttribLocation(program_id, "position");
	normal_id = glGetAttribLocation(program_id, "normal");

    // Allocate memory for vao
    glGenVertexArrays(1, &vao);

    // Bind to vao
    glBindVertexArray(vao);

    // Bind vertices to vao
    glBindBuffer(GL_ARRAY_BUFFER, vbo_vertices);
    glVertexAttribPointer(position_id, 3, GL_FLOAT, GL_FALSE, 0, 0);
    glEnableVertexAttribArray(position_id);
    glBindBuffer(GL_ARRAY_BUFFER, 0);

	// Bind normals to vao
	glBindBuffer(GL_ARRAY_BUFFER, vbo_normals);
	glVertexAttribPointer(normal_id, 3, GL_FLOAT, GL_FALSE, 0, 0);
	glEnableVertexAttribArray(normal_id);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

    
    // Stop bind to vao
    glBindVertexArray(0);
}


void InitObjects()
{
	bool res = loadOBJ("assets/teapot.obj", vertices, uvs, normals);
}

int main(int argc, char ** argv)
{
    InitGlutGlew(argc, argv);
	InitObjects();
	InitShaders();
    InitMatrices();
    InitBuffers();

    glEnable(GL_DEPTH_TEST);
    glDisable(GL_CULL_FACE);

    HWND hWnd = GetConsoleWindow();
    ShowWindow(hWnd, SW_HIDE);

    glutMainLoop();

    return 0;
}
