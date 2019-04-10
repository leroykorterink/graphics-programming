#include <vector>
#include <glm/glm.hpp>
#include <glm/gtc/type_ptr.hpp>

#include "Model.h"
#include "objloader.hpp"

#include "src/lib/VertexArray.h"
#include "src/lib/VertexBuffer.h"
#include "src/lib/IndexBuffer.h"

using namespace std;

/**
 * Loads model and save its content to current object
 */
Model::Model(GLuint programId, char* modelPath, char* texturePath) {
	_programId = programId;

	Material = new MaterialProperties(
		glm::vec3(0.2, 0.2, 0.2),
		glm::vec3(0.5, 0.5, 0.5),
		glm::vec3(1.0),
		123
	);

	loadOBJ(modelPath, Vertices, Uvs, Normals);
}

/**
 * Initializes the current object by generating vbos and binding it to vao
 */
void Model::Init()
{
	// Set material properties as uniforms
	GLuint 
		uniform_material_ambient, 
		uniform_material_diffuse,
		uniform_material_specular, 
		uniform_material_power;

	uniform_material_ambient = glGetUniformLocation(_programId, "materialAmbient");
	uniform_material_diffuse = glGetUniformLocation(_programId, "materialDiffuse");
	uniform_material_specular = glGetUniformLocation(_programId, "materialSpecular");
	uniform_material_power = glGetUniformLocation(_programId, "materialPower");

	glUniform3fv(uniform_material_ambient, 1, glm::value_ptr(Material->AmbientColor));
	glUniform3fv(uniform_material_diffuse, 1, glm::value_ptr(Material->DiffuseColor));
	glUniform3fv(uniform_material_specular, 1, glm::value_ptr(Material->Specular));
	glUniform1f(uniform_material_power, Material->Power);

	// Initialize buffers
	GLuint
		position_id,
		normal_id,
		vbo_vertices,
		vbo_normals;

	VertexArray vertexArray;

	// Create vertex buffer and vertex buffer layout for vertices
	VertexBuffer vertices(&Vertices, Vertices.size() * sizeof(glm::vec3));

	// vbo for normals
	VertexBuffer normals(&Normals, Normals.size() * sizeof(glm::vec3));

	VertexBufferLayout layout;
	layout.Push<float>(3);
	layout.Push<float>(3);

	// Add vertices to vertex array
	vertexArray.AddBuffer(vertices, layout);
	

	// Get vertex attributes
	position_id = glGetAttribLocation(_programId, "position");
	normal_id = glGetAttribLocation(_programId, "normal");

	vertexArray.Bind();

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
	vertexArray.Unbind();
}

void Model::Update(int deltaTime) {
}

void Model::Draw() {
	glBindVertexArray(_vertexArrayObject);
	glDrawArrays(GL_TRIANGLES, 0, Vertices.size());
	glBindVertexArray(0);

	// Send mvp
	glUseProgram(_programId);
}