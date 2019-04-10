#include <GL/glew.h>
#include "Texture.h"
#include "../vendor/texture.h"

Texture::Texture(const char * path) : 
	_rendererId(0),
	_filePath(path),
	_localBuffer(nullptr), 
	_width(0),
	_height(0),
	_BPP(0)
{
	// TODO Load image file
	// - Modifiy textures.cpp file to expose width, height and image data

	glGenTextures(1, &_rendererId);
	glBindTexture(GL_TEXTURE_2D, _rendererId);

	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_CLAMP_TO_EDGE);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_CLAMP_TO_EDGE);

	// Load texture into gpu memory
	glTexImage2D(GL_TEXTURE_2D, 0, GL_RGBA8, _width, _height, 0, GL_RGBA, GL_UNSIGNED_BYTE, _localBuffer);
	glBindTexture(GL_TEXTURE_2D, 0);

	// We can clear the local buffer when the texture is loaded into gpu
	if (_localBuffer) {
		delete[] _localBuffer;
	}
}

Texture::~Texture()
{
	// Delete texture from gpu memory
	glDeleteTextures(1, &_rendererId);
}

void Texture::Bind(unsigned int slot) const
{
	glActiveTexture(GL_TEXTURE0 + slot);
	glBindTexture(GL_TEXTURE_2D, _rendererId);
}

void Texture::Unbind() const
{
	glBindTexture(GL_TEXTURE_2D, 0);
}
