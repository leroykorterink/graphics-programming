#pragma once

#include "VertexArray.h"
#include "IndexBuffer.h"
#include "Shader.h"

class Renderer
{
public:
	Renderer();
	~Renderer();

	void Draw(const VertexArray & vertexArray, const IndexBuffer & indexBuffer, const Shader & shader);
	void Clear() const;
};

