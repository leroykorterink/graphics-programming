#pragma once

#include <glm/glm/glm.hpp>
#include <GL/glew.h>
#include <vector>

class Object3d
{
public:
	std::vector<glm::vec3> vertices;
	std::vector<glm::vec3> normals;
	std::vector<glm::vec2> uvs;

	GLuint textureId;

	Object3d(char* modelPath, char* texturePath);
	~Object3d();

	void Init(const GLuint programId, GLuint vertexArrayObject);
	void Update();
};

