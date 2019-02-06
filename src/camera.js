const camera = new THREE.PerspectiveCamera(
  75, // fov — Camera frustum vertical field of view.
  window.innerWidth / window.innerHeight, // aspect — Camera frustum aspect ratio.
  0.1, // near — Camera frustum near plane.
  1000
); // far — Camera frustum far plane.

camera.position.set(1, 1, 3);

export const controls = new THREE.OrbitControls(camera);
export default camera;
