import StaticComponent from "../core/StaticComponent.js";
import makeObjLoader from "../util/makeObjLoader.js";

const loadObj = makeObjLoader(fileName => `assets/Mill/${fileName}`);

class Landscape extends StaticComponent {
  constructor(scene) {
    super();

    this.init(scene);
  }

  async init(scene) {
    const mesh = (await loadObj("mill_model.obj")).children[2];

    let matrix = new THREE.Matrix4();
    matrix.scale(0.08, 0.08, 0.08);

    mesh.applyMatrix(matrix);

    scene.add(mesh);
  }
}

export default Landscape;
