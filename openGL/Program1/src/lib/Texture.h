#pragma once
class Texture
{
private:
	unsigned int _rendererId;
	const char*  _filePath;
	char* _localBuffer;
	int _width, _height, _BPP;

public:
	Texture(const char* path);
	~Texture();

	void Bind(unsigned int slot = 0) const;
	void Unbind() const;

	inline int GetWidth() const { return _width; }
	inline int GetHeight() const { return _height; }
};

