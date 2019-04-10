#pragma once

#include "VertexBuffer.h"
#include "VertexBufferLayout.h"

class VertexBuffer
{
private:
	unsigned int _rendererId;

public:
	VertexBuffer(const void * data, unsigned int size);
	~VertexBuffer();

	void Bind() const;
	void Unbind() const;

	void AddBuffer(const VertexBuffer & vertexBuffer, const VertexBufferLayout & vertexBufferLayout);
};

