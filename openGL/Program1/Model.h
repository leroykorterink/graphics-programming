#pragma once

#include <glm/glm.hpp>
#include <GL/glew.h>
#include <vector>

#include "MaterialProperties.cpp";

class Model
{
private:
	GLuint _vertexArrayObject;
	GLuint _programId;

public:
	std::vector<glm::vec3> Vertices;
	std::vector<glm::vec3> Normals;
	std::vector<glm::vec2> Uvs;

	GLuint TextureId;

	MaterialProperties* Material;

	Model(GLuint programId, char* modelPath, char* texturePath);

	void Init();
	void Update(int deltaTime);
	void Draw();
};
