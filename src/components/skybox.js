import StaticComponent from "../core/StaticComponent.js";
import makeLoadTexture from "../util/makeLoadTexture.js";

const SIZE = 1000;

const directions = ["front", "back", "top", "bottom", "right", "left"];

const loadTexture = makeLoadTexture(
  direction => `assets/Skybox/cloudtop_${direction}.png`
);

class Skybox extends StaticComponent {
  constructor(scene) {
    super();
    this.createMesh(scene);
  }

  async createMesh(scene) {
    const materials = await Promise.all(
      directions.map(
        async direction =>
          new THREE.MeshBasicMaterial({
            map: await loadTexture(direction),
            side: THREE.BackSide
          })
      )
    );

    const mesh = new THREE.Mesh(
      new THREE.CubeGeometry(SIZE, SIZE, SIZE),
      materials
    );

    scene.add(mesh);
  }
}

export default Skybox;
