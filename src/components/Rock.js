import StaticComponent from "../core/StaticComponent.js";
import makeObjLoader from "../util/makeObjLoader.js";
import makeLoadTexture from "../util/makeLoadTexture.js";

const loadObj = makeObjLoader(fileName => `assets/Rock/${fileName}`);
const loadTexture = makeLoadTexture(fileName => `assets/Rock/${fileName}`);

const getRandom = (min, max) => {
  min = Math.ceil(min);
  max = Math.floor(max);

  //The maximum is inclusive and the minimum is inclusive
  return Math.floor(Math.random() * (max - min + 1)) + min;
};

export default (position, angle = 0) =>
  class Rock extends StaticComponent {
    constructor(scene) {
      super();

      this.init(scene);
    }

    async init(scene) {
      const radians = (Math.PI / 180) * angle;

      const group = await loadObj("rock_model.obj");
      const material = await loadTexture("rock_texture.png");
      material.specular = new THREE.Color(0.1, 0.1, 0.1);
      material.roughness = 1;

      group.children.forEach(mesh => {
        mesh.material.map = material;
        mesh.material.needsUpdate = true;
      });

      const mesh = group.children[getRandom(0, group.children.length)];

      mesh.rotateY(radians);
      mesh.geometry.computeBoundingBox();

      const translateMatrix = new THREE.Matrix4();
      translateMatrix.makeTranslation(
        position.x,
        position.y + -mesh.geometry.boundingBox.min.y,
        position.z
      );

      mesh.geometry.applyMatrix(translateMatrix);

      // Add mesh to scene
      scene.add(mesh);
    }
  };
