#version 430 core

// Material properties
uniform vec3 materialAmbient;
uniform vec3 materialDiffuse;
uniform vec3 materialSpecular;
uniform float materialPower;

// Input from vertex shader
in VS_OUT
{
    vec3 N;
    vec3 L;
    vec3 V;
} fs_in;

in vec2 UV;
uniform sampler2D textureSampler;

void main()
{
    // Normalize the incoming N, L and V vectors
    vec3 N = normalize(fs_in.N);
    vec3 L = normalize(fs_in.L);
    vec3 V = normalize(fs_in.V);

    // Calculate R locally
    vec3 R = reflect(-L, N);

    // Compute the diffuse and specular components for each fragment
    vec3 diffuse = max(dot(N, L), 0.0) * texture2D(textureSampler, UV).rgb;
    vec3 specular = pow(max(dot(R, V), 0.0), materialPower) * materialSpecular;

    // Write final color to the framebuffer
    gl_FragColor = vec4(materialAmbient + diffuse + specular, 1.0);
}
