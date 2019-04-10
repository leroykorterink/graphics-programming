#pragma once

#include <GL/glew.h>
#include <vector>

struct VertexBufferElement 
{
	unsigned int type;
	unsigned int count;
	unsigned char normalized;

	static unsigned int GetSizeOfType(unsigned int type) {
		switch (type) {
			case GL_FLOAT: return 4;
			case GL_UNSIGNED_INT: return 4;
			case GL_UNSIGNED_BYTE: return 1;
		}

		return 0;
	}
};

class VertexBufferLayout
{
private:
	std::vector<VertexBufferElement> _elements;
	unsigned int _stride;

public:
	VertexBufferLayout()
		: _stride(0) {};

	inline unsigned int GetStride() const { return _stride; }
	inline const std::vector<VertexBufferElement> & GetElements() const { return _elements; }

	template<typename T>
	void Push(unsigned int count) {
		static_assert(false);
	}

	template<>
	void Push<float>(unsigned int count) {
		_elements.push_back({ count, GL_FLOAT, GL_FALSE });
		_stride += VertexBufferElement::GetSizeOfType(GL_FLOAT) * count;
	}

	template<>
	void Push<unsigned int>(unsigned int count) {
		_elements.push_back({ GL_UNSIGNED_INT, count, GL_TRUE });
		_stride += VertexBufferElement::GetSizeOfType(GL_UNSIGNED_INT) * count;
	}
	
	template<>
	void Push<unsigned char>(unsigned int count) {
		_elements.push_back({ GL_UNSIGNED_BYTE, count, GL_FALSE });
		_stride += VertexBufferElement::GetSizeOfType(GL_UNSIGNED_BYTE) * count;
	}
};

