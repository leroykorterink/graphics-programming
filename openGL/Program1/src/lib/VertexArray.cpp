#include "VertexArray.h"

#include "Renderer.h"

VertexArray::VertexArray()
{
	// Allocate memory for vao
	glGenVertexArrays(1, &_rendererId);
}


VertexArray::~VertexArray()
{
	glDeleteVertexArrays(1, &_rendererId);
}

void VertexArray::Bind() const
{
	glBindVertexArray(_rendererId);
}

void VertexArray::Unbind() const
{
	glBindVertexArray(0);
}

void VertexArray::AddBuffer(const VertexBuffer & vertexBuffer, VertexBufferLayout & vertexBufferLayout)
{
	Bind();
	vertexBuffer.Bind();

	const auto& elements = vertexBufferLayout.GetElements();
	unsigned int offset = 0;

	for (unsigned int i = 0; i < elements.size(); i++) {
		const auto& element = elements[i];

		glEnableVertexAttribArray(i);
		glVertexAttribPointer(
			i,
			element.count, 
			element.type, 
			element.normalized, 
			vertexBufferLayout.GetStride(), 
			(const void*) offset
		);

		offset += element.count * VertexBufferElement::GetSizeOfType(element.type);
	}
}
