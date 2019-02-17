import StaticComponent from "../core/StaticComponent.js";
import makeFBXLoader from "../util/makeFBXLoader.js";
import makeLoadTexture from "../util/makeLoadTexture.js";

const loadFBX = makeFBXLoader(fileName => `assets/Rooster/${fileName}`);

export default (position, angle = 0) =>
  class Rock extends StaticComponent {
    constructor(scene) {
      super();

      this.init(scene);
    }

    async init(scene) {
      const group = await loadFBX("rooster_model.fbx");
      const mesh = group.children[0];

      mesh.material = new THREE.MeshStandardMaterial({
        color: "#ffffff",
        roughness: 1,
        metalness: 0.1,
        flatShading: true
      });

      // Add mesh to scene
      scene.add(mesh);
    }
  };
