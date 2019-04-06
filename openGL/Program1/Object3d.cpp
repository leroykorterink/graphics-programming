#include <vector>

#include "Object3d.h"
#include "objloader.hpp"

using namespace std;

Object3d::Object3d(char* modelPath, char* texturePath) {
	// load model and save its content to current object
	loadOBJ(modelPath, vertices, uvs, normals);
}

void Object3d::Init(GLuint programId, GLuint* vertexArrayObject)
{
	GLuint
		position_id,
		normal_id,
		vbo_vertices,
		vbo_normals;

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
	position_id = glGetAttribLocation(programId, "position");
	normal_id = glGetAttribLocation(programId, "normal");

	// Allocate memory for vao
	glGenVertexArrays(1, vertexArrayObject);

	// Bind to vao
	glBindVertexArray(*vertexArrayObject);

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

void Object3d::Update()
{
}