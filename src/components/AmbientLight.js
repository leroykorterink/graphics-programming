import StaticComponent from "../core/StaticComponent.js";

class AmbientLight extends StaticComponent {
  constructor(scene) {
    super();

    var light = new THREE.DirectionalLight("blue");
    light.castShadow = true;

    scene.add(new THREE.DirectionalLightHelper(light, 5));
    scene.add(light);
  }
}

export default AmbientLight;
