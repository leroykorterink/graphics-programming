import StaticComponent from "../core/StaticComponent.js";

const SIZE = 1000;

class Skybox extends StaticComponent {
  constructor() {
    super();

    const geometry = new THREE.BoxGeometry(SIZE, SIZE, SIZE);
    const material = new THREE.MeshNormalMaterial({
      color: 0x00ff00,
      side: THREE.BackSide
    });

    this.object = new THREE.Mesh(geometry, material);
  }
}

export default Skybox;
