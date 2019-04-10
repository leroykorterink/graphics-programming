#include <iostream>
#include <glm/gtc/type_ptr.hpp>

#include "Shader.h"
#include "../../glsl.h"

Shader::Shader(const char * vertexShaderPath, const char * fragmentShaderPath)
	: _rendererId(0)
{
	char * vertexshader = glsl::readFile(vertexShaderPath);
	char * fragshader = glsl::readFile(fragmentShaderPath);

	_rendererId = glsl::makeShaderProgram(
		glsl::makeVertexShader(vertexshader),
		glsl::makeFragmentShader(fragshader)
	);
}

Shader::~Shader()
{
}

void Shader::Bind() const
{
	glUseProgram(_rendererId);
}

void Shader::Unbind() const
{
	glUseProgram(0);
}

void Shader::SetUniform1i(const char * variableName, int value)
{
	glUniform1i(GetUnitformLocation(variableName), value);
}

void Shader::SetUniform1f(const char * variableName, float value)
{
	glUniform1f(GetUnitformLocation(variableName), value);
}

void Shader::SetUniform4fv(const char * variableName, glm::vec4 value)
{
	glUniform4fv(GetUnitformLocation(variableName), 1, glm::value_ptr(value));
}

void Shader::SetUniform3fv(const char * variableName, glm::vec3 value)
{
	glUniform3fv(GetUnitformLocation(variableName), 1, glm::value_ptr(value));
}

int Shader::GetUnitformLocation(const char * variableName)
{
	if (_uniformLocationCache.find(variableName) != _uniformLocationCache.end()) {
		return _uniformLocationCache[variableName];
	}

	int location = glGetUniformLocation(_rendererId, variableName);

	if (location == -1) {
		std::cerr << "Uniform " << variableName << "does not exist" << std::endl;
	}

	_uniformLocationCache[variableName] = location;

	return location;
}
