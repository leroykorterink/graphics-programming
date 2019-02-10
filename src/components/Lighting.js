import AdvancedComponent from "../core/AdvancedComponent.js";

class Lighting extends AdvancedComponent {
  constructor(scene) {
    super();

    const ambientLight = new THREE.AmbientLight(0xffffff, 1);
    scene.add(ambientLight);

    const directionalLight = new THREE.DirectionalLight(0xffffff, 1);
    directionalLight.position.set(8, 12, 12);
    directionalLight.castShadow = true;

    directionalLight.shadow.camera.left = -25;
    directionalLight.shadow.camera.right = 25;
    directionalLight.shadow.camera.top = 25;
    directionalLight.shadow.camera.bottom = -25;

    directionalLight.shadow.mapSize.width = 512; // default
    directionalLight.shadow.mapSize.height = 512; // default
    directionalLight.shadow.camera.near = 1; // default
    directionalLight.shadow.camera.far = 50; // default

    scene.add(directionalLight);
    scene.add(new THREE.DirectionalLightHelper(directionalLight));
    scene.add(new THREE.CameraHelper(directionalLight.shadow.camera));
  }

  update() {}
}

export default Lighting;
