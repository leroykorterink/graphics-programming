import StaticComponent from "../core/StaticComponent.js";
import makeObjLoader from "../util/makeObjLoader.js";

const loadObj = makeObjLoader(fileName => `assets/Mill/${fileName}`);

export default (x, z, alpha) =>
  class Landscape extends StaticComponent {
    constructor(scene) {
      super();

      this.init(scene);
    }

    async init(scene) {
      const mesh = (await loadObj("mill_model.obj")).children[2];

      // Decrease scale
      var scaleMatrix = new THREE.Matrix4().makeScale(0.08, 0.08, 0.08);
      mesh.geometry.applyMatrix(scaleMatrix);

      // Move mesh to y 0
      mesh.geometry.computeBoundingBox();
      const boundingBox = mesh.geometry.boundingBox;

      const translateMatrix = new THREE.Matrix4();
      translateMatrix.makeTranslation(x, -boundingBox.min.y, z);

      mesh.geometry.applyMatrix(translateMatrix);

      // Rotate mesh
      const rotationMatrix = new THREE.Matrix4();
      rotationMatrix.makeRotationY(alpha);
      mesh.applyMatrix(rotationMatrix);

      // Add mesh to scene
      scene.add(mesh);
    }
  };
