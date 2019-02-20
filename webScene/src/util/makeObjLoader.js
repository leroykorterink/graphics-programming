export default (createResourceUrl = name => name, onProgress) => {
  const loader = new THREE.OBJLoader();

  return object =>
    new Promise((resolve, reject) => {
      loader.load(
        // resource URL
        createResourceUrl(object),
        // onLoad callback
        resolve,
        // onProgress callback
        onProgress,
        // onError callback
        reject
      );
    });
};
