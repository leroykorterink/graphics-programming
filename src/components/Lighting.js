import AdvancedComponent from "../core/AdvancedComponent.js";

class Lighting extends AdvancedComponent {
  constructor(scene) {
    super();

    this.sun = this.createSun();

    this.ambientLight = new THREE.AmbientLight(0xffffff, 0.2);

    scene.add(this.sun);
    scene.add(this.ambientLight);
  }

  createSun() {
    const directionalLight = new THREE.DirectionalLight(0xffffff, 1);
    directionalLight.position.set(8, 25, 25);
    directionalLight.castShadow = true;

    directionalLight.shadow.camera.left = -50;
    directionalLight.shadow.camera.right = 50;
    directionalLight.shadow.camera.top = 50;
    directionalLight.shadow.camera.bottom = -50;

    directionalLight.shadow.camera.near = 1; // default
    directionalLight.shadow.camera.far = 100; // default

    return directionalLight;
  }

  update() {
    const matrix = new THREE.Matrix4();
    matrix.makeRotationY(0.0005);

    this.sun.position.applyMatrix4(matrix);
  }
}

export default Lighting;
