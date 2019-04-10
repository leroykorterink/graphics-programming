#include <glm/glm.hpp>
#include <GL/glew.h>

struct MaterialProperties {
	glm::vec3 AmbientColor;
	glm::vec3 DiffuseColor;
	glm::vec3 Specular;
	GLfloat Power;

	MaterialProperties(
		glm::vec3 ambientColor, 
		glm::vec3 diffuseColor,
		glm::vec3 specular,
		GLfloat power
	) {
		AmbientColor = ambientColor;
		DiffuseColor = diffuseColor;
		Specular = specular;
		Power = power;
	}
};