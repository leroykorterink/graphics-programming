#pragma once

#include <string>
#include <unordered_map>

#include <glm/glm.hpp>

class Shader
{
private:
	unsigned int _rendererId;

	// Uniform cache
	std::unordered_map<const char *, int> _uniformLocationCache;

public:
	Shader(const char * vertexShaderPath, const char * fragmentShaderPath);
	~Shader();

	void Bind() const;
	void Unbind() const;

	void SetUniform1i(const char * variableName, int value);
	void SetUniform1f(const char * variableName, float value);
	void SetUniform4fv(const char * variableName, glm::vec4 value);
	void SetUniform3fv(const char * variableName, glm::vec3 value);

private:
	int GetUnitformLocation(const char * variableName);
};

