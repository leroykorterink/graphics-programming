import StaticComponent from "../core/StaticComponent.js";
import makeObjLoader from "../util/makeObjLoader.js";
import makeLoadTexture from "../util/makeLoadTexture.js";

const loadObj = makeObjLoader(fileName => `assets/Island/${fileName}`);
const loadTexture = makeLoadTexture(fileName => `assets/Island/${fileName}`);

export default position =>
  class Landscape extends StaticComponent {
    constructor(scene) {
      super();

      this.init(scene);
    }

    async init(scene) {
      const mesh = (await loadObj("island.obj")).children[0];

      const rockMaterial = new THREE.MeshStandardMaterial({
        color: "#383231",
        roughness: 0.6,
        metalness: 0.3,
        flatShading: true
      });

      const topMaterial = new THREE.MeshStandardMaterial({
        color: "#005229",
        roughness: 0.9,
        metalness: 0.1
      });

      mesh.material[0] = rockMaterial;
      mesh.material[1] = topMaterial;

      mesh.material[0].needsUpdate = true;
      mesh.material[1].needsUpdate = true;

      mesh.traverse(child => {
        if (!child.isMesh) return;

        child.receiveShadow = true;
        child.castShadow = true;
      });

      mesh.receiveShadow = true;
      mesh.castShadow = true;

      mesh.applyMatrix(new THREE.Matrix4().makeScale(5, 3, 5));

      mesh.geometry.computeBoundingBox();

      mesh.translateX(position.x);
      mesh.translateY(-mesh.geometry.boundingBox.max.y * 3 + position.y);
      mesh.translateZ(position.z);

      // Add mesh to scene
      scene.add(mesh);
    }
  };
