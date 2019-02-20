export default (createResourceUrl = name => name) => {
  const loader = new THREE.TextureLoader();

  return texture =>
    new Promise((resolve, reject) => {
      loader.load(
        // resource URL
        createResourceUrl(texture),
        // onLoad callback
        resolve,
        // onProgress callback currently not supported
        undefined,
        // onError callback
        reject
      );
    });
};
