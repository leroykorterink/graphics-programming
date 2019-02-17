import makeObjLoader from "../util/makeObjLoader.js";
import makeLoadTexture from "../util/makeLoadTexture.js";
import AdvancedComponent from "../core/AdvancedComponent.js";

const loadObj = makeObjLoader(fileName => `assets/Airship/${fileName}`);
const loadTexture = makeLoadTexture(fileName => `assets/Airship/${fileName}`);

const convertToRadians = degrees => (Math.PI / 180) * degrees;

export default startPosition =>
  class Airship extends AdvancedComponent {
    constructor(scene) {
      super();

      const translate = new THREE.Matrix4().makeTranslation(-0.16, 0, 0);
      const rotateY = new THREE.Matrix4().makeRotationY(convertToRadians(0.2));

      this.transformationMatrix = new THREE.Matrix4().multiplyMatrices(
        translate,
        rotateY
      );

      this.init(scene);
    }

    async init(scene) {
      const group = await loadObj("airship_model.obj");
      const texture = await loadTexture("airship_texture.jpg");

      group.children.forEach(mesh => {
        mesh.material.map = texture;
        mesh.material.flatShading = true;
        mesh.material.needsUpdate = true;
      });

      const scaleMatrix = new THREE.Matrix4().makeScale(0.01, 0.01, 0.01);
      const rotateMatrix = new THREE.Matrix4().makeRotationX(
        convertToRadians(-65)
      );

      let matrix = new THREE.Matrix4().multiplyMatrices(
        scaleMatrix,
        rotateMatrix
      );

      group.applyMatrix(matrix);
      group.applyMatrix(
        new THREE.Matrix4().makeTranslation(
          startPosition.x,
          startPosition.y,
          startPosition.z
        )
      );

      this.group = group;

      scene.add(group);
    }

    update() {
      if (!this.group) return;

      this.group.applyMatrix(this.transformationMatrix);
    }
  };
