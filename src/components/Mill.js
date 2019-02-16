import StaticComponent from "../core/StaticComponent.js";
import makeObjLoader from "../util/makeObjLoader.js";
import makeLoadTexture from "../util/makeLoadTexture.js";

const loadObj = makeObjLoader(fileName => `assets/Mill/${fileName}`);
const loadTexture = makeLoadTexture(fileName => `assets/Mill/${fileName}`);

export default (position, theta) =>
  class Landscape extends StaticComponent {
    constructor(scene) {
      super();

      this.init(scene);
    }

    async init(scene) {
      const mesh = (await loadObj("mill_model.obj")).children[2];
      const material = await loadTexture("mill_texture.jpg");

      mesh.material.map = material;
      mesh.material.needsUpdate = true;

      // Decrease scale
      var scaleMatrix = new THREE.Matrix4().makeScale(0.08, 0.08, 0.08);
      mesh.geometry.applyMatrix(scaleMatrix);

      // Move mesh to y 0
      mesh.geometry.computeBoundingBox();
      const boundingBox = mesh.geometry.boundingBox;

      const translateMatrix = new THREE.Matrix4();
      translateMatrix.makeTranslation(0, -boundingBox.min.y, 0);

      mesh.geometry.applyMatrix(translateMatrix);

      // Rotate mesh
      mesh.rotateY(theta);
      mesh.position.set(position.x, position.y, position.z);

      mesh.traverse(child => {
        if (!child.isMesh) return;

        child.receiveShadow = true;
        child.castShadow = true;
      });

      // Add mesh to scene
      scene.add(mesh);
    }
  };
