import AdvancedComponent from "./AdvancedComponent.js";

class Camera {
  constructor() {
    this.camera = new THREE.PerspectiveCamera(
      75, // fov — Camera frustum vertical field of view.
      window.innerWidth / window.innerHeight, // aspect — Camera frustum aspect ratio.
      0.1, // near — Camera frustum near plane.
      1000
    ); // far — Camera frustum far plane.

    this.camera.position.set(1, 1, 3);

    this.controls = new THREE.OrbitControls(this.camera);
  }

  getCamera() {
    return this.camera;
  }

  update() {
    this.controls.update();
  }
}

export default Camera;
